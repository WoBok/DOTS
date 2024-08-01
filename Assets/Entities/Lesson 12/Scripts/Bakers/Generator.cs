using Unity.Entities;
using UnityEngine;

namespace Entity_Lesson12
{
    class Generator : MonoBehaviour
    {
        public GameObject prefab;
        public int xCount;
        public int zCount;
    }

    class GeneratorBaker : Baker<Generator>
    {
        public override void Bake(Generator authoring)
        {
            var meshRender = authoring.prefab.transform.GetComponentsInChildren<MeshRenderer>();
            foreach (var mesh in meshRender)
            {
                mesh.sharedMaterial.color = new Color(Random.value, Random.value, Random.value);
            }
            var entity = GetEntity(TransformUsageFlags.None);
            var component = new GeneratorComponent
            {
                prefab = GetEntity(authoring.prefab, TransformUsageFlags.None),
                xCount = authoring.xCount,
                zCount = authoring.zCount
            };
            AddComponent(entity, component);
        }
    }
}