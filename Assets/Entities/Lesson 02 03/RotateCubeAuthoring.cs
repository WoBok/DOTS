using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

struct RotateSpeed : IComponentData
{
    public float rotateSpeed;
}
public class RotateCubeAuthoring : MonoBehaviour
{
    [Range(0, 360)]
    public float rotateSpeed = 180;

    public class Baker : Baker<RotateCubeAuthoring>
    {
        public override void Bake(RotateCubeAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);

            var data = new RotateSpeed
            {
                rotateSpeed = math.radians(authoring.rotateSpeed)
            };

            AddComponent(entity, data);
        }
    }
}