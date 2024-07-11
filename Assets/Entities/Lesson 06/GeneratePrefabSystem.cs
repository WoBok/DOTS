using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

[BurstCompile]
[RequireMatchingQueriesForUpdate]
[UpdateInGroup(typeof(GeneratePrefabSystemGroup))]
partial struct GeneratePrefabSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<GeneratePrefab>();
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var generator = SystemAPI.GetSingleton<GeneratePrefab>();
        var cubesArray = CollectionHelper.CreateNativeArray<Entity>(generator.count, Allocator.Temp);
        state.EntityManager.Instantiate(generator.entity, cubesArray);
        int count = 0;
        foreach (var entity in cubesArray)
        {
            var moveAndRotateComponent = new MoveAndRotateComponent()
            {
                rotateSpeed = (count + 1) * math.radians(90),
                moveSpeed = (count + 1)
            };
            state.EntityManager.AddComponentData<MoveAndRotateComponent>(entity, moveAndRotateComponent);

            var position = new float3((count - generator.count / 2) * 1.2f, 0, 0);
            var transform = SystemAPI.GetComponentRW<LocalTransform>(entity);
            transform.ValueRW.Position = position;
            count++;
        }
        cubesArray.Dispose();

        state.Enabled = false;
    }
}