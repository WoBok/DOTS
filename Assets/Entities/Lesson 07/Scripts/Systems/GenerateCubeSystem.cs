using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

[BurstCompile]
[RequireMatchingQueriesForUpdate]
[UpdateInGroup(typeof(Lesson07SystemGroup))]
partial struct GenerateCubeSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<GenerateCubeComponent>();
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var generator = SystemAPI.GetSingleton<GenerateCubeComponent>();
        var cubesArray = CollectionHelper.CreateNativeArray<Entity>(generator.xCount * generator.zCount, Allocator.Temp);
        state.EntityManager.Instantiate(generator.prefab, cubesArray);

        int count = 0;
        foreach (var entity in cubesArray)
        {
            state.EntityManager.AddComponent<WaveCubeTagComponent>(entity);

            var x = (count % generator.xCount - generator.xCount / 2) * 2;
            var z = (count / generator.zCount - generator.zCount / 2) * 2;
            var position = new float3(x, 0, z);

            var transform = SystemAPI.GetComponentRW<LocalTransform>(entity);
            transform.ValueRW.Position = position;
            count++;
        }

        cubesArray.Dispose();

        state.Enabled = false;
    }

    [BurstCompile]
    public void OnDestroy(ref SystemState state)
    {

    }
}