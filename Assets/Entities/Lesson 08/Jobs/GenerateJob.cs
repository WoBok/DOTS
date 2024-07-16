using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;

public partial struct GenerateJob : IJobFor
{
    [ReadOnly] public Entity prefab;
    public NativeArray<Entity> entities;
    public EntityCommandBuffer ecb;
    public void Execute(int index)
    {
        entities[index] = ecb.Instantiate(prefab);
    }
}