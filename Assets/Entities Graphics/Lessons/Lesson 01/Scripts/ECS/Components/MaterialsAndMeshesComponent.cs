using Unity.Entities;
using UnityEngine;

public struct MaterialsAndMeshesComponent : IComponentData
{
    public Material redMaterial;
    public Material greenMaterial;
    public Material blueMaterial;
    public Mesh cubeMesh;
    public Mesh sphereMesh;
    public Mesh capsuleMesh;
}