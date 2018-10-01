using UnityEngine;

namespace Homework
{
	/// <summary>
	/// Класс определяет поведение многоствольного огнестрельного оружия
	/// </summary>
	public sealed class MultiBarreledFirearms : Weapons
	{
		[SerializeField]
		private Transform[] firepoints;     // Позиции для выстрелов
		private int currentFirepoint;

		public override void Fire() {
			if (!TryShoot()) return;
			Ammo ammo = ObjectsPool.Instance.GetObject(_ammoID) as Ammo;
			if (OnFire != null)
				OnFire.Invoke(firepoints[currentFirepoint].parent.gameObject);
			ammo.Initialize(firepoints[currentFirepoint], _force);
			currentFirepoint++;
			if (currentFirepoint >= firepoints.Length)
				currentFirepoint = 0;
		}

		public override void Reload() {

		}
	}
}