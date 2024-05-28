using Unity.Entities;
using UnityEngine;

struct GreenCubeTag : IComponentData
{

}
public class GreenCubeTagAuthoring : MonoBehaviour
{
    public class Baker : Baker<GreenCubeTagAuthoring>
    {
        public override void Bake(GreenCubeTagAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);
            var greenCubeTag = new GreenCubeTag();
            AddComponent(entity, greenCubeTag);
        }
    }
}