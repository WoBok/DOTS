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
            //TransformUsageFlags���Լ��ٲ���Ҫ����������糡���е�һЩ�������������й����ж����ᷢ��λ�õĸı䣬������LocalToWorld��LocalTransfor��Parent�����������Ҫ���Դ˼������ݵĴ���
            //Dynamic��Indicates that an entity requires the necessary transform components to be moved at runtime(LocalTransform, LocalToWorld).
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