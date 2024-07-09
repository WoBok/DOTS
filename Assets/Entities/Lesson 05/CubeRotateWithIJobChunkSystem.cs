using Unity.Burst;
using Unity.Burst.Intrinsics;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;

[BurstCompile]
struct CubeRotateJobChunk : IJobChunk
{
    public float deltaTime;
    public ComponentTypeHandle<LocalTransform> transformTypeHandle;
    [ReadOnly] public ComponentTypeHandle<RotateSpeed> rotationSpeedTypeHandle;

    public void Execute(in ArchetypeChunk chunk, int unfilteredChunkIndex, bool useEnabledMask, in v128 chunkEnabledMask)
    {
        var chunkTransforms = chunk.GetNativeArray(ref transformTypeHandle);
        var chunkRotationSpeeds = chunk.GetNativeArray(ref rotationSpeedTypeHandle);

        //枚举器和for循环两者选其一
        //根据Mask过滤掉被禁用的Entity
        var enumerator = new ChunkEntityEnumerator(useEnabledMask, chunkEnabledMask, chunk.Count);
        while (enumerator.NextEntityIndex(out var i))
        {
            var speed = chunkRotationSpeeds[i];
            chunkTransforms[i] = chunkTransforms[i].RotateZ(speed.rotateSpeed * deltaTime);
        }

        //for (int i = 0, chunkEntityCount = chunk.Count; i < chunkEntityCount; i++)
        //{
        //    var speed = chunkRotationSpeeds[i];
        //    chunkTransforms[i] = chunkTransforms[i].RotateY(speed.rotateSpeed * deltaTime);
        //}
    }
}

[DisableAutoCreation]
[BurstCompile]
[UpdateInGroup(typeof(Lesson05SystemGroup))]
public partial struct CubeRotateJobChunkSystem : ISystem
{
    EntityQuery rotateCubes;
    ComponentTypeHandle<LocalTransform> transformTypeHandle;
    ComponentTypeHandle<RotateSpeed> rotationSpeedTypeHandle;

    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        var queryBuilder = new EntityQueryBuilder(Allocator.Temp).WithAll<RotateSpeed, LocalTransform>();
        rotateCubes = state.GetEntityQuery(queryBuilder);

        transformTypeHandle = state.GetComponentTypeHandle<LocalTransform>();
        rotationSpeedTypeHandle = state.GetComponentTypeHandle<RotateSpeed>();
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        transformTypeHandle.Update(ref state);
        rotationSpeedTypeHandle.Update(ref state);

        var job = new CubeRotateJobChunk
        {
            deltaTime = SystemAPI.Time.DeltaTime,
            transformTypeHandle = transformTypeHandle,
            rotationSpeedTypeHandle = rotationSpeedTypeHandle
        };
        //需传入依赖项的jobHandle(state.Dependency)并返回IJobChunk的输入依赖项，以确保辅助Job在主Job之前完成
        state.Dependency = job.ScheduleParallel(rotateCubes, state.Dependency);
    }
}