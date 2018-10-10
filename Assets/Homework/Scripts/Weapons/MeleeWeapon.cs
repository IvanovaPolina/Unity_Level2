using UnityEngine;
using UnityEngine.Events;

namespace Homework
{
	public class MeleeWeapon : Weapons
	{
		public static UnityAction OnAttack;
		[SerializeField]
		private Transform startHitPoint;
		[SerializeField]
		private Transform endHitPoint;
		[SerializeField]
		private float damage = 10f;
		[SerializeField]
		private float linecastTime = 0.25f;
		private bool isHitted;
		private RaycastHit hit;

		private void Linecast() {
			isHitted = Physics.Linecast(startHitPoint.position, endHitPoint.position, out hit);
			if (isHitted) {
				ISetDamage obj = hit.collider.GetComponent<ISetDamage>();
				if (obj != null) obj.ApplyDamage(damage);
			}
		}

		public override void Fire() {
			if (!TryShoot()) return;
			if (OnFire != null) OnFire.Invoke(endHitPoint.parent.gameObject);
			if (OnAttack != null) OnAttack.Invoke();
			Invoke("Linecast", linecastTime);
		}

		public override void Reload() {
			
		}
	}
}