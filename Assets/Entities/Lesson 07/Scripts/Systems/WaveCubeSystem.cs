using Unity.Burst;
using Unity.Entities;
using Unity.Profiling;

[BurstCompile]
[RequireMatchingQueriesForUpdate]
[UpdateInGroup(typeof(Lesson07SystemGroup))]
partial struct WaveCubeSystem : ISystem
{
    static readonly ProfilerMarker profilerMarker = new ProfilerMarker("WaveCubeJobEntity");
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        //state.RequireForUpdate<WaveCubeTagComponent>();//??会让WaveCubeSystem增加一倍
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        using (profilerMarker.Auto())
        {
            var job = new WaveCubeJobEntity() { elapsedTime = (float)SystemAPI.Time.ElapsedTime };
            job.ScheduleParallel();
        }
    }
}