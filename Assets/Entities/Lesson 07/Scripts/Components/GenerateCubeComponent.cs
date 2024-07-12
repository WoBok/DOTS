using Unity.Entities;

public struct GenerateCubeComponent : IComponentData
{
    public Entity prefab;
    public int xCount;
    public int zCount;
}