using Homework.Data;
using UnityEngine;

namespace Homework
{
	/// <summary>
	/// Базовый класс для всех медикаментов
	/// </summary>
	public class MedicalDevices : BaseSceneObject, ISaveLoadObject
	{
		// Сколько HP восстанавливает данный медикамент
		[SerializeField]
		private float treatmentAmount = 10f;

		public GameObjectData ObjectData { get { return objData; } set { objData = value; } }
		private GameObjectData objData;
		public Component GameObjectScript { get { return this; } }

		protected override void Awake() {
			base.Awake();
			objData = new GameObjectData() {
				name = Name,
				position = new GameObjectData.Vector3(Position.x, Position.y, Position.z),
				quaternion = new GameObjectData.Quaternion(Rotation.x, Rotation.y, Rotation.z, Rotation.w),
				scale = new GameObjectData.Vector3(Scale.x, Scale.y, Scale.z)
			};
		}

		private void OnTriggerEnter(Collider other) {
			ISetDamage obj = other.GetComponent<ISetDamage>();
			if (obj != null) {
				obj.ApplyDamage(-treatmentAmount);
				Destroy(InstanceObject);
			}
		}
	}
}