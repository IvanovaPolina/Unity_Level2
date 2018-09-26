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
				// Наносим урон, меняем дамаг и уничтожаем пулю
				SetDamage(hit);
				Destroy(InstanceObject, 0.3f);
			}
			else Position = finalPos;
		}

		private void SetDamage(RaycastHit hit) {
			ISetDamage objSet = hit.collider.GetComponent<ISetDamage>();
			if (objSet == null) return;    // если объект не реализует ISetDamage, значит не может получать урон
			IChangeDamage objChange = hit.collider.GetComponent<IChangeDamage>();
			if (objChange != null) {	// если объект реализует IChangeDamage - наносим ему увеличенный урон
				objChange.ApplyMultDamage(damage);
				return;
			}
			objSet.ApplyDamage(damage);
		}
	}
}