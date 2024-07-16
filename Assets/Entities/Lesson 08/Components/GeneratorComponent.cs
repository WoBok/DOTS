using Unity.Entities;

public struct GeneratorComponent : IComponentData
{
    public Entity prefab;
    public int totalNum;
    public int perTickTimeNum;
    public int tickTime;
    public bool isUseParallel;
}