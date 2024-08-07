using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;

namespace Entity_Lesson12
{
    [MaterialProperty("_BaseColor")]
    public struct MaterialColorComponent : IComponentData
    {
        public float4 color;
    }
}