using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

public class CounterJobBehaviourT : MonoBehaviour
{
    public struct CounterJob : IJob
    {
        public NativeArray<int> numbers;
        public int result;
        public void Execute()
        {
            var temp = 0;
            for (int i = 0; i < numbers.Length; i++)
            {
                temp += numbers[i];
                Debug.Log(temp);
            }
            result = temp;
        }
    }

    void Start()
    {
        var numCount = 10;
        NativeArray<int> numbers = new NativeArray<int>(numCount, Allocator.TempJob);
        for (int i = 0; i < numCount; i++)
        {
            numbers[i] = i + 1;
        }
        var jobData = new CounterJob
        {
            numbers = numbers,
            result = 10
        };

        var handle = jobData.Schedule();
        handle.Complete();
        Debug.Log(jobData.result);
        numbers.Dispose();
    }
    //我们再来看回顾一下Job的特点：
    //需要声明成struct
    //struct中的数据必须是blittable的或者是NativeContainer
    //要实现IJob接口
    //这些限制条件其实都是为了一个目的，就是要把C#中的Job数据复制到native层，
    //最终由native job system去执行job中的逻辑。想到这其实我们的答案也就显而易见了，
    //Execute()方法中修改的其实只是我们CounterJob的一个副本，并不是原始的CounterJob。
    //因此当我们需要从Job中获得计算结果的时候，我们需要使用NativeContainer，否则会得到
    //不正确的结果。
}
