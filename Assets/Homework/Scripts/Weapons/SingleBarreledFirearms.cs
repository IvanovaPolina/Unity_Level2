using UnityEngine;

namespace Homework
{
	/// <summary>
	/// Класс определяет поведение одноствольного огнестрельного оружия
	/// </summary>
	public sealed class SingleBarreledFirearms : Weapons
	{
		[SerializeField]
		private Transform _firepoint;	// Позиция, из которой будут вылетать пули

		public override void Fire() {
			if (!TryShoot()) return;
			Ammo ammo = Instantiate(_ammoPrefab, _firepoint.position, _firepoint.rotation);
			ammo.Initialize(_force);
		}

		public override void Reload() {
			
		}
	}
}