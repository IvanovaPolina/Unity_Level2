using UnityEngine;

namespace Homework
{
	public sealed class EnemyColored : Box
	{
		public override void ApplyDamage(float damage) {
			if (health <= 0) return;
			health -= damage;
			Color = Random.ColorHSV();
			if (health <= 0) Die();
		}

		protected override void Die() {
			Color = Color.red;
			Collider.enabled = false;
			Rigidbody.useGravity = true;
			Destroy(InstanceObject, 2f);
		}
	}
}