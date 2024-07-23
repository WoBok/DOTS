using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Entity_Lesson10
{
     readonly partial struct MarchingAspect : IAspect
    {
        readonly RefRW<LocalTransform> transform;
        readonly RefRO<MovementComponent> movementComponent;
        readonly RefRO<RandomTargetComponent> randomTargetComponent;

        public bool IsNeedDestroy()
        {
            return math.distance(transform.ValueRO.Position, randomTargetComponent.ValueRO.targetPosition) <= 0.02f ? true : false;
        }
        public void Move(float deltaTime)
        {
            var direction = randomTargetComponent.ValueRO.targetPosition - transform.ValueRO.Position;
            direction = math.normalize(direction);
            transform.ValueRW.Position += direction * movementComponent.ValueRO.movementSpeed * deltaTime;
        }
    }
}