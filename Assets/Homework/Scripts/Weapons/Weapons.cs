using UnityEngine;

namespace Homework
{
	/// <summary>
	/// Базовый класс для всех типов оружий
	/// </summary>
	public abstract class Weapons : BaseSceneObject
	{
		[SerializeField]
		protected Ammo _ammoPrefab;

		// Сила выстрела
		[SerializeField]
		protected float _force = 50f;
		[SerializeField]
		protected float reloadTime;
		private float reloadTimer;
		// Время задержки между выстрелами
		[SerializeField]
		protected float timeout = 0.5f;
		protected float lastShotTime;

		/// <summary>
		/// Функция для вызова выстрела, обязательна во всех классах наследниках
		/// </summary>
		public abstract void Fire();

		/// <summary>
		/// Проверяет, можно ли стрелять
		/// </summary>
		/// <returns>true - если стрелять можно, false - если нет</returns>
		protected bool TryShoot() {
			if (Time.time - lastShotTime < timeout) return false;
			lastShotTime = Time.time;
			return true;
		}

		// Функция перезарядки
		public abstract void Reload();
	}
}