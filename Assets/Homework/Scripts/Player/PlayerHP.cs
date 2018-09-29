using UnityEngine;

namespace Homework
{
	public sealed class PlayerHP : Box
	{
		public override void ApplyDamage(float damage) {
			if (health <= 0) return;
			health -= damage;
			// проигрываем звук
			if (health <= 0) Die();
		}

		protected override void Die() {
			// проигрываем анимацию падения на землю
			Destroy(InstanceObject, 2f);
		}
	}
}