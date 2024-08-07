using Unity.Entities;
using Unity.Entities.Serialization;
using UnityEngine;

namespace MaterialOverrides
{
    class PrefabReference : Singleton<PrefabReference>
    {
        public GameObject prefab;
    }

    class PrefabReferenceBaker : Baker<PrefabReference>
    {
        public override void Bake(PrefabReference authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);
            var prefabReference = new EntityPrefabReference(authoring.prefab);
            var component = new PrefabReferenceComponent { prefabReference = prefabReference };
            AddComponent(entity, component);
        }
    }
}