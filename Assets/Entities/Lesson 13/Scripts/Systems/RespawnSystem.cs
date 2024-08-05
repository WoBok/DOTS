using Unity.Entities;
using Unity.Scenes;
using UnityEngine;

namespace Entity_Lesson13
{
    [RequireMatchingQueriesForUpdate]
    [UpdateInGroup(typeof(Lesson13SystemGroup))]
    partial struct RespawnSystem : ISystem, ISystemStartStop
    {
        int index;
        float timer;
        Entity controllerEntity;
        Entity instanceEntity;
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<RespawnTimerComponent>();
            Debug.Log("OnCreate");
        }
        public void OnUpdate(ref SystemState state)
        {
            if (!controllerEntity.Equals(default))
            {
                if (state.EntityManager.HasComponent<PrefabLoadResult>(controllerEntity))
                {
                    if (!instanceEntity.Equals(default))
                        state.EntityManager.DestroyEntity(instanceEntity);
                    var data = state.EntityManager.GetComponentData<PrefabLoadResult>(controllerEntity);
                    instanceEntity = state.EntityManager.Instantiate(data.PrefabRoot);
                    state.EntityManager.DestroyEntity(controllerEntity);
                    timer = 0;
                }

                timer += SystemAPI.Time.DeltaTime;
                var timeSpan = SystemAPI.GetSingleton<RespawnTimerComponent>().timeSpan;
                if (timer >= timeSpan)
                {
                    var prefabBuffer = SystemAPI.GetSingletonBuffer<PrefabBufferComponent>();
                    state.EntityManager.AddComponentData(controllerEntity, new RequestEntityPrefabLoaded
                    {
                        Prefab = prefabBuffer[index % prefabBuffer.Length].prefabReference
                    });
                    index++;
                    timer = 0;
                }
            }
            Debug.Log("OnUpdate");
        }
        public void OnDestroy(ref SystemState state)
        {
            Debug.Log("OnDestroy");
        }
        public void OnStartRunning(ref SystemState state)
        {
            controllerEntity = default;
            instanceEntity = default;
            controllerEntity = state.EntityManager.CreateEntity();
            var prefabBuffer = SystemAPI.GetSingletonBuffer<PrefabBufferComponent>();
            state.EntityManager.AddComponentData(controllerEntity, new RequestEntityPrefabLoaded
            {
                Prefab = prefabBuffer[0].prefabReference
            });
            state.EntityManager.AddComponent<RespawnCleanupComponent>(controllerEntity);
            index = 1;
            Debug.Log("OnStartRunning");
        }
        public void OnStopRunning(ref SystemState state)
        {
            if (!instanceEntity.Equals(default))
            {
                state.EntityManager.DestroyEntity(instanceEntity);
                instanceEntity = default;
            }
            if (!controllerEntity.Equals(default))
            {
                state.EntityManager.DestroyEntity(controllerEntity);
                index = 0;
                timer = 0;
                controllerEntity = default;
            }
            Debug.Log("OnStopRunning");
        }
    }
}