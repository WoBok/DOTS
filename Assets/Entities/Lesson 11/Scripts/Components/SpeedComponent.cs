using Unity.Entities;

namespace Entity_Lesson11
{
	public struct SpeedComponent : IComponentData
	{
		public float movementSpeed;
		public float rotationSpeed;
	} 
}