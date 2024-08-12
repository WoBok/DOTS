using UnityEngine;

namespace Entities_Graphics.MeshDeformations
{
    public class BlendShapeDemo : MonoBehaviour
    {
        SkinnedMeshRenderer m_SkinnedMeshRenderer;
        void Start()
        {
            m_SkinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
        }

        void Update()
        {
            if (m_SkinnedMeshRenderer == null) return;
            if (m_SkinnedMeshRenderer.sharedMesh.blendShapeCount == 0) return;

            var weight0 = ((Mathf.Cos(Time.time) + 1) / 2) * 100;
            m_SkinnedMeshRenderer.SetBlendShapeWeight(0, weight0);
        }
    } 
}