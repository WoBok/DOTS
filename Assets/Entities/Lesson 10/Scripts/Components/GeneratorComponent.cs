using Unity.Entities;
using Unity.Mathematics;

namespace Entity_Lesson10
{
    public struct GeneratorComponent : IComponentData
    {
        public Entity prefab;
        public int count;
        public int countOfTickTime;
        public float tickTime;
        public float3 generationPosition;
        public float3 generationRange;
        public float3 targetPosition;
        public float3 targetRange;
        public float rotationSpeed;
        public float movementSpeed;
    }
}