using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;

partial struct UpdateEntitiesPositionSystem : ISystem
{
    BufferLookup<SpectrumDataComponent> m_SpectrumBufferLookup;
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        //m_SpectrumBufferLookup = state.GetBufferLookup<SpectrumDataComponent>(true);
        //foreach(var (entity,color ) in SystemAPI.Query<> )
    }
    //[BurstCompile]
    //public void OnUpdate(ref SystemState state)
    //{
    //    var entity = state.EntityManager.CreateEntity();
    //    var buffer = state.EntityManager.GetBuffer<SpectrumDataComponent>(entity);
    //    buffer.Clear();
    //    for (int i = 0; i < 1024; i++)
    //    {
    //        buffer.Add(new SpectrumDataComponent { value = (float)math.sin(SystemAPI.Time.ElapsedTime) });
    //    }

    //    //m_SpectrumBufferLookup.SetBufferEnabled(entity, true);
    //    //m_SpectrumBufferLookup.Update(ref state);

    //    var job = new UpdateEntitiesPositionJob
    //    {
    //        elapsTime = (float)SystemAPI.Time.ElapsedTime,
    //        spectrumBuffer = buffer
    //    };
    //    job.ScheduleParallel();
    //}
}