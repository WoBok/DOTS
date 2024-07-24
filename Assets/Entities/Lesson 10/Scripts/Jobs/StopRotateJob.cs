using Unity.Burst.Intrinsics;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Entity_Lesson10
{
    public partial struct StopRotateJob : IJobChunk
    {
        public float deltaTime;
        public float elapsedTime;
        public float2 leftRightBound;
        public ComponentTypeHandle<LocalTransform> transformTypeHandle;
        public ComponentTypeHandle<RotationComponent> rotationSpeedTypeHandle;

        public void Execute(in ArchetypeChunk chunk, int unfilteredChunkIndex, bool useEnabledMask, in v128 chunkEnabledMask)
        {
            var transformChunk = chunk.GetNativeArray(ref transformTypeHandle);
            var rotationSpeedChunk = chunk.GetNativeArray(ref rotationSpeedTypeHandle);
            var enumerator = new ChunkEntityEnumerator(useEnabledMask, chunkEnabledMask, chunk.Count);
            while (enumerator.NextEntityIndex(out var i))
            {
                var enable = chunk.IsComponentEnabled(ref rotationSpeedTypeHandle, i);
                if (enable)
                {
                    if (transformChunk[i].Position.x > leftRightBound.x && transformChunk[i].Position.x < leftRightBound.y)
                    {
                        chunk.SetComponentEnabled(ref rotationSpeedTypeHandle, i, false);
                    }
                    else
                    {
                        var speed = rotationSpeedChunk[i];
                        transformChunk[i] = transformChunk[i].RotateY(speed.rotationSpeed * deltaTime);
                    }
                }
                else
                {
                    if (transformChunk[i].Position.x < leftRightBound.x || transformChunk[i].Position.x > leftRightBound.y)
                    {
                        chunk.SetComponentEnabled(ref rotationSpeedTypeHandle, i, true);
                        var trans = transformChunk[i];
                        trans.Scale = 1;
                        transformChunk[i] = trans;
                    }
                    else
                    {
                        var trans = transformChunk[i];
                        trans.Scale = math.sin(elapsedTime * 4) * 0.3f + 1;
                        transformChunk[i] = trans;
                    }
                }
            }
        }
    }
}