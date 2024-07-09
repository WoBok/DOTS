using Unity.Entities;
using UnityEngine;

struct GeneratePrefab : IComponentData
{
    public Entity entity;
    public int count;
}
public class GeneratePrefabAuthoring : Singleton<GeneratePrefabAuthoring>
{
    public GameObject prefab;
    public int count;

    class GeneratePrefabBaker : Baker<GeneratePrefabAuthoring>
    {
        public override void Bake(GeneratePrefabAuthoring authoring)
        {
            //TransformUsageFlags用以减少不必要的组件，比如场景中的一些物体在整个运行过程中都不会发生位置的改变，类似于LocalToWorld、LocalTransfor、Parent等组件都不需要，以此减少数据的传递
            //Dynamic：Indicates that an entity requires the necessary transform components to be moved at runtime(LocalTransform, LocalToWorld).
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            var generatePrefab = new GeneratePrefab()
            {
                entity = GetEntity(authoring.prefab, TransformUsageFlags.None),
                count = authoring.count
            };
            AddComponent(entity, generatePrefab);
        }
    }
}