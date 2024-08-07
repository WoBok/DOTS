using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Entity_Lesson12
{
    [RequireMatchingQueriesForUpdate]
    [UpdateInGroup(typeof(Lesson12SystemGroup))]
    [UpdateAfter(typeof(GeneratorSystem))]
    partial struct ChangeColorSystem : ISystem
    {
        EntityQuery query;
        Random random;
        bool isChanged;
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            var queryBuilder = new EntityQueryBuilder(Allocator.Temp).WithAll<LocalTransform, MaterialColorComponent>();
            query = state.GetEntityQuery(queryBuilder);
            random = new Random(1);
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            //var entities = query.ToEntityArray(Allocator.Temp);
            //var materialColorComponents = query.ToComponentDataArray<MaterialColorComponent>(Allocator.Temp);
            //for (int i = 0; i < materialColorComponents.Length; i++)
            //{
            //    var materialColorComponent = materialColorComponents[i];
            //    materialColorComponent.color = new float4(random.NextFloat(), random.NextFloat(), random.NextFloat(), 1);
            //    //materialColorComponents[i] = materialColorComponent;

            //}
            //entities.Dispose();
            //materialColorComponents.Dispose();

            foreach (var mc in SystemAPI.Query<RefRW<MaterialColorComponent>>())
            {
                mc.ValueRW.color = new float4(random.NextFloat(), random.NextFloat(), random.NextFloat(), 1);
                isChanged = true;
            }
            if (isChanged)
            {
                state.Enabled = false;
            }
        }
    }
}