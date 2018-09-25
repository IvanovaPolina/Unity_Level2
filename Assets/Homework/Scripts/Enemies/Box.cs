using UnityEngine;

namespace Homework
{
	/// <summary>
	/// Базовый класс для всех объектов, способных получать урон
	/// </summary>
	public abstract class Box : BaseSceneObject, ISetDamage
	{
		[SerializeField]
		protected float health = 100f;

		public abstract void ApplyDamage(float damage);

		protected abstract void Die();
	}
}