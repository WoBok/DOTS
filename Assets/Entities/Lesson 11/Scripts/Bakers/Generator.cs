using Unity.Entities;
using UnityEngine;

namespace Entity_Lesson11
{
    class Generator : MonoBehaviour
    {
        public GameObject redCubePrefab;
        public GameObject greenCubePrefab;
        public GameObject blueCubePrefab;
        public int totalNum;
        public float tickTime;
        public Vector3 redCubeGeneratorPos;
        public Vector3 greenCubeGeneratorPos;
        public Vector3 blueCubeGeneratorPos;
        public Vector3 cubeTargetPos;
    }

    class GeneratorBaker : Baker<Generator>
    {
        public override void Bake(Generator authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);
            var data = new GeneratorComponent
            {
                redCubePrefab = GetEntity(authoring.redCubePrefab, TransformUsageFlags.Dynamic),
                greenCubePrefab = GetEntity(authoring.greenCubePrefab, TransformUsageFlags.Dynamic),
                blueCubePrefab = GetEntity(authoring.blueCubePrefab, TransformUsageFlags.Dynamic),
                totalNum = authoring.totalNum,
                tickTime = authoring.tickTime,
                redCubeGeneratorPos = authoring.redCubeGeneratorPos,
                greenCubeGeneratorPos = authoring.greenCubeGeneratorPos,
                blueCubeGeneratorPos = authoring.blueCubeGeneratorPos,
                cubeTargetPos = authoring.cubeTargetPos
            };
            AddComponent(entity, data);
        }
    }
}