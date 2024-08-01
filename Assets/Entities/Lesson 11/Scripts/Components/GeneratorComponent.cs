using Unity.Entities;
using Unity.Mathematics;

namespace Entity_Lesson11
{
    public struct GeneratorComponent : IComponentData
    {
        public Entity redCubePrefab;
        public Entity greenCubePrefab;
        public Entity blueCubePrefab;
        public int totalNum;
        public float tickTime;
        public float3 redCubeGeneratorPos;
        public float3 greenCubeGeneratorPos;
        public float3 blueCubeGeneratorPos;
        public float3 cubeTargetPos;
    } 
}