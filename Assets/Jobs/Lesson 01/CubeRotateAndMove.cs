using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Profiling;
using UnityEngine;
using UnityEngine.Jobs;

public class CubeRotateAndMove : MonoBehaviour
{
    [BurstCompile]
    struct MoveAndRotateCube : IJobParallelForTransform
    {
        [ReadOnly] public float deltaTime;
        [ReadOnly] public float time;
        [ReadOnly] public float x;
        [ReadOnly] public float rotateSpeed;
        [ReadOnly] public float moveSpeed;
        public void Execute(int index, TransformAccess transform)
        {
            //transform.rotation = Quaternion.AngleAxis(transform.rotation.eulerAngles.y + rotateSpeed * (time % 360) * deltaTime, Vector3.up);
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + Vector3.up * rotateSpeed * deltaTime);
            transform.position += Vector3.right * deltaTime * moveSpeed;
            if (transform.position.x > x)
                transform.position -= Vector3.right * x * 2;
        }
    }

    public int sum = 500;
    public float rotateSpeed;
    public float moveSpeed;

    static readonly ProfilerMarker<int> profilerMarker = new ProfilerMarker<int>("Cube", "Object Count");
    Vector3 boundary = new Vector3(20, 1, 10);
    TransformAccessArray m_TransformAccessArray;
    JobHandle jobHandle;

    void Start()
    {
        var boxCollider = GetComponent<BoxCollider>();
        if (boxCollider != null)
            boundary = boxCollider.size;

        m_TransformAccessArray = new TransformAccessArray(4 * sum);

        for (int i = 0; i < sum; i++)
        {
            var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            var material = new Material(Shader.Find("Universal Render Pipeline/Simple Lit"));
            material.color = Color.blue;
            cube.GetComponent<MeshRenderer>().material = material;
            cube.transform.localEulerAngles = Vector3.up * Random.value * 360;
            cube.transform.localPosition = new Vector3(Random.Range(-boundary.x, boundary.x), Random.Range(-boundary.y, boundary.y), Random.Range(-boundary.z, boundary.z));
            m_TransformAccessArray.Add(cube.transform);
        }
    }

    void Update()
    {
        using (profilerMarker.Auto(m_TransformAccessArray.length))
        {
            jobHandle.Complete();

            var waveJob = new MoveAndRotateCube
            {
                deltaTime = Time.deltaTime,
                time = Time.time,
                x = boundary.x,
                rotateSpeed = rotateSpeed,
                moveSpeed = moveSpeed
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