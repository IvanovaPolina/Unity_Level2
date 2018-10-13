using Homework.Data;
using UnityEngine;

namespace Homework
{
	public sealed class PlayerModel : BaseSceneObject, ISetDamage, ISaveLoadObject
	{
		public static PlayerModel LocalPlayer { get; private set; }

		[Range(1f, 1000f)]
		[SerializeField]
		private float maxHealth = 100f;
		public float MaxHealth { get { return maxHealth; } }
		[Range(1f, 1000f)]
		[SerializeField]
		private float currentHealth = 100f;
		public float CurrentHealth { get { return currentHealth; } set { currentHealth = value; } }

		[HideInInspector]
		public Weapons[] weapons;
		public float maxDistanceToControlTeammate = 40f;

		public GameObjectData ObjectData { get { return objData; } set { objData = value; } }
		private GameObjectData objData;
		public Component GameObjectScript { get { return this; } }

		protected override void Awake() {
			if (LocalPlayer) DestroyImmediate(this);
			else LocalPlayer = this;
			base.Awake();

			weapons = GetComponentsInChildren<Weapons>(true);
			objData = new GameObjectData() {
				name = Name,
				HP = CurrentHealth,
				position = new GameObjectData.Vector3(Position.x, Position.y, Position.z),
				quaternion = new GameObjectData.Quaternion(Rotation.x, Rotation.y, Rotation.z, Rotation.w),
				scale = new GameObjectData.Vector3(Scale.x, Scale.y, Scale.z)
			};
		}

		public void ApplyDamage(float damage) {
			if (currentHealth <= 0) return;
			currentHealth -= damage;
			// проигрываем звук
			if (currentHealth <= 0) Die();
		}

		private void Die() {
			// проигрываем анимацию падения на землю
			Destroy(InstanceObject, 2f);
		}
	}
}