using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;
namespace Entities_Graphics.Lesson01
{
    public partial struct CreateInstanceJob : IJobParallelFor
    {
        public Entity prototype;
        public EntityCommandBuffer.ParallelWriter ecb;
        public int xCount;
        public int zCount;
        public bool isUsingDisableRending;
        public float rendingRange;
        public void Execute(int index)
        {
            var entity = ecb.Instantiate(index, prototype);
            ecb.SetComponent(index, entity, new LocalToWorld { Value = ComputeTransform(index, entity) });
            ecb.SetComponent(index, entity, new MaterialColor { Value = ComputeColor(index) });
        }

        float4x4 ComputeTransform(int index, Entity entity)
        {
            var x = index / xCount - xCount / 2;
            var z = index % zCount - zCount / 2;

            if (isUsingDisableRending)
            {
                if (math.sqrt(x * x + z * z) > rendingRange)
                    ecb.AddComponent<DisableRendering>(index, entity);
            }

            return float4x4.Translate(new float3(x * 1.1f, 0, z * 1.1f));
        }
        float4 ComputeColor(int index)
        {
            var t = (float)index / (xCount * zCount);
            var color = Color.HSVToRGB(t, t / 2 + 0.5f, t / 2 + 0.5f);
            return new float4(color.r, color.g, color.b, 1);
        }
    }
}