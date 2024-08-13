using Unity.Entities;
using Unity.Mathematics;

namespace Unity.Rendering
{
    [MaterialProperty("_Intensity")]
    struct IntensityFloatOverride : IComponentData
    {
        public float Value;
    }
}
