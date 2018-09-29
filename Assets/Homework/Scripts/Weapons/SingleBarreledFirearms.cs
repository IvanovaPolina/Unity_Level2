using UnityEngine;

namespace Homework
{
	/// <summary>
	/// Класс определяет поведение одноствольного огнестрельного оружия
	/// </summary>
	public sealed class SingleBarreledFirearms : Weapons
	{
		[SerializeField]
		private Transform _firepoint;	// Позиция для выстрелов

		public override void Fire() {
			if (!TryShoot()) return;
			Ammo ammo = ObjectsPool.Instance.GetObject(_ammoID) as Ammo;
			ammo.Initialize(_firepoint, _force);
		}

		public override void Reload() {
			
		}
	}
}