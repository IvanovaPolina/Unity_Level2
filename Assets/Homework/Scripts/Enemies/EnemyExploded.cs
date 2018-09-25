using UnityEngine;

namespace Homework
{
	public sealed class EnemyExploded : Box
	{
		[SerializeField]
		private Collider trigger;   // зона, которая отталкивает и наносит урон
		[SerializeField]
		private float explosionDamage = 10f;
		[SerializeField]
		private float explosionForce = 50f;
		[SerializeField]
		private ParticleSystem explosion;
		[SerializeField]
		private AudioClip bloodClip;
		[SerializeField]
		private AudioClip explosionClip;

		public override void ApplyDamage(float damage) {
			if (health <= 0) return;
			health -= damage;
			if (health <= 0) {
				Die();
				return;
			}
			AudioSource.PlayClipAtPoint(bloodClip, Position, 1f);
		}

		protected override void Die() {
			ParticleSystem instExplosion = Instantiate(explosion, Position, Quaternion.identity);
			AudioSource.PlayClipAtPoint(explosionClip, Position, 1f);
			trigger.enabled = true;		// активируем зону поражения от взрыва
			Collider[] purposes = Physics.OverlapBox(trigger.bounds.center, trigger.bounds.extents);	// находим всё, что сейчас в зоне поражения
			for (int i = 0; i < purposes.Length; i++) {
				ISetDamage obj = purposes[i].GetComponent<ISetDamage>();
				if (obj != null) {	// если объект может получить урон
					obj.ApplyDamage(explosionDamage);   // наносим урон
					ApplyForce(purposes[i]);	// отталкиваем объект взрывной волной
				}
			}
			Destroy(instExplosion.gameObject, instExplosion.main.duration);	// уничтожаем particle после того, как он проиграется
			Destroy(InstanceObject, instExplosion.main.duration + 1f);
		}

		private void ApplyForce(Collider applyingForce) {
			Vector3 explosionPosition = trigger.bounds.center;
			float radius = (trigger.bounds.center + trigger.bounds.max).magnitude;	// радиус отталкивания
			float downShift = trigger.bounds.extents.y;	// насколько смещена вниз начальная точка взрыва (вектор направления считается от неё до радиуса поражения)
			Rigidbody rb = applyingForce.GetComponent<Rigidbody>();
			if (rb) {
				float mass = rb.mass;
				rb.mass = 1;
				rb.AddExplosionForce(explosionForce, explosionPosition, radius, downShift, ForceMode.Impulse);
				rb.mass = mass;
			}
		}
	}
}