using Unity.Entities;

namespace Entity_Lesson08
{
    public struct GeneratorComponent : IComponentData
    {
        public Entity prefab;
        public int totalNum;
        public int perTickTimeNum;
        public float tickTime;
        public bool isUseParallel;
    }
}