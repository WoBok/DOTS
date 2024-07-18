using Entity_Lesson09;
using Unity.Entities;
using UnityEngine;

class NextWayPoint : MonoBehaviour
{
    [HideInInspector] public uint nextIndex;
}

class NextWayPointBaker : Baker<NextWayPoint>
{
    public override void Bake(NextWayPoint authoring)
    {
        var entity = GetEntity(TransformUsageFlags.None);
        var data = new NextWayPointComponent
        {
            nextIndex = authoring.nextIndex
        };
        AddComponent(entity, data);
    }
}