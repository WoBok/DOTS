using Entities_Graphics.Lesson01;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Rendering;
using UnityEngine;
using UnityEngine.Rendering;

public class CreateEntity : MonoBehaviour
{
    public int xCount;
    public int zCount;

    public bool isUsingDisableRending;
    public float rendingRange;

    public Material[] materials;
    public Mesh[] meshes;
    void Start()
    {
        CerateEntityUsingAddComponents();
    }

    void CerateEntityUsingAddComponents()
    {
        var world = World.DefaultGameObjectInjectionWorld;
        var entityManager = world.EntityManager;

        var prototype = entityManager.CreateEntity();

        var description = new RenderMeshDescription(shadowCastingMode: ShadowCastingMode.Off, receiveShadows: false);

        var renderMeshArray = new RenderMeshArray(materials, meshes);

        RenderMeshUtility.AddComponents(
            prototype,
            entityManager,
            description,
            renderMeshArray,
            MaterialMeshInfo.FromRenderMeshArrayIndices(0, 0)
            );

        entityManager.AddComponentData(prototype, new MaterialColor());

        var ecb = new EntityCommandBuffer(Allocator.TempJob);

        var createInstanceJob = new CreateInstanceJob
        {
            prototype = prototype,
            ecb = ecb.AsParallelWriter(),
            xCount = xCount,
            zCount = zCount,
            isUsingDisableRending = isUsingDisableRending,
            rendingRange = rendingRange
        };

        var createInstanceJobHandle = createInstanceJob.Schedule(xCount * zCount, 128);
        createInstanceJobHandle.Complete();

        ecb.Playback(entityManager);
        ecb.Dispose();
        entityManager.DestroyEntity(prototype);
    }
}