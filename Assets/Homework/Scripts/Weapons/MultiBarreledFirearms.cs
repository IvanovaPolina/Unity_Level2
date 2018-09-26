using UnityEngine;

namespace Homework
{
	/// <summary>
	/// Класс определяет поведение многоствольного огнестрельного оружия
	/// </summary>
	public sealed class MultiBarreledFirearms : Weapons
	{
		[SerializeField]
		private Transform[] firepoints;		// Позиции, из которых будут вылетать пули
		private int currentFirepoint;

		public override void Fire() {
			if (!TryShoot()) return;
			Ammo ammo = Instantiate(_ammoPrefab, firepoints[currentFirepoint].position, firepoints[currentFirepoint].rotation);
			ammo.Initialize(_force);
			currentFirepoint++;
			if (currentFirepoint >= firepoints.Length)
				currentFirepoint = 0;
		}

		public override void Reload() {

		}
	}
}