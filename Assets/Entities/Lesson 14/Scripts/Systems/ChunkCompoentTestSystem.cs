using Unity.Burst;
using Unity.Entities;

namespace Entity_Lesson14
{
    [UpdateInGroup(typeof(Lesson14SystemGroup))]
    partial struct ChunkCompoentTestSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            var entity1 = state.EntityManager.CreateEntity();
            state.EntityManager.AddComponentData(entity1, new GeneralComponent { num = 1 });
            state.EntityManager.AddChunkComponentData<ChunkComponentA>(entity1);

            var entity2 = state.EntityManager.CreateEntity();
            state.EntityManager.AddComponentData(entity2, new GeneralComponent { num = 2 });
            state.EntityManager.AddChunkComponentData<ChunkComponentA>(entity2);
            state.EntityManager.AddChunkComponentData<ChunkComponentB>(entity2);

            ArchetypeChunk chunk = state.EntityManager.GetChunk(entity1);
            state.EntityManager.SetChunkComponentData(chunk, new ChunkComponentA { numA = 2 });

            var entity3 = state.EntityManager.CreateEntity();
            state.EntityManager.AddChunkComponentData<ChunkComponentA>(entity3);
            state.EntityManager.AddChunkComponentData<ChunkComponentB>(entity3);

            state.Enabled = false;
        }
    }
}

//var entity4 = state.EntityManager.CreateEntity();
//state.EntityManager.AddChunkComponentData<ChunkComponentAB>(entity4);