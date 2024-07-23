using Unity.Burst.Intrinsics;
using Unity.Entities;

public partial struct StopRotateJob : IJobChunk
{

    public void Execute(in ArchetypeChunk chunk, int unfilteredChunkIndex, bool useEnabledMask, in v128 chunkEnabledMask)
    {
    }

}