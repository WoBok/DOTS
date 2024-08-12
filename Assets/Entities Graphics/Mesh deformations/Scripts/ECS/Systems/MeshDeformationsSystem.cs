using Unity.Deformations;
using Unity.Entities;
using Unity.Mathematics;

namespace Entities_Graphics.MeshDeformations
{
    [RequireMatchingQueriesForUpdate]
    [UpdateInGroup(typeof(MeshDeformationsSystemGroup))]
    partial class MeshDeformationsSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            var elapsedTime = SystemAPI.Time.ElapsedTime;
            Entities.ForEach((ref DynamicBuffer<BlendShapeWeight> BlendWeights, in BlendShapesSpeedComponent data) =>
            {
                for (int i = 0; i < BlendWeights.Length; i++)
                {
                    BlendWeights[i] = new BlendShapeWeight { Value = (float)((math.cos(elapsedTime * data.speed) + 1) / 2) * 100 };
                }
            }).ScheduleParallel();
        }
    }
}