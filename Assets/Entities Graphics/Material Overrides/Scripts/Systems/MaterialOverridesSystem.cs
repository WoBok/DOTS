using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Scenes;

namespace MaterialOverrides
{
    [UpdateInGroup(typeof(MaterialOverridesSystemGroup))]
    partial struct MaterialOverridesSystem : ISystem, ISystemStartStop
    {
        bool isInstantiated;
        bool isAddedComponent;
        Entity prefabReferenceComponentEntity;
        Entity instance;
        Random random;
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<PrefabReferenceComponent>();
            random = new Random(1);
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            if (!isInstantiated)
            {
                foreach (var prefab in SystemAPI.Query<RefRO<PrefabLoadResult>>())
                {
                    instance = state.EntityManager.Instantiate(prefab.ValueRO.PrefabRoot);
                    isInstantiated = true;
                }
            }
            var elapsedTime = SystemAPI.Time.ElapsedTime;
            var color = new float4(
                (float)math.cos(elapsedTime * 2 + math.PI / 2),
                (float)math.cos(elapsedTime * 3 + math.PI / 3),
                (float)math.cos(elapsedTime * 4 + math.PI / 4),
                1
                );
            var data = new MaterialColorComponent
            {
                color = color
            };
            if (isInstantiated)
            {
                if (!isAddedComponent)
                {
                    state.EntityManager.AddComponent<MaterialColorComponent>(instance);
                }
                //state.EntityManager.SetComponentData(instance, data);
            }
            foreach (var mc in SystemAPI.Query<RefRW<MaterialColorComponent>>())
            {
                //mc.ValueRW = data;
                mc.ValueRW.color = color;
            }
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {

        }

        [BurstCompile]
        public void OnStartRunning(ref SystemState state)
        {
            prefabReferenceComponentEntity = state.EntityManager.CreateEntity();
            var prefabReference = SystemAPI.GetSingleton<PrefabReferenceComponent>();
            var comData = new RequestEntityPrefabLoaded { Prefab = prefabReference.prefabReference };
            state.EntityManager.AddComponentData(prefabReferenceComponentEntity, comData);
        }

        [BurstCompile]
        public void OnStopRunning(ref SystemState state)
        {

        }
    }
}