using Unity.Entities;
using Unity.Entities.Serialization;

namespace MaterialOverrides
{
    public struct PrefabReferenceComponent : IComponentData
    {
        public EntityPrefabReference prefabReference;
    }
}