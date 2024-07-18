using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Entity_Lesson09
{
    [RequireMatchingQueriesForUpdate]
    [UpdateInGroup(typeof(Lesson09SystemGroup))]
    partial struct MovementUsingWayPointsSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<WayPointsComponent>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            DynamicBuffer<WayPointsComponent> wayPoints = SystemAPI.GetSingletonBuffer<WayPointsComponent>();
            var deltaTime = SystemAPI.Time.DeltaTime;
            if (!wayPoints.IsEmpty)
            {
                foreach (var (transform, nextIndex, speed) in SystemAPI.Query<RefRW<LocalTransform>, RefRW<NextWayPointComponent>, RefRW<MoveAndRotateComponent>>())
                {
                    var direction = wayPoints[(int)nextIndex.ValueRO.nextIndex].wayPoint - transform.ValueRO.Position;
                    direction = math.normalize(direction);
                    transform.ValueRW.Position += direction * deltaTime * speed.ValueRO.moveSpeed;
                    transform.ValueRW.Rotation = Quaternion.Lerp(transform.ValueRW.Rotation, Quaternion.LookRotation(direction), speed.ValueRO.rotateSpeed * deltaTime);
                    if (math.distance(wayPoints[(int)nextIndex.ValueRO.nextIndex].wayPoint, transform.ValueRO.Position) < 0.02f)
                    {
                        //nextIndex.ValueRW.nextIndex = (uint)((nextIndex.ValueRO.nextIndex + 1) % wayPoints.Length);
                        if (nextIndex.ValueRW.nextIndex < wayPoints.Length - 1)
                        {
                            nextIndex.ValueRW.nextIndex = (uint)(nextIndex.ValueRO.nextIndex + 1);
                        }
                    }
                }
            }
        }
    }
}