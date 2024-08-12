using Unity.Entities;
using UnityEngine;

namespace Entities_Graphics.MeshDeformations
{
    class BlendShapesSpeed : MonoBehaviour
    {
        public float speed;
    }

    class BlendShapesSpeedBaker : Baker<BlendShapesSpeed>
    {
        public override void Bake(BlendShapesSpeed authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);
            var data = new BlendShapesSpeedComponent { speed = authoring.speed };
            AddComponent(entity, data);
        }
    } 
}