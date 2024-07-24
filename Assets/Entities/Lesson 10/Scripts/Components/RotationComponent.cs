using Unity.Entities;

namespace Entity_Lesson10
{
    public struct RotationComponent : IComponentData, IEnableableComponent
    {
        public float rotationSpeed;
    }
}