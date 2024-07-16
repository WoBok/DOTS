using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using UnityEngine;

namespace Entity_Lesson07
{
    partial struct GeneratorSystem : ISystem
    {
        float timer;
        int totalEntitys;
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
            //RefRW<RandomComponent> random = SystemAPI.GetSingletonRW<RandomComponent>();
            EntityCommandBuffer ecb = new EntityCommandBuffer(Allocator.TempJob);
            var entityArray = CollectionHelper.CreateNativeArray<Entity>(generatorComponent.perTickTimeNum, Allocator.TempJob);
            if (generatorComponent.isUseParallel)
            {
                Debug.Log("Uncompleted...");
            }
            else
            {
                var job = new GeneratorJob
                {
                    prefab = generatorComponent.prefab,
                    entities = entityArray,
                    ecb = ecb
                    //random = random
                };
                state.Dependency = job.Schedule(entityArray.Length, state.Dependency);
            }
            state.Dependency.Complete();
            ecb.Playback(state.EntityManager);
            entityArray.Dispose();
            ecb.Dispose();
        }
    }
}