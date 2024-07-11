using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

readonly partial struct MoveAndRotateAspect : IAspect
{
    readonly RefRW<LocalTransform> localTransform;
    readonly RefRO<MoveAndRotateComponent> moveAndRotateComponent;

    public void Move(float elapsedTime)
    {
        localTransform.ValueRW.Position.y = math.sin(elapsedTime * moveAndRotateComponent.ValueRO.moveSpeed);
    }
    public void Rotate(float deltaTime)
    {
        localTransform.ValueRW = localTransform.ValueRW.RotateY(deltaTime * moveAndRotateComponent.ValueRO.rotateSpeed);
    }
    public void MoveAndRotate(float elapsedTime, float deltaTime)
    {
        Move(elapsedTime);
        Rotate(deltaTime);
    }
}