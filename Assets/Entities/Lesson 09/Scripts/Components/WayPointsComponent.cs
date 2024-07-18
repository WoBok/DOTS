using Unity.Entities;
using Unity.Mathematics;

namespace Entity_Lesson09
{
    [InternalBufferCapacity(8)]
    public struct WayPointsComponent : IBufferElementData
    {
        public float3 wayPoint;
    }
}