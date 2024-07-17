using Unity.Entities;
using Unity.Mathematics;

public struct RandomComponent : IComponentData
{
    public Random random;
}
public class RandomSingleton : Singleton<RandomSingleton>
{
    public uint seed = 1;

}
class Baker : Baker<RandomSingleton>
{
    public override void Bake(RandomSingleton authoring)
    {
        var entity = GetEntity(TransformUsageFlags.None);
        var data = new RandomComponent
        {
            random = new Random(authoring.seed)
        };
        AddComponent(entity, data);
    }
}