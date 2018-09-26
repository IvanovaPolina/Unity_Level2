using UnityEngine;

namespace Homework
{
	/// <summary>
	/// Базовый класс для всех боеприпасов
	/// </summary>
	public abstract class Ammo : BaseSceneObject
	{
		/// <summary>
		/// Количество урона от боеприпаса
		/// </summary>
		[SerializeField]
		protected float damage = 10f;

		/// <summary>
		/// Задаёт изначальную скорость пули
		/// </summary>
		public abstract void Initialize(float force);
	}
}