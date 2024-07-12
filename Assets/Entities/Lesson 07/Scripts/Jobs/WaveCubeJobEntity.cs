using Unity.Burst;
using Unity.Collections;
using Unity.Entities;

[BurstCompile]
public partial struct WaveCubeJobEntity : IJobEntity
{
    [ReadOnly] public float elapsedTime;
    //public void Execute(ref LocalTransform transform, WaveCubeTagComponent tag)
    //{
    //    var sqrDistance = math.length(transform.Position);
    //    transform.Position += new float3(0, 0.1f, 0) * math.sin(elapsedTime * 3 + sqrDistance * 0.1f);
    //}
    public void Execute(WaveCubeAspect aspect)
    {
        aspect.WaveCubes(elapsedTime);
    }
}