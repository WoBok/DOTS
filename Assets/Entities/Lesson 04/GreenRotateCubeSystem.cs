using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;

[DisableAutoCreation]
[UpdateInGroup(typeof(Lesson04SystemGroup))]
[BurstCompile]
public partial struct GreenRotateCubeSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var deltaTime = SystemAPI.Time.DeltaTime;
        foreach (var (transform, speed, _) in
             SystemAPI.Query<RefRW<LocalTransform>, RefRO<RotateSpeed>, RefRO<GreenCubeTag>>()
            )
        {
            transform.ValueRW = transform.ValueRO.RotateY(speed.ValueRO.rotateSpeed * deltaTime);
        }
    }
}