using UnityEngine;

namespace Homework
{
	/// <summary>
	/// Класс определяет поведение обычных огнестрельных пуль
	/// </summary>
	public sealed class Bullet : Ammo
	{
		[SerializeField]
		private float lifetime = 5f;		// время существования пули
		[SerializeField]
		private LayerMask layerMask;    // слои, в которые может попадать пуля

		private bool isHitted = false;
		private float speed;    // скорость пули

		/// <summary>
		/// Задаёт изначальную скорость пули
		/// </summary>
		public override void Initialize(float force) {
			speed = force;
		}

		private void Start() {
			Destroy(InstanceObject, lifetime);
		}

		private void FixedUpdate() {
			if (isHitted) return;
			Vector3 finalPos = Position + Transform.forward * speed * Time.fixedDeltaTime;
			RaycastHit hit;
			if (Physics.Linecast(Position, finalPos, out hit, layerMask)) {
				isHitted = true;
				Position = hit.point;
				// Наносим урон и уничтожаем пулю
				ISetDamage obj = hit.collider.GetComponent<ISetDamage>();
				if (obj != null) obj.ApplyDamage(damage);
				Destroy(InstanceObject, 0.3f);
			} 
			else Position = finalPos;
		}
	}
}