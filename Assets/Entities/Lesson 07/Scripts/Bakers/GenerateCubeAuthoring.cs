using Unity.Entities;
using UnityEngine;

class GenerateCubeAuthoring : Singleton<GenerateCubeAuthoring>
{
    public GameObject prefab;
    public int xCount;
    public int zCount;
}

class GenerateCubeAuthoringBaker : Baker<GenerateCubeAuthoring>
{
    public override void Bake(GenerateCubeAuthoring authoring)
    {
        var entity = GetEntity(TransformUsageFlags.None);
        var data = new GenerateCubeComponent()
        {
            prefab = GetEntity(authoring.prefab, TransformUsageFlags.Dynamic),
            xCount = authoring.xCount,
            zCount = authoring.zCount
        };
        AddComponent(entity, data);
    }
}