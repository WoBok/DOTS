using Unity.Entities;
using Unity.Entities.Serialization;
using UnityEngine;

namespace Entity_Lesson13
{
    class RespawnController : MonoBehaviour
    {
        public GameObject[] prefabs;
        public float timeSpan;
    }

    class RespawnControllerBaker : Baker<RespawnController>
    {
        public override void Bake(RespawnController authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);
            var data = new RespawnTimerComponent { timeSpan = authoring.timeSpan };
            AddComponent(entity, data);
            var prefabBuffer = AddBuffer<PrefabBufferComponent>(entity);
            for (int i = 0; authoring.prefabs.Length > i; i++)
            {
                var prefabBufferElem = new PrefabBufferComponent
                {
                    prefabReference = new EntityPrefabReference(authoring.prefabs[i]),
                };
                prefabBuffer.Add(prefabBufferElem);
            }
        }
    }
}