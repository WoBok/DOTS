using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Entity_Lesson12
{
    [RequireMatchingQueriesForUpdate]
    [UpdateInGroup(typeof(Lesson12SystemGroup))]
    partial struct GeneratorSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<GeneratorComponent>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var generator = SystemAPI.GetSingleton<GeneratorComponent>();
            var entityArray = CollectionHelper.CreateNativeArray<Entity>(generator.xCount * generator.zCount, Allocator.Temp);
            state.EntityManager.Instantiate(generator.prefab, entityArray);
            int count = 0;
            foreach (var entity in entityArray)
            {
                int x = count % generator.xCount - generator.xCount / 2;
                int z = count / generator.zCount - generator.zCount / 2;
                float3 position = new float3(x * 1.2f, 0, z * 1.2f);

                var transform = SystemAPI.GetComponentRW<LocalTransform>(entity);
                transform.ValueRW.Position = position;

                count++;
            }
            entityArray.Dispose();
            state.Enabled = false;
        }
    }
}