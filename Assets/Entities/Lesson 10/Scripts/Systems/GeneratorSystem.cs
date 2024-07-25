using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Entity_Lesson10
{
    [RequireMatchingQueriesForUpdate]
    [UpdateInGroup(typeof(Lesson10SystemGroup))]
    partial struct GeneratorSystem : ISystem
    {
        float timer;
        int totalCubes;
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<GeneratorComponent>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var generator = SystemAPI.GetSingleton<GeneratorComponent>();
            if (totalCubes >= generator.count)
            {
                state.Enabled = false;
                return;
            }
            if (timer >= generator.tickTime)
            {
                var entityArray = CollectionHelper.CreateNativeArray<Entity>(generator.countOfTickTime, Allocator.Temp);
                state.EntityManager.Instantiate(generator.prefab, entityArray);
                foreach (var entity in entityArray)
                {
                    state.EntityManager.AddComponentData(entity, new RotationComponent { rotationSpeed = math.radians(generator.rotationSpeed) });
                    state.EntityManager.AddComponentData(entity, new MovementComponent { movementSpeed = generator.movementSpeed });
                    var randomSingleton = SystemAPI.GetSingletonRW<RandomComponent>();
                    var targetRange = generator.targetRange;
                    var randomPosition = randomSingleton.ValueRW.random.NextFloat3(-targetRange * 0.5f, targetRange * 0.5f);
                    var targetPosition = generator.targetPosition + new float3(randomPosition.x, 0, randomPosition.z);
                    state.EntityManager.AddComponentData(entity, new RandomTargetComponent { targetPosition = targetPosition });
                    randomSingleton = SystemAPI.GetSingletonRW<RandomComponent>();//??
                    var generationRange = generator.generationRange;
                    randomPosition = randomSingleton.ValueRW.random.NextFloat3(-generationRange * 0.5f, generationRange * 0.5f);
                    var generationPosition = generator.generationPosition + new float3(randomPosition.x, 0, randomPosition.z);
                    var transform = SystemAPI.GetComponentRW<LocalTransform>(entity);
                    transform.ValueRW.Position = generationPosition;
                }
                entityArray.Dispose();
                totalCubes += generator.countOfTickTime;
                timer = 0;
            }
            timer += SystemAPI.Time.DeltaTime;
        }
    }
}