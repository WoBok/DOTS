using Unity.Burst;
using Unity.Entities;

namespace Entity_Lesson10
{
    [RequireMatchingQueriesForUpdate]
    [UpdateInGroup(typeof(Lesson10SystemGroup))]
    partial struct MarchingSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {

        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {

        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {

        }
    }
}