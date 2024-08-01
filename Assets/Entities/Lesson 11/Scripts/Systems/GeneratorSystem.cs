using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Entity_Lesson11
{
    [RequireMatchingQueriesForUpdate]
    [UpdateInGroup(typeof(Lesson11SystemGroup))]

    partial struct GeneratorSystem : ISystem
    {
        float timer;
        int totalCount;
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<GeneratorComponent>();
            timer = 0.0f;
            totalCount = 0;
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            //The EntityManager provides an API to create, read, update, and destroy entities.
            //A World has one EntityManager, which manages all the entities for that World.

            var generator = SystemAPI.GetSingleton<GeneratorComponent>();
            if (totalCount >= generator.totalNum)
            {
                state.Enabled = false;
                return;
            }
            if (timer >= generator.tickTime)
            {
                var redCube = state.EntityManager.Instantiate(generator.redCubePrefab);
                var greenCube = state.EntityManager.Instantiate(generator.greenCubePrefab);
                var blueCube = state.EntityManager.Instantiate(generator.blueCubePrefab);

                //添加Component时使用同一个对象和分别创建有什么区别？？？
                var speedSharedComponent = new SpeedSharedComponent
                {
                    rotationSpeed = math.radians(180),
                    movementSpeed = 5
                };

                state.EntityManager.AddSharedComponent(redCube, speedSharedComponent);

                state.EntityManager.AddSharedComponent(greenCube, speedSharedComponent);

                state.EntityManager.AddSharedComponent(blueCube, speedSharedComponent);

                state.EntityManager.AddSharedComponent(redCube, new GroupSharedComponent { group = 0 });

                state.EntityManager.AddSharedComponent(greenCube, new GroupSharedComponent { group = 1 });

                state.EntityManager.AddSharedComponent(blueCube, new GroupSharedComponent { group = 2 });

                var redCubeTransform = SystemAPI.GetComponentRW<LocalTransform>(redCube);
                var greenCubeTransform = SystemAPI.GetComponentRW<LocalTransform>(greenCube);
                var blueCubeTransform = SystemAPI.GetComponentRW<LocalTransform>(blueCube);

                redCubeTransform.ValueRW.Position = generator.redCubeGeneratorPos;
                greenCubeTransform.ValueRW.Position = generator.greenCubeGeneratorPos;
                blueCubeTransform.ValueRW.Position = generator.blueCubeGeneratorPos;

                totalCount += 3;
                timer -= generator.tickTime;
            }
            timer += SystemAPI.Time.DeltaTime;
        }
    }
}