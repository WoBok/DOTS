using Unity.Entities;
using Unity.Entities.Serialization;

namespace Entity_Lesson13
{
    public struct PrefabBufferComponent : IBufferElementData
    {
        public EntityPrefabReference prefabReference;
    }
}