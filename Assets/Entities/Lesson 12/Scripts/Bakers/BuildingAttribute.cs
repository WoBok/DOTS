using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace Entity_Lesson12
{
    public enum BuidingType
    {
        BT_Spawner,
        BT_DefenderTower,
        BT_MAX
    }

    public enum ArmorType
    {
        AT_None = 0,
        AT_Light,
        AT_Normal,
        AT_Heavy,
        AT_Hero,
        AT_Max
    }

    public enum DamageType
    {
        DT_Slash = 0,
        DT_Pricks,
        DT_Smash,
        DT_Magic,
        DT_Chaos,
        DT_Hero,
        DT_Max
    }

    public struct BuildingAttributeBlobData
    {
        public Entity prefab; //8byte
        public BuidingType buildingType; //4byte
        public int level; //4byte
        public float tickTime; //4byte
        public int spawnCountPerTicktime; //4byte
        public float maxLife; //4byte
        public ArmorType armorType; //4byte
        public DamageType damageType; //4byte
        public float maxDamage; //4byte
        public float minDamage; //4byte
        public float upgradeTime; //4byte
        public float upgradeCost; //4byte
    }

    class BuildingAttribute : MonoBehaviour
    {
        public bool usingBlob = true;

        [Header("Data")]
        public GameObject prefab;
        public BuidingType buildingType; //4byte
        public int level; //4byte
        public float tickTime; //4byte
        public int spawnCountPerTicktime; //4byte
        public float maxLife; //4byte
        public ArmorType armorType; //4byte
        public DamageType damageType; //4byte
        public float maxDamage; //4byte
        public float minDamage; //4byte
        public float upgradeTime; //4byte
        public float upgradeCost; //4byte
    }

    class BuildingAttributeBaker : Baker<BuildingAttribute>
    {
        public override void Bake(BuildingAttribute authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);

            if (authoring.usingBlob)
            {
                var attributesComponent = new BuildingAttributesComponent { currentlife = authoring.maxLife };
                AddComponent(entity, attributesComponent);

                var buildingAttributes = CreateBuildAttributesData(authoring);
                AddBlobAsset(ref buildingAttributes, out var hash);

                var attibutesData = new BuildingAttributeBlobComponent
                {
                    buildingAttributes = buildingAttributes
                };
                AddComponent(entity, attibutesData);
            }
            else
            {
                var allAttributesComponent = new BuildingAllAttributesComponent
                {
                    prefab = GetEntity(authoring.prefab, TransformUsageFlags.Dynamic),
                    buildingType = authoring.buildingType,
                    level = authoring.level,
                    tickTime = authoring.tickTime,
                    spawnCountPerTicktime = authoring.spawnCountPerTicktime,
                    maxLife = authoring.maxLife,
                    armorType = authoring.armorType,
                    damageType = authoring.damageType,
                    maxDamage = authoring.maxDamage,
                    minDamage = authoring.minDamage,
                    upgradeTime = authoring.upgradeTime,
                    upgradeCost = authoring.upgradeCost
                };
                AddComponent(entity, allAttributesComponent);
            }
        }

        BlobAssetReference<BuildingAttributeBlobData> CreateBuildAttributesData(BuildingAttribute authoring)
        {
            var builder = new BlobBuilder(Allocator.Temp);
            ref BuildingAttributeBlobData buildingAttributeData = ref builder.ConstructRoot<BuildingAttributeBlobData>();

            buildingAttributeData.prefab = GetEntity(authoring.prefab, TransformUsageFlags.Dynamic);
            buildingAttributeData.buildingType = authoring.buildingType;
            buildingAttributeData.level = authoring.level;
            buildingAttributeData.tickTime = authoring.tickTime;
            buildingAttributeData.spawnCountPerTicktime = authoring.spawnCountPerTicktime;
            buildingAttributeData.maxLife = authoring.maxLife;
            buildingAttributeData.armorType = authoring.armorType;
            buildingAttributeData.damageType = authoring.damageType;
            buildingAttributeData.maxDamage = authoring.maxDamage;
            buildingAttributeData.minDamage = authoring.minDamage;
            buildingAttributeData.upgradeTime = authoring.upgradeTime;
            buildingAttributeData.upgradeCost = authoring.upgradeCost;

            var result = builder.CreateBlobAssetReference<BuildingAttributeBlobData>(Allocator.Persistent);
            builder.Dispose();
            return result;
        }
    }
}