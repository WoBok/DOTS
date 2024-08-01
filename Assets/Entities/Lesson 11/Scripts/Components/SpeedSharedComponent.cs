using Unity.Entities;

namespace Entity_Lesson11
{
    public struct SpeedSharedComponent : ISharedComponentData
    {
        public float movementSpeed;
        public float rotationSpeed;
    }
}