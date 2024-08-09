namespace UnityEngine.Rendering.UnifiedRayTracing
{
    internal class HardwareRayTracingBackend : IRayTracingBackend
    {
        public HardwareRayTracingBackend(RayTracingResources resources)
        {
            m_Resources = resources;
        }

        public IRayTracingShader CreateRayTracingShader(Object shader, string kernelName, GraphicsBuffer dispatchBuffer)
        {
            Debug.Assert(shader is RayTracingShader);
            return new HardwareRayTracingShader((RayTracingShader)shader, kernelName, dispatchBuffer);
        }

        public IRayTracingAccelStruct CreateAccelerationStructure(AccelerationStructureOptions options, ReferenceCounter counter)
        {
            return new HardwareRayTracingAccelStruct(options, m_Resources.hardwareRayTracingMaterial, counter, options.enableCompaction);
        }

        RayTracingResources m_Resources;
    }
}
