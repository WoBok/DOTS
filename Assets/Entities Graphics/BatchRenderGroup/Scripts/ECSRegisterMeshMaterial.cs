using Unity.Entities;
using Unity.Rendering;

public partial class ECSRegisterMeshMaterial : SystemBase
{
    protected override void OnUpdate()
    {
        var entitiesGraphicsSystem = World.GetOrCreateSystemManaged<EntitiesGraphicsSystem>();
        entitiesGraphicsSystem.RegisterMesh(new UnityEngine.Mesh());
    }
}
