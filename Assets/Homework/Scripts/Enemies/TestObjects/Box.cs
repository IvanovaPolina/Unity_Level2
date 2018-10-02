using UnityEngine;

namespace Homework
{
	/// <summary>
	/// Базовый класс для всех объектов, способных получать урон
	/// </summary>
	public abstract class Box : BaseSceneObject, ISetDamage
	{
		[SerializeField]
		protected float currentHealth = 100f;
		public float CurrentHealth { get { return currentHealth; } }

		[SerializeField]
		protected float maxHealth = 100f;
		public float MaxHealth { get { return maxHealth; } }

		public abstract void ApplyDamage(float damage);

		protected abstract void Die();
	}
}