using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;

[UpdateInGroup(typeof(RotateCubeSystemGroup))]
//[UpdateBefore(typeof())]
[BurstCompile]
public partial struct RotateCubeSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var deltaTime = SystemAPI.Time.DeltaTime;
        foreach (var (transform, speed) in
             SystemAPI.Query<RefRW<LocalTransform>, RefRO<RotateSpeed>>()
            )
        {
            transform.ValueRW = transform.ValueRO.RotateY(speed.ValueRO.rotateSpeed * deltaTime);
        }
    }
}