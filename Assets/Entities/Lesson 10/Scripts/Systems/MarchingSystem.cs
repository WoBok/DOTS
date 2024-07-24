using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Profiling;
using Unity.Transforms;

namespace Entity_Lesson10
{
    [RequireMatchingQueriesForUpdate]
    [UpdateInGroup(typeof(Lesson10SystemGroup))]
    partial struct MarchingSystem : ISystem
    {
        static readonly ProfilerMarker profilerMarker = new ProfilerMarker(nameof(MarchingSystem));
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

        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {

        }
    }
}