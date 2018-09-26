using UnityEngine;

namespace Homework
{
	public sealed class ShotgunBullet : Ammo
	{
		[SerializeField]
		private float radius = 0.5f;    // максимальное отклонение траектории полёта пули
		[SerializeField]
		private float lifetime = 5f;        // время существования пули
		[SerializeField]
		private LayerMask layerMask;    // слои, в которые может попадать пуля

		private bool isHitted = false;
		private float speed;    // скорость пули
		private Vector3 finalPos = new Vector3(0, 0, 1);
		private Vector2 endPoint;

		public override void Initialize(float force) {
			speed = force;
		}

		private void Start() {
			Destroy(InstanceObject, lifetime);
			endPoint = Random.insideUnitCircle * radius;    // берем рандомную точку в пределах круга
		}

		private void FixedUpdate() {
			if (isHitted) return;
			Vector3 endDirPoint = new Vector3(endPoint.x, endPoint.y, finalPos.z); // определяем конечную точку для вектора направления
			finalPos = Position + endDirPoint * speed * Time.fixedDeltaTime;
			RaycastHit hit;
			if (Physics.Linecast(Position, finalPos, out hit, layerMask)) {
				isHitted = true;
				Position = hit.point;
				// Наносим урон и уничтожаем пулю
				ISetDamage obj = hit.collider.GetComponent<ISetDamage>();
				if (obj != null) obj.ApplyDamage(damage);
				Destroy(InstanceObject);
			} else Position = finalPos;
		}
	}
}