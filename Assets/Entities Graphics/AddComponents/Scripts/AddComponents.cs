using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;
using UnityEngine.Rendering;

namespace Entities_Graphics.AddComponents
{
    [GenerateTestsForBurstCompatibility]
    public struct SpawnJob : IJobParallelFor
    {
        public Entity prototype;
        public int entityCount;
        public EntityCommandBuffer.ParallelWriter ecb;
        public int meshCount;
        public void Execute(int index)
        {
            var entity = ecb.Instantiate(index, prototype);
            ecb.SetComponent(index, entity, new LocalToWorld { Value = ComputeTransform(index) });
            ecb.SetComponent(index, entity, new MaterialColor() { Value = ComputeColor(index) });
            ecb.SetComponent(index, entity, MaterialMeshInfo.FromRenderMeshArrayIndices(0, index % meshCount));
        }

        float4x4 ComputeTransform(int index)
        {
            float radius = 10;
            int numberOfTurns = 20;
            float scale = 0.1f;

            var t = (float)index / (entityCount - 1);

            var hight = 2 * radius;
            var currentRadius = math.sin(t * math.PI) * radius;
            var phi = numberOfTurns * math.PI2 * t;

            var x = Mathf.Cos(phi) * currentRadius;
            var z = Mathf.Sin(phi) * currentRadius;
            var y = t * hight - radius;

            return float4x4.TRS(
                 new float3(x, y, z),
                 quaternion.identity,
                 new float3(scale)
                 );
        }
        float4 ComputeColor(int index)
        {
            var t = (float)index / (entityCount - 1);
            var color = Color.HSVToRGB(t, 1, 1);
            return new float4(color.r, color.g, color.b, 1);
        }
    }

    public class AddComponents : MonoBehaviour
    {
        public Mesh[] meshes;
        public Material material;
        public int entityCount;
        void Start()
        {
            var world = World.DefaultGameObjectInjectionWorld;
            var entityManager = world.EntityManager;

            var prototype = entityManager.CreateEntity();
            var description = new RenderMeshDescription(shadowCastingMode: ShadowCastingMode.Off,
                receiveShadows: false);//必须设置shadowCastingMode: ShadowCastingMode.Off, receiveShadows: false
            var renderMeshArray = new RenderMeshArray(new Material[] { material }, meshes);

            RenderMeshUtility.AddComponents(
                prototype,
                entityManager,
                description,
                renderMeshArray,
                MaterialMeshInfo.FromRenderMeshArrayIndices(0, 1)//更改下标，尝试查看变化
                );

            entityManager.AddComponentData(prototype, new MaterialColor());

            var ecb = new EntityCommandBuffer(Allocator.TempJob);

            var spawnJob = new SpawnJob
            {
                prototype = prototype,
                ecb = ecb.AsParallelWriter(),
                entityCount = entityCount,
                meshCount = meshes.Length
            };

            var spawnHandle = spawnJob.Schedule(entityCount, 128);
            spawnHandle.Complete();

            ecb.Playback(entityManager);
            ecb.Dispose();
            entityManager.DestroyEntity(prototype);
        }
    }
}