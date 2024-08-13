using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;

public partial struct WaveJob : IJobEntity
{
    [ReadOnly] public float elapsedTime;
    public void Execute(ref LocalToWorld localToWorld, ref MaterialColor materialColor, ref MaterialMeshInfo materialMeshInfo)
    {
        var distance = math.length(localToWorld.Position);
        var sinValue = math.sin(distance * 0.1f + elapsedTime * 3);
        var position = localToWorld.Position + new float3(0, sinValue, 0);
        localToWorld.Value = float4x4.Translate(position);
        var sinValue0To1 = (sinValue + 1) / 2;
        var color = Color.HSVToRGB(sinValue0To1, 1, 1);
        materialColor.Value = new float4(color.r, color.g, color.b, 1);
        materialMeshInfo = MaterialMeshInfo.FromRenderMeshArrayIndices((int)(sinValue0To1 * 3), (int)(sinValue0To1 * 3));
    }
}