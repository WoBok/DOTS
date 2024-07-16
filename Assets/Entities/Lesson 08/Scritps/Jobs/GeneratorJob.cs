using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;

namespace Entity_Lesson07
{
    public partial struct GeneratorJob : IJobFor
    {
        [ReadOnly] public Entity prefab;
        public NativeArray<Entity> entities;
        public EntityCommandBuffer ecb;
        public void Execute(int index)
        {
            entities[index] = ecb.Instantiate(prefab);
        }
    } 
}