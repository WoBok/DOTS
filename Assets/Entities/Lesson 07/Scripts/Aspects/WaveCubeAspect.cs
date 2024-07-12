using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public readonly partial struct WaveCubeAspect : IAspect
{
    readonly RefRW<LocalTransform> transform;
    readonly RefRO<WaveCubeTagComponent> tag;
    public void WaveCubes(float elapsedTime)
    {
        var sqrDistance = math.length(transform.ValueRO.Position);
        transform.ValueRW.Position += new float3(0, 1f, 0) * math.sin(elapsedTime * 3 + sqrDistance * 0.1f);
    }
}