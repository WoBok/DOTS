using Unity.Entities;

namespace Entity_Lesson12
{
    public struct BuildingAllAttributesComponent : IComponentData
    {
        public Entity prefab; //8byte
        public BuidingType buildingType; //4byte
        public int level; //4byte
        public float tickTime; //4byte
        public int spawnCountPerTicktime; //4byte
        public float maxLife; //4byte
        public float currentlife; // 4byte
        public ArmorType armorType; //4byte
        public DamageType damageType; //4byte
        public float maxDamage; //4byte
        public float minDamage; //4byte
        public float upgradeTime; //4byte
        public float upgradeCost; //4byte
    }
}