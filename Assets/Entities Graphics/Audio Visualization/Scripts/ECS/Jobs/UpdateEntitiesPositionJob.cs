using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public partial struct UpdateEntitiesPositionJob : IJobEntity
{
    public float elapsTime;
    [ReadOnly] public BufferLookup<SpectrumDataComponent> spectrumBufferLookup;
    public DynamicBuffer<SpectrumDataComponent> spectrumBuffer;
    public void Execute([ChunkIndexInQuery] int chunkIndex, ref LocalToWorld localToWorld)
    {
        var entityPosition = localToWorld.Position;
        var spectrum = spectrumBuffer[chunkIndex];
        var position = new float3(entityPosition.x, spectrum.value, entityPosition.z);
        localToWorld.Value = float4x4.Translate(position);
    }
}