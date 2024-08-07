using System;
using Unity.Collections;
using UnityEngine.Assertions;

namespace UnityEngine.Rendering.UnifiedRayTracing
{
    internal struct BlockAllocator : IDisposable
    {
        public struct Block
        {
            public int offset;
            public int count;

            public static Block Invalid = new Block() { offset = 0, count = 0 };
        }

        public struct Allocation
        {
            public int handle;
            public Block block;

            public static Allocation Invalid = new Allocation() { handle = -1 };
            public bool valid => handle != -1;
        }

        private int m_freeElementCount;
        private int m_MaxElementCount;
        private NativeList<Block> m_freeBlocks;
        private NativeList<Block> m_usedBlocks;
        private NativeList<int> m_freeSlots;

        public int freeElementsCount => m_freeElementCount;
        public int freeBlocks => m_freeBlocks.Length;
        public int capacity => m_MaxElementCount;

        public void Initialize(int maxElementCounts)
        {
            m_MaxElementCount = maxElementCounts;
            m_freeElementCount = maxElementCounts;

            if (!m_freeBlocks.IsCreated)
                m_freeBlocks = new NativeList<Block>(Allocator.Persistent);
            else
                m_freeBlocks.Clear();
            m_freeBlocks.Add(new Block() { offset = 0, count = m_freeElementCount });

            if (!m_usedBlocks.IsCreated)
                m_usedBlocks = new NativeList<Block>(Allocator.Persistent);
            else
                m_usedBlocks.Clear();

            if (!m_freeSlots.IsCreated)
                m_freeSlots = new NativeList<int>(Allocator.Persistent);
            else
                m_freeSlots.Clear();
        }

        private int CalculateGeometricGrowthCapacity(int newMaxElementCount)
        {
            var oldCapacity = capacity;

            if (oldCapacity > int.MaxValue - oldCapacity / 2)
            {
                return int.MaxValue; // geometric growth would overflow
            }

            var geometricNewCapacity = oldCapacity + oldCapacity / 2;

            if (geometricNewCapacity < newMaxElementCount)
                return newMaxElementCount; // geometric growth would be insufficient

            else
                return geometricNewCapacity;
        }

        public int Grow(int newMaxElementCount)
        {
            newMaxElementCount = CalculateGeometricGrowthCapacity(newMaxElementCount);

            var oldCapacity = m_MaxElementCount;
            var addedElements = newMaxElementCount - oldCapacity;

            if (addedElements <= 0)
                throw new System.Exception("newMaxElementCount must be greater than current capacity");

            m_freeElementCount += addedElements;
            m_MaxElementCount = newMaxElementCount;

            int blockToMerge = m_freeBlocks.Length;
            m_freeBlocks.Add(new Block() { offset = oldCapacity, count = addedElements });

            while (blockToMerge != -1)
                blockToMerge = MergeBlockFrontBack(blockToMerge);

            return m_MaxElementCount;
        }

        public Allocation GrowAndAllocate(int elementCounts, out int oldCapacity, out int newCapacity)
        {
            oldCapacity = capacity;
            newCapacity = Grow(capacity + elementCounts);
            var alloc = Allocate(elementCounts);
            Assert.IsTrue(alloc.valid);
            return alloc;
        }

        public void Dispose()
        {
            m_MaxElementCount = 0;
            m_freeElementCount = 0;
            if (m_freeBlocks.IsCreated)
                m_freeBlocks.Dispose();
            if (m_usedBlocks.IsCreated)
                m_usedBlocks.Dispose();
            if (m_freeSlots.IsCreated)
                m_freeSlots.Dispose();
        }

        public Allocation Allocate(int elementCounts)
        {
            if (elementCounts > m_freeElementCount || m_freeBlocks.IsEmpty)
                return Allocation.Invalid;

            int selectedBlock = -1;
            int currentBlockCount = 0;
            for (int b = 0; b < m_freeBlocks.Length; ++b)
            {
                Block block = m_freeBlocks[b];

                //simple naive allocator, we find the smallest possible space to allocate in our blocks.
                if (elementCounts <= block.count && (selectedBlock == -1 || block.count < currentBlockCount))
                {
                    currentBlockCount = block.count;
                    selectedBlock = b;
                }
            }

            if (selectedBlock == -1)
                return Allocation.Invalid;

            Block allocationBlock = m_freeBlocks[selectedBlock];
            Block split = allocationBlock;

            split.offset += elementCounts;
            split.count -= elementCounts;
            allocationBlock.count = elementCounts;

            if (split.count > 0)
                m_freeBlocks[selectedBlock] = split;
            else
                m_freeBlocks.RemoveAtSwapBack(selectedBlock);

            int allocationHandle;
            if (m_freeSlots.IsEmpty)
            {
                allocationHandle = (int)m_usedBlocks.Length;
                m_usedBlocks.Add(allocationBlock);
            }
            else
            {
                allocationHandle = m_freeSlots[m_freeSlots.Length - 1];
                m_freeSlots.RemoveAtSwapBack(m_freeSlots.Length - 1);
                m_usedBlocks[allocationHandle] = allocationBlock;
            }

            m_freeElementCount -= elementCounts;
            return new Allocation() { handle = allocationHandle, block = allocationBlock };
        }

        private int MergeBlockFrontBack(int freeBlockId)
        {
            Block targetBlock = m_freeBlocks[freeBlockId];
            for (int i = 0; i < m_freeBlocks.Length; ++i)
            {
                if (i == freeBlockId)
                    continue;

                Block freeBlock = m_freeBlocks[i];
                bool mergeTargetBlock = false;
                if (targetBlock.offset == (freeBlock.offset + freeBlock.count))
                {
                    freeBlock.count += targetBlock.count;
                    mergeTargetBlock = true;
                }
                else if (freeBlock.offset == (targetBlock.offset + targetBlock.count))
                {
                    freeBlock.offset = targetBlock.offset;
                    freeBlock.count += targetBlock.count;
                    mergeTargetBlock = true;
                }

                if (mergeTargetBlock)
                {
                    m_freeBlocks[i] = freeBlock;
                    m_freeBlocks.RemoveAtSwapBack(freeBlockId);
                    return i == m_freeBlocks.Length ? freeBlockId : i;
                }
            }

            return -1;
        }

        public void FreeAllocation(in Allocation allocation)
        {
            if (!allocation.valid)
                throw new System.Exception("Cannot free invalid allocation");

            m_freeSlots.Add(allocation.handle);
            m_usedBlocks[allocation.handle] = Block.Invalid;

            int blockToMerge = m_freeBlocks.Length;
            m_freeBlocks.Add(allocation.block);

            while (blockToMerge != -1)
                blockToMerge = MergeBlockFrontBack(blockToMerge);

            m_freeElementCount += allocation.block.count;
        }
    }
}
