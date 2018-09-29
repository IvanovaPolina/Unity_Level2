using UnityEngine;

namespace Homework
{
	/// <summary>
	/// Класс определяет поведение обычных огнестрельных пуль
	/// </summary>
	public sealed class Bullet : Ammo
	{
		[SerializeField]
		private string poolID;
		[SerializeField]
		private int bulletsCount = 50;
		[SerializeField]
		private float lifetime = 5f;		// время существования пули
		[SerializeField]
		private LayerMask layerMask;    // слои, в которые может попадать пуля

		private bool isHitted = false;
		private float speed;    // скорость пули

		public override string PoolID { get { return poolID; } }
		public override int ObjectsCount { get { return bulletsCount; } }

		/// <summary>
		/// Задаёт изначальную скорость пули
		/// </summary>
		public override void Initialize(Transform firepoint, float force) {
			Position = firepoint.position;
			Rotation = firepoint.rotation;
			CancelInvoke();
			isHitted = false;	// для переиспользования пули
			speed = force;
			Invoke("Disable", lifetime);
			gameObject.SetActive(true);
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
				Disable();
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