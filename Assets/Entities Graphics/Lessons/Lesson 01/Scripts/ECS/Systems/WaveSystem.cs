using Unity.Burst;
using Unity.Entities;

namespace Entities_Graphics.Lesson01
{
    [UpdateInGroup(typeof(EntitiesGraphicsLesson01SystemGroup))]
    partial struct WaveSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            //var job = new WaveJob { elapsedTime = (float)SystemAPI.Time.ElapsedTime };
            //job.ScheduleParallel();
        }
    }
}