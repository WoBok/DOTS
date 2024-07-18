using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace Entity_Lesson09
{
    class WayPoints : MonoBehaviour
    {
        public List<Vector3> wayPoints;

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (wayPoints != null)
                {
                    if (wayPoints.Count > 0)
                    {
                        //var minDistance = float.MaxValue;
                        //var index = 0;

                        var worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        worldPosition.z = 0;
                        //for (int i = 0; i < wayPoints.Count; i++)
                        //{
                        //    var distance = Vector3.Distance(worldPosition, wayPoints[i]);
                        //    if (distance < minDistance)
                        //    {
                        //        minDistance = distance;
                        //        index = i;
                        //    }
                        //}
                        //wayPoints.Insert(index + 1, worldPosition);
                        wayPoints.Add(worldPosition);
                    }
                }
            }
        }
        void OnDrawGizmos()
        {
            if (wayPoints != null)
            {
                if (wayPoints.Count >= 2)
                {
                    for (int i = 0; i < wayPoints.Count; i++)
                    {
                        if (i + 1 < wayPoints.Count)
                        {
                            Gizmos.color = Color.yellow;
                            Gizmos.DrawLine(wayPoints[i], wayPoints[(i + 1) % wayPoints.Count]);
                        }
                        Gizmos.color = Color.blue;
                        Gizmos.DrawSphere(wayPoints[i], 0.25f);
                    }
                }
            }
        }
    }

    class WayPointsBaker : Baker<WayPoints>
    {
        public override void Bake(WayPoints authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);
            DynamicBuffer<WayPointsComponent> wayPoints = AddBuffer<WayPointsComponent>(entity);
            wayPoints.Length = authoring.wayPoints.Count;
            for (int i = 0; i < authoring.wayPoints.Count; i++)
            {
                wayPoints[i] = new WayPointsComponent { wayPoint = authoring.wayPoints[i] };
            }
        }
    }
}