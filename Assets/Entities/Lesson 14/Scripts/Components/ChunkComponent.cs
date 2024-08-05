using Unity.Entities;

namespace Entity_Lesson14
{

    public struct ChunkComponentA : IComponentData
    {
        public int numA;
    }
    public struct ChunkComponentB : IComponentData
    {
        public int numB;
    }
    public struct ChunkComponentAB : IComponentData
    {
        public int numA;
        public int numB;
    }
}