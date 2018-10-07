using UnityEngine;

namespace Homework
{
	public sealed class PlayerModel : BaseSceneObject, ISetDamage
	{
		public static PlayerModel LocalPlayer { get; private set; }

		[Range(1f, 1000f)]
		[SerializeField]
		private float maxHealth = 100f;
		public float MaxHealth { get { return maxHealth; } }
		[Range(1f, 1000f)]
		[SerializeField]
		private float currentHealth = 100f;
		public float CurrentHealth { get { return currentHealth; } }

		[HideInInspector]
		public Weapons[] weapons;
		public float maxDistanceToControlTeammate = 40f;

		protected override void Awake() {
			if (LocalPlayer) DestroyImmediate(this);
			else LocalPlayer = this;
			base.Awake();

			weapons = GetComponentsInChildren<Weapons>(true);
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