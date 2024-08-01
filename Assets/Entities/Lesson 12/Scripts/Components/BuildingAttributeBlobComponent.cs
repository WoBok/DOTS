using Unity.Entities;

namespace Entity_Lesson12
{
    public struct BuildingAttributeBlobComponent : IComponentData
    {
        public BlobAssetReference<BuildingAttributeBlobData> buildingAttributes;
    }
}