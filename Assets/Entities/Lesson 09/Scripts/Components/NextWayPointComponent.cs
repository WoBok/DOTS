using Unity.Entities;

namespace Entity_Lesson09
{
    public struct NextWayPointComponent : IComponentData
    {
        public uint nextIndex;
    }
}