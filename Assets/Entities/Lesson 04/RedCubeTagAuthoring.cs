using Unity.Entities;
using UnityEngine;

struct RedCubeTag : IComponentData
{

}
public class RedCubeTagAuthoring : MonoBehaviour
{
    public class Baker : Baker<RedCubeTagAuthoring>
    {
        public override void Bake(RedCubeTagAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);
            var redCubeTag = new RedCubeTag();
            AddComponent(entity, redCubeTag);
        }
    }
}