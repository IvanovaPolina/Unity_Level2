using UnityEngine;

namespace Homework
{
	public class Box : BaseSceneObject, ISetDamage
	{
		public float health = 100f;

		public void ApplyDamage(float damage) {
			if (health <= 0) return;
			health -= damage;
			Color = Random.ColorHSV();
			if (health <= 0) Die();
		}

		private void Die() {
			Color = Color.red;
			Collider.enabled = false;
			Destroy(InstanceObject, 2f);
		}
	}
}