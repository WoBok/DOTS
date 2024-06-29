using Unity.Burst;
using Unity.Collections;
using Unity.Mathematics;
using Unity.Profiling;
using UnityEngine;
using UnityEngine.Jobs;

namespace Jobs.DOD
{
    public class DOD : MonoBehaviour
    {
        [BurstCompile]  
        struct WaveCubeJob : IJobParallelForTransform
        {
            [ReadOnly] public float elapsedTime;
            public void Execute(int index, TransformAccess transform)
            {
                var distance = math.length(transform.position);
                transform.localPosition += Vector3.up * math.sin(3 * elapsedTime + 0.2f * distance);
            }
        }

        static readonly ProfilerMarker<int> profilerMarker = new ProfilerMarker<int>("Wave", "Object Count");
        public int xHalfCount = 40;
        public int zHalfCount = 40;

        TransformAccessArray m_TransformAccessArray;

        Unity.Jobs.JobHandle jobHandle;

        void Start()
        {
            m_TransformAccessArray = new TransformAccessArray(4 * xHalfCount * zHalfCount);
            for (int i = -xHalfCount; i <= xHalfCount; i++)
            {
                for (int j = -zHalfCount; j <= zHalfCount; j++)
                {
                    var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cube.transform.position = new Vector3(i * 1.1f, 0, j * 1.1f);
                    var material = new Material(Shader.Find("Universal Render Pipeline/Lit"));
                    material.SetColor("_BaseColor", Color.yellow);
                    cube.GetComponent<MeshRenderer>().material = material;
                    m_TransformAccessArray.Add(cube.transform);
                }
            }
        }

        void Update()
        {
            using (profilerMarker.Auto(m_TransformAccessArray.length))
            {
                jobHandle.Complete();

                var waveJob = new WaveCubeJob
                {
                    elapsedTime = Time.time
                };
                jobHandle = waveJob.Schedule(m_TransformAccessArray);
            }
        }

        void OnDestroy()
        {
            if (m_TransformAccessArray.isCreated)
                m_TransformAccessArray.Dispose();
        }
    }
}