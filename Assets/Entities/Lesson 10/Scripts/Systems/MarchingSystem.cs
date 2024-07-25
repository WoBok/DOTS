using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Profiling;
using Unity.Transforms;

namespace Entity_Lesson10
{
    [RequireMatchingQueriesForUpdate]
    [UpdateInGroup(typeof(Lesson10SystemGroup))]
    partial struct MarchingSystem : ISystem
    {
        //static readonly ProfilerMarker profilerMarker = new ProfilerMarker("CubesMarchWithEntity");
        EntityQuery entityQuery;
        ComponentTypeHandle<LocalTransform> transformTypeHandle;
        ComponentTypeHandle<RotationComponent> rotationComponentTypeHandle;
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<MovementComponent>();
            state.RequireForUpdate<RotationComponent>();

            var queryBuilder = new EntityQueryBuilder(Allocator.Temp).WithAll<RotationComponent, LocalTransform>().WithOptions(EntityQueryOptions.IgnoreComponentEnabledState);
            entityQuery = state.GetEntityQuery(queryBuilder);

            transformTypeHandle = state.GetComponentTypeHandle<LocalTransform>();
            rotationComponentTypeHandle = state.GetComponentTypeHandle<RotationComponent>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            //using (profilerMarker.Auto())
            {
                var generator = SystemAPI.GetSingleton<GeneratorComponent>();
                transformTypeHandle.Update(ref state);
                rotationComponentTypeHandle.Update(ref state);
                var job1 = new StopRotateJob()
                {
                    deltaTime = SystemAPI.Time.DeltaTime,
                    elapsedTime = (float)SystemAPI.Time.ElapsedTime,
                    leftRightBound = new float2(generator.generationPosition.x / 2, generator.targetPosition.x / 2),
                    transformTypeHandle = transformTypeHandle,
                    rotationSpeedTypeHandle = rotationComponentTypeHandle
                };
                state.Dependency = job1.ScheduleParallel(entityQuery, state.Dependency);

                var ecb = new EntityCommandBuffer(Allocator.TempJob);
                var parallelWriter = ecb.AsParallelWriter();

                var job2 = new MarchingJob()
                {
                    deltaTime = SystemAPI.Time.DeltaTime,
                    parallelWriter = parallelWriter
                };
                state.Dependency = job2.ScheduleParallel(state.Dependency);
                state.Dependency.Complete();
                ecb.Playback(state.EntityManager);
                ecb.Dispose();
            }
        }
    }
}