using UnityEngine;

namespace Homework
{
	/// <summary>
	/// Базовый класс для всех боеприпасов
	/// </summary>
	public abstract class Ammo : BaseSceneObject, IPoolable
	{
		/// <summary>
		/// Количество урона от боеприпаса
		/// </summary>
		[SerializeField]
		protected float damage = 10f;

		public abstract string PoolID { get; }
		public abstract int ObjectsCount { get; }
		public bool IsActive { get { return InstanceObject.activeSelf; } }

		/// <summary>
		/// Задаёт изначальную скорость пули
		/// </summary>
		public abstract void Initialize(Transform firepoint, float force);

		public virtual void Disable() {
			gameObject.SetActive(false);
		}
	}
}