using Unity.Entities;
using UnityEngine;

namespace Entity_Lesson10
{
    class Generator : MonoBehaviour
    {
        public GameObject prefab;
        public int count;
        public int countOfTickTime;
        public float tickTime;
        public Vector3 generationPosition;
        public Vector3 generationRange;
        public Vector3 targetPosition;
        public Vector3 targetRange;
        public float rotationSpeed;
        public float movementSpeed;
    }

    class GeneratorBaker : Baker<Generator>
    {
        public override void Bake(Generator authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);
            var data = new GeneratorComponent
            {
                prefab = GetEntity(authoring.prefab, TransformUsageFlags.Dynamic),
                count = authoring.count,
                countOfTickTime = authoring.countOfTickTime,
                tickTime = authoring.tickTime,
                generationPosition = authoring.generationPosition,
                generationRange = authoring.generationRange,
                targetPosition = authoring.targetPosition,
                targetRange = authoring.targetRange,
                rotationSpeed = authoring.rotationSpeed,
                movementSpeed = authoring.movementSpeed
            };
            AddComponent(entity, data);
        }
    }
}