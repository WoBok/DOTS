using Unity.Entities;

namespace Entity_Lesson10
{
    public struct MovementComponent : IComponentData, IEnableableComponent
    {
        public float movementSpeed;
    }
}