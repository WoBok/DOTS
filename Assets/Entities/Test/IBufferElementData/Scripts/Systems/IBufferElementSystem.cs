using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public struct BufferElementComponent : IBufferElementData
{
    public float3 position;
    public float speed;
}
[UpdateInGroup(typeof(IBufferElementDataTestSystemGroup))]
partial struct IBufferElementSystem : ISystem
{
    Entity m_BufferElementEntity;
    public void OnCreate(ref SystemState state)
    {
        m_BufferElementEntity = state.EntityManager.CreateEntity();
        var buffer = state.EntityManager.AddBuffer<BufferElementComponent>(m_BufferElementEntity);
        buffer.Add(new BufferElementComponent { position = 0 });
        //Debug.Log($"Length: {buffer.Length}, Capacity: {buffer.Capacity}, DefaultBufferCapacityNumerator: {TypeManager.DefaultBufferCapacityNumerator}");
    }
}