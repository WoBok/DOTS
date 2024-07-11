using Unity.Entities;

public struct MoveAndRotateComponent : IComponentData
{
    public float moveSpeed;
    public float rotateSpeed;
}