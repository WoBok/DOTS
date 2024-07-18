using System.Collections;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

public class CounterJobBehaviour : MonoBehaviour
{
    public struct CounterJob : IJob
    {
        public NativeArray<int> numbers;
        public NativeArray<int> result;
        public void Execute()
        {
            var temp = 0;
            for (int i = 0; i < numbers.Length; i++)
            {
                temp += numbers[i];
                Debug.Log(temp);
            }
            result[0] = temp;
        }
    }
    JobHandle job;
    void Start()
    {
        var numCount = 10;
        NativeArray<int> numbers = new NativeArray<int>(numCount, Allocator.TempJob);
        var result = new NativeArray<int>(1, Allocator.TempJob);

        for (int i = 0; i < numCount; i++)
        {
            numbers[i] = i + 1;
        }
        var jobData = new CounterJob
        {
            numbers = numbers,
            result = result
        };

        job = jobData.Schedule();
        job.Complete();
        Debug.Log(result[0]);
        Debug.Log("12345678");

        result.Dispose();
        numbers.Dispose();
    }

    IEnumerator CompleteJobs()
    {
        yield return new WaitForSeconds(1);
        Debug.Log(1);
        yield return new WaitForSeconds(1);
        Debug.Log(2);
        yield return new WaitForSeconds(1);
        Debug.Log(3);
        yield return new WaitForSeconds(1);
        Debug.Log(4);
        yield return new WaitForSeconds(1);
        Debug.Log(5);
        job.Complete();
    }
}