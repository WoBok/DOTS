using Unity.Entities;
using UnityEngine;

namespace Entity_Lesson08
{
    public class Generator : Singleton<Generator>
    {
        public GameObject prefab;
        public int totalNum;
        public int perTickTimeNum;
        public float tickTime;
        public bool isUseParallel;
    }

    class GeneratorBaker : Baker<Generator>
    {
        public override void Bake(Generator authoring)
        {
            //if (authoring.isUseParallel)
            //{
            //    authoring.prefab.GetComponent<MeshRenderer>().material.SetColor("_BaseColor", Color.red);
            //}
            //else
            //{
            //    var renderer = authoring.prefab.GetComponent<MeshRenderer>();
            //    renderer.material.SetColor("_BaseColor", Color.green);
            //}

            var entity = GetEntity(TransformUsageFlags.None);

            var data = new GeneratorComponent()
            {
                prefab = GetEntity(authoring.prefab, TransformUsageFlags.Dynamic),
                totalNum = authoring.totalNum,
                perTickTimeNum = authoring.perTickTimeNum,
                tickTime = authoring.tickTime,
                isUseParallel = authoring.isUseParallel
            };

            AddComponent(entity, data);
        }
    }
}