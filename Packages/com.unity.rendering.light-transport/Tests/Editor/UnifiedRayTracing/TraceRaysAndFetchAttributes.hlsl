#include "Packages/com.unity.rendering.light-transport/Runtime/UnifiedRayTracing/FetchGeometry.hlsl"
#include "Packages/com.unity.rendering.light-transport/Runtime/UnifiedRayTracing/TraceRay.hlsl"

UNIFIED_RT_DECLARE_ACCEL_STRUCT(_AccelStruct);

struct RayWithFlags
{
    float3 origin;
    float tMin;
    float3 direction;
    float tMax;
    uint culling;
    uint instanceMask;
    uint padding;
    uint padding2;
};

StructuredBuffer<RayWithFlags> _Rays;
RWStructuredBuffer<UnifiedRT::Hit> _Hits;
RWStructuredBuffer<UnifiedRT::HitGeomAttributes> _HitAttributes;

void RayGenExecute(UnifiedRT::DispatchInfo dispatchInfo)
{

    RayWithFlags rayWithFlags = _Rays[dispatchInfo.globalThreadIndex];
    UnifiedRT::Ray ray;
    ray.origin = rayWithFlags.origin;
    ray.direction = rayWithFlags.direction;
    ray.tMin = rayWithFlags.tMin;
    ray.tMax = rayWithFlags.tMax;

    UnifiedRT::RayTracingAccelStruct accelStruct = UNIFIED_RT_GET_ACCEL_STRUCT(_AccelStruct);
    UnifiedRT::Hit hitResult = UnifiedRT::TraceRayClosestHit(dispatchInfo, accelStruct, rayWithFlags.instanceMask, ray, rayWithFlags.culling);
    if (hitResult.IsValid())
    {
        UnifiedRT::HitGeomAttributes hitAttribs = UnifiedRT::FetchHitGeomAttributes(hitResult);
        _HitAttributes[dispatchInfo.globalThreadIndex] = hitAttribs;
    }
    else
    {
        _HitAttributes[dispatchInfo.globalThreadIndex] = (UnifiedRT::HitGeomAttributes)0;
    }


    _Hits[dispatchInfo.globalThreadIndex] = hitResult;
}
