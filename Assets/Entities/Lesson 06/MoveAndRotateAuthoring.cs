using Unity.Entities;
using UnityEngine;

class MoveAndRotateAuthoring : MonoBehaviour
{
    public float moveSpeed;
    public float rotateSpeed;
}

class MoveAndRotateAuthoringBaker : Baker<MoveAndRotateAuthoring>
{
    public override void Bake(MoveAndRotateAuthoring authoring)
    {
        var entity = GetEntity(TransformUsageFlags.Dynamic);
        var data = new MoveAndRotateComponent
        {
            moveSpeed = authoring.moveSpeed,
            rotateSpeed = authoring.rotateSpeed
        };
        AddComponent(entity, data);
    }
}