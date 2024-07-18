using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;

namespace Entity_Lesson08
{
    [UpdateInGroup(typeof(Lesson08SystemGroup))]
    partial struct GeneratorSystem : ISystem
    {
        float timer;
        int totalEntitys;

        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<GeneratorComponent>();
            //state.RequireForUpdate<RandomComponent>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var generatorComponent = SystemAPI.GetSingleton<GeneratorComponent>();
            if (totalEntitys >= generatorComponent.totalNum)
            {
                state.Enabled = false;
                return;
            }
            if (timer >= generatorComponent.tickTime)
            {
                GenerateEntityJob(ref state, generatorComponent);
                timer = 0;
            }
            timer += generatorComponent.tickTime;
        }
        void GenerateEntityJob(ref SystemState state, GeneratorComponent generatorComponent)
        {
            RefRW<RandomComponent> random = SystemAPI.GetSingletonRW<RandomComponent>();
            EntityCommandBuffer ecb = new EntityCommandBuffer(Allocator.TempJob);
            var entityArray = CollectionHelper.CreateNativeArray<Entity>(generatorComponent.perTickTimeNum, Allocator.TempJob);
            if (generatorComponent.isUseParallel)
            {
                EntityCommandBuffer.ParallelWriter parallelWriter = ecb.AsParallelWriter();
                var job = new GeneratorWithParallelWriterJob
                {
                    prefab = generatorComponent.prefab,
                    entities = entityArray,
                    parallelWriter = parallelWriter,
                    random = random
                };
                state.Dependency = job.ScheduleParallel(entityArray.Length, 1, state.Dependency);
            }
            else
            {
                var job = new GeneratorJob
                {
                    prefab = generatorComponent.prefab,
                    entities = entityArray,
                    ecb = ecb,
                    random = random
                };
                state.Dependency = job.Schedule(entityArray.Length, state.Dependency);
            }
            state.Dependency.Complete();
            ecb.Playback(state.EntityManager);
            totalEntitys += generatorComponent.perTickTimeNum;
            entityArray.Dispose();
            ecb.Dispose();
        }
    }
}