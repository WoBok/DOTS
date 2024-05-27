using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

public class AddJobBehaviour : MonoBehaviour
{
    public struct AddJobs : IJob
    {
        public float a;
        public float b;
        public NativeArray<float> result;
        public void Execute()
        {
            result[0] = a + b;
        }
    }

    JobHandle jobHandle;
    NativeArray<float> result;
    void Start()
    {
        result = new NativeArray<float>(1, Allocator.Persistent);
        var job = new AddJobs { a = 1, b = 2, result = result };
        jobHandle = job.Schedule();
        jobHandle.Complete();
        Debug.Log(result[0]);
        Debug.Log("123");
    }
    void Update()
    {
        if (jobHandle != null)
        {
            if (jobHandle.IsCompleted)
            {
                jobHandle.Complete();
                Debug.Log(result[0]);
            }
        }
    }
    void OnDisable()
    {
        if (result.IsCreated)
        {
            result.Dispose();
        }
    }
}