using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;

public partial struct GeneratorWithParallelWriterJob : IJobFor
{
    [ReadOnly] public Entity prefab;
    public NativeArray<Entity> entities;
    public EntityCommandBuffer.ParallelWriter parallelWriter;
    [NativeDisableUnsafePtrRestriction] public RefRW<RandomComponent> random;

    public void Execute(int index)
    {
        entities[index] = parallelWriter.Instantiate(index, prefab);
        parallelWriter.AddComponent(index, entities[index], new MoveAndRotateComponent
        {
            rotateSpeed = math.radians(60),
            moveSpeed = 5
        });
        var targetPosition = random.ValueRW.random.NextFloat2(new float2(-15, -15), new float2(15, 15));
        parallelWriter.AddComponent(index, entities[index], new RandomTargetComponent
        {
            targetPosition = new float3(targetPosition.x, 0, targetPosition.y)
        });
    }
}