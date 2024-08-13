using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Rendering;
using UnityEngine;
using UnityEngine.Rendering;

namespace AudioVisualization
{
    public class CreateVisualizationEntities : MonoBehaviour
    {
        public int xCount;
        public int zCount;
        public Material[] materials;
        public Mesh[] meshes;
        void Start()
        {
            CreateEntities();
        }

        void CreateEntities()
        {
            var world = World.DefaultGameObjectInjectionWorld;
            var entityManager = world.EntityManager;

            var prototype = entityManager.CreateEntity();

            var renderMeshDescription = new RenderMeshDescription(shadowCastingMode: ShadowCastingMode.Off,
                receiveShadows: false);

            var renderMeshArray = new RenderMeshArray(materials, meshes);

            RenderMeshUtility.AddComponents(
                prototype,
                entityManager,
                renderMeshDescription,
                renderMeshArray,
                MaterialMeshInfo.FromRenderMeshArrayIndices(0, 0)
                );

            entityManager.AddComponentData(prototype, new MaterialColor());

            var ecb = new EntityCommandBuffer(Allocator.TempJob);

            var createVisualizationEntitiesJob = new CreateVisualizationEntitiesJob
            {
                prototype = prototype,
                ecb = ecb.AsParallelWriter(),
                xCount = xCount,
                zCount = zCount
            };

            var jobHandle = createVisualizationEntitiesJob.Schedule(xCount * zCount, 128);
            jobHandle.Complete();

            ecb.Playback(entityManager);
            ecb.Dispose();
            entityManager.DestroyEntity(prototype);
        }
    }
}