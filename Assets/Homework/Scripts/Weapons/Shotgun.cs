using UnityEngine;

namespace Homework
{
	/// <summary>
	/// Класс определяет поведение оружия "Дробовик"
	/// </summary>
	public sealed class Shotgun : Weapons
	{
		[SerializeField]
		private Transform _firepoint;   // Позиция для выстрелов
		[SerializeField]
		private int ammoCount = 5;  // сколько пуль будет выстреливать дробовик

		public override void Fire() {
			if (!TryShoot()) return;
			for (int i = 0; i < ammoCount; i++) {
				Ammo ammo = ObjectsPool.Instance.GetObject(_ammoID) as Ammo;
				ammo.Initialize(_firepoint, _force);
			}
		}

		public override void Reload() {

		}
	}
}