using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;

namespace Entity_Lesson08
{
    public partial struct GeneratorJob : IJobFor
    {
        [ReadOnly] public Entity prefab;
        public NativeArray<Entity> entities;
        public EntityCommandBuffer ecb;
        [NativeDisableUnsafePtrRestriction] public RefRW<RandomComponent> random;
        public void Execute(int index)
        {
            entities[index] = ecb.Instantiate(prefab);
            ecb.AddComponent(entities[index], new MoveAndRotateComponent
            {
                rotateSpeed = math.radians(60),
                moveSpeed = 5
            });
            var targetPos = random.ValueRW.random.NextFloat2(new float2(-15, -15), new float2(15, 15));
            ecb.AddComponent(entities[index], new RandomTargetComponent
            {
                targetPosition = new float3(targetPos.x, 0, targetPos.y)
            });
        }
    }
}