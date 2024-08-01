using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Entity_Lesson11
{
    [RequireMatchingQueriesForUpdate]
    [UpdateInGroup(typeof(Lesson11SystemGroup))]
    [UpdateAfter(typeof(GeneratorSystem))]
    partial struct MarchingSystem : ISystem
    {
        EntityQuery entityQuery;
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            var queryBuilder = new EntityQueryBuilder(Allocator.TempJob).WithAll<LocalTransform, GroupSharedComponent>();
            entityQuery = state.GetEntityQuery(queryBuilder);
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;
            var elapsedTime = SystemAPI.Time.ElapsedTime;

            var generator = SystemAPI.GetSingleton<GeneratorComponent>();
            entityQuery.SetSharedComponentFilter(new GroupSharedComponent { group = 1 });
            var entities = entityQuery.ToEntityArray(Allocator.Temp);
            var localTransforms = entityQuery.ToComponentDataArray<LocalTransform>(Allocator.Temp);
            for (int i = 0; i < entities.Length; i++)
            {
                var speedComponent = state.EntityManager.GetSharedComponent<SpeedSharedComponent>(entities[i]);
                var transform = localTransforms[i];
                if (transform.Position.x > generator.cubeTargetPos.x)
                {
                    state.EntityManager.DestroyEntity(entities[i]);
                }
                else
                {
                    transform.Position += speedComponent.movementSpeed * deltaTime * new float3(1, (float)math.sin(elapsedTime * 20), 0);
                    transform.RotateY(speedComponent.rotationSpeed * deltaTime);
                    state.EntityManager.SetComponentData(entities[i], transform);
                }
            }
            entities.Dispose();
            localTransforms.Dispose();
        }
    }
}