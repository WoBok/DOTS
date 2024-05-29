using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;

[BurstCompile]
partial struct CubeRotateJobEntity : IJobEntity//JobEntityҲ����JobChunk��ʵ�֣����������������ϲ��첻��
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