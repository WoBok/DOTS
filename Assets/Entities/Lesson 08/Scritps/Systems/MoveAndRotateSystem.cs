using Unity.Burst;
using Unity.Entities;

namespace Entity_Lesson08
{
    [UpdateAfter(typeof(GeneratorSystem))]
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
            var ecb = new EntityCommandBuffer(Unity.Collections.Allocator.TempJob);
            EntityCommandBuffer.ParallelWriter paralleWriter = ecb.AsParallelWriter();
            var job = new MoveAndRotateJob
            {
                deltaTime = SystemAPI.Time.DeltaTime,
                parallelWriter = paralleWriter
            };
            state.Dependency = job.ScheduleParallel(state.Dependency);
            state.Dependency.Complete();
            ecb.Playback(state.EntityManager);
            ecb.Dispose();
        }
    }
}