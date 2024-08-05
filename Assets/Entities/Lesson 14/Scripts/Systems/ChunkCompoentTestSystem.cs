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
            state.EntityManager.AddComponentData<GeneralComponent>(entity1, new GeneralComponent { num = 1 });
            state.EntityManager.AddChunkComponentData<ChunkComponentA>(entity1);
            state.Enabled = false;
        }
    }
}