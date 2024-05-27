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
    //�����������ع�һ��Job���ص㣺
    //��Ҫ������struct
    //struct�е����ݱ�����blittable�Ļ�����NativeContainer
    //Ҫʵ��IJob�ӿ�
    //��Щ����������ʵ����Ϊ��һ��Ŀ�ģ�����Ҫ��C#�е�Job���ݸ��Ƶ�native�㣬
    //������native job systemȥִ��job�е��߼����뵽����ʵ���ǵĴ�Ҳ���Զ��׼��ˣ�
    //Execute()�������޸ĵ���ʵֻ������CounterJob��һ��������������ԭʼ��CounterJob��
    //��˵�������Ҫ��Job�л�ü�������ʱ��������Ҫʹ��NativeContainer�������õ�
    //����ȷ�Ľ����
}
