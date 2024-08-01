using Unity.Entities;

namespace Entity_Lesson12
{
    public struct GeneratorComponent : IComponentData
    {
        public Entity prefab;
        public int xCount;
        public int zCount;
    }
}