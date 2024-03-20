using UnityEngine;
using UnityEngine.Jobs;

namespace Jobs.DOD
{
    public class DOD : MonoBehaviour
    {
        struct WaveCubeJob : IJobParallelForTransform
        {
            public void Execute(int index, TransformAccess transform)
            {
                throw new System.NotImplementedException();
            }
        }
    }
}