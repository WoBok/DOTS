using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

namespace AudioVisualization
{
    public partial struct CreateVisualizationEntitiesJob : IJobParallelFor
    {
        public Entity prototype;
        public EntityCommandBuffer.ParallelWriter ecb;
        public int xCount;
        public int zCount;

        public void Execute(int index)
        {
            var entity = ecb.Instantiate(index, prototype);
            ecb.SetComponent(index, entity, new LocalToWorld { Value = ComputeTransform(index) });
        }

        float4x4 ComputeTransform(int index)
        {
            var x = index / xCount - xCount / 2;
            var z = index % zCount - zCount / 2;

            return float4x4.Translate(new float3(x * 1.1f, 0, z * 1.1f));
        }
    }
}