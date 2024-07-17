using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Entity_Lesson08
{
    [BurstCompile]
    public partial struct MoveAndRotateJob : IJobEntity
    {
        public float deltaTime;
        public EntityCommandBuffer.ParallelWriter parallelWriter;
        public void Execute([ChunkIndexInQuery] int chunkIndex, Entity entity, ref LocalTransform transform, in RandomTargetComponent target, in MoveAndRotateComponent speed)
        {
            var distance = math.distance(transform.Position, target.targetPosition);
            if (distance < 0.02f)
            {
                parallelWriter.DestroyEntity(chunkIndex, entity);
            }
            else
            {
                float3 dir = math.normalize(target.targetPosition - transform.Position);
                transform.Position += dir * speed.moveSpeed * deltaTime;
                transform = transform.RotateY(speed.rotateSpeed * deltaTime);
            }
        }
    }
}