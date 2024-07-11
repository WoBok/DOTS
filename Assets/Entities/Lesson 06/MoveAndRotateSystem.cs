using Unity.Burst;
using Unity.Entities;

[BurstCompile]
[RequireMatchingQueriesForUpdate]
[UpdateInGroup(typeof(GeneratePrefabSystemGroup))]
partial struct MoveAndRotateSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<MoveAndRotateComponent>();
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        float deltaTime = SystemAPI.Time.DeltaTime;
        double elapsedTime = SystemAPI.Time.ElapsedTime;

        foreach (var aspect in SystemAPI.Query<MoveAndRotateAspect>())
        {
            aspect.MoveAndRotate((float)elapsedTime, deltaTime);
        }
    }
}