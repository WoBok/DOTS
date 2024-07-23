using Unity.Entities;

namespace Entity_Lesson10
{
    partial struct MarchingJob : IJobEntity
    {
        public float deltaTime;
        public EntityCommandBuffer.ParallelWriter parallelWriter;
        void Execute([ChunkIndexInQuery] int index, Entity entity, MarchingAspect aspect)
        {
            if (aspect.IsNeedDestroy())
            {
                parallelWriter.DestroyEntity(index, entity);
            }
            else
            {
                aspect.Move(deltaTime);
            }
        }
    }
}