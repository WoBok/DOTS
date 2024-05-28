using Unity.Entities;
using UnityEngine;

struct BlueCubeTag : IComponentData
{

}
public class BlueCubeTagAuthoring : MonoBehaviour
{
    public class Baker : Baker<BlueCubeTagAuthoring>
    {
        public override void Bake(BlueCubeTagAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);
            var blueCubeTag = new BlueCubeTag();
            AddComponent(entity, blueCubeTag);
        }
    }
}