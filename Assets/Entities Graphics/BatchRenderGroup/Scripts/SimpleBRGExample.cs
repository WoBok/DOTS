using System;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Rendering;

namespace Entities_Graphics.BatchRrenderGroup
{
    public class SimpleBRGExample : MonoBehaviour
    {
        BatchRendererGroup m_BRG;
        void Start()
        {
            m_BRG = new BatchRendererGroup(OnPerformCulling, IntPtr.Zero);
        }
        void OnDisable()
        {
            m_BRG.Dispose();
        }
        public unsafe JobHandle OnPerformCulling(
            BatchRendererGroup rendererGroup,
            BatchCullingContext cullingContext,
            BatchCullingOutput cullingOutput,
            IntPtr userContext)
        {
            // This example doesn't use jobs, so it can return an empty JobHandle.
            // Performance-sensitive applications should use Burst jobs to implement
            // culling and draw command output. In this case, this function would return a
            // handle here that completes when the Burst jobs finish.
            return new JobHandle();
        }
    }
}