using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;

[BurstCompile]
partial struct CubeRotateJobEntity : IJobEntity//JobEntity也是用JobChunk来实现，所以两者在性能上差异不大。
{
    public float deltaTime;
    public void Execute(ref LocalTransform transform, in RotateSpeed speed)
    {
        transform = transform.RotateY(speed.rotateSpeed * deltaTime);
    }
}

[BurstCompile]
[UpdateInGroup(typeof(Lesson05SystemGroup))]
partial struct CubeRotateJobEntitySystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var job = new CubeRotateJobEntity() { deltaTime = SystemAPI.Time.DeltaTime };
        job.ScheduleParallel();
    }
}