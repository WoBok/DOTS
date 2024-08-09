using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;

namespace MaterialOverrides
{
    [MaterialProperty("_ColorDOTS")]
    public struct MaterialColorComponent : IComponentData
    {
        public float4 color;
    }
}