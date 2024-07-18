using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Entity_Lesson09
{
    [RequireMatchingQueriesForUpdate]
    [UpdateInGroup(typeof(Lesson09SystemGroup))]
    [UpdateAfter(typeof(MovementUsingWayPointsSystem))]
    partial struct AddWayPointInputSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<WayPointsComponent>();
        }

        public void OnUpdate(ref SystemState state)
        {
            if (Input.GetMouseButtonDown(0))
            {
                DynamicBuffer<WayPointsComponent> wayPoints = SystemAPI.GetSingletonBuffer<WayPointsComponent>();
                if (!wayPoints.IsEmpty)
                {
                    //var minDistance = float.MaxValue;
                    //var index = 0;

                    float3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    worldPosition.z = 0;
                    //for (int i = 0; i < wayPoints.Length; i++)
                    //{
                    //    var distance = math.distance(worldPosition, wayPoints[i].wayPoint);
                    //    if (distance < minDistance)
                    //    {
                    //        minDistance = distance;
                    //        index = i;
                    //    }
                    //}
                    //wayPoints.Insert(index + 1, new WayPointsComponent { wayPoint = worldPosition });
                    wayPoints.Add(new WayPointsComponent { wayPoint = worldPosition });
                }
            }
        }
    }
}