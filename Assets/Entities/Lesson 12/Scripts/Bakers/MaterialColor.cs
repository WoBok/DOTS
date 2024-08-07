using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Entity_Lesson12
{
    class MaterialColor : MonoBehaviour
    {
        public Color color;
    }

    class MaterialColorBaker : Baker<MaterialColor>
    {
        public override void Bake(MaterialColor authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);
            var componentColor = new float4(authoring.color.r, authoring.color.g, authoring.color.b, authoring.color.a);
            var component = new MaterialColorComponent
            {
                color = componentColor
            };
            AddComponent(entity, component);
        }
    }
}