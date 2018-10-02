using UnityEngine;

namespace Homework
{
	public sealed class PlayerHP : Box
	{
		public override void ApplyDamage(float damage) {
			if (currentHealth <= 0) return;
			currentHealth -= damage;
			// проигрываем звук
			if (currentHealth <= 0) Die();
		}

		protected override void Die() {
			// проигрываем анимацию падения на землю
			Destroy(InstanceObject, 2f);
		}
	}
}