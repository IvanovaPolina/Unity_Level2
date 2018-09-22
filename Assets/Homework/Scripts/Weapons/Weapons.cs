using UnityEngine;

namespace Homework
{
	/// <summary>
	/// Базовый класс для всех типов оружий
	/// </summary>
	public abstract class Weapons : BaseSceneObject
	{
		#region Serialize Variable
		// Позиция, из которой будут вылетать снаряды
		[SerializeField] protected Transform _firepoint;
		// Сила выстрела
		[SerializeField] protected float _force = 500f;
		// Время задержки между выстрелами
		[SerializeField] protected float _rechargeTime = 0.2f;
		#endregion

		#region Protected Variable
		// Флаг, разрешающий выстрел
		protected bool _canFire = true;
		#endregion

		#region Abstract Function
		// Функция для вызова выстрела, обязательна во всех дочерних классах
		public abstract void Fire();
		#endregion
	}
}