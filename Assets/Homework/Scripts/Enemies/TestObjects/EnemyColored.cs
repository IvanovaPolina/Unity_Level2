using System.Collections;
using UnityEngine;

namespace Homework
{
	public sealed class EnemyColored : Box, IChangeDamage
	{
		[Range(0.01f, 1f)]
		[SerializeField]
		private float maxMult = 0.5f;	// сколько дамага может максимально прибавиться
		[Range(0.01f, 1f)]
		[SerializeField]
		private float mult = 0.1f;	// сколько дамага будет каждый раз прибавляться
		private float currentMult = 0;	// текущий прибавленный дамаг (в %)
		private float currentDamage = 0;    // текущий дамаг
		[SerializeField]
		private float multDamageTime = 2f;    // время, по истечении которого дамаг сбрасывается до стандартного
		private float lastDamagedTime = 0;	// храним время последнего получения дамага

		public override void ApplyDamage(float damage) {
			if (currentHealth <= 0) return;
			currentHealth -= damage;
			Color = Random.ColorHSV();
			if (currentHealth <= 0) Die();
		}

		protected override void Die() {
			Color = Color.red;
			Collider.enabled = false;
			Rigidbody.useGravity = true;
			Destroy(InstanceObject, 2f);
		}

		/// <summary>
		/// Изменяет урон
		/// </summary>
		/// <param name="damage"></param>
		private void MultDamage(float damage) {
			if (Time.time - lastDamagedTime < multDamageTime) { // если попадание прошло в указанный промежуток времени
				currentMult += mult;    // 0.1f, 0.2f, 0.3f...
				if (currentMult > maxMult) currentMult = maxMult;   // currentMult = 0.5f
				currentDamage = damage + damage * currentMult;  // каждый раз увеличиваем дамаг на 10%
			} else {
				currentDamage = damage; // если время истекло - сбрасываем дамаг до стандартного
				currentMult = 0;	// сбрасываем увеличение дамага
			}
			lastDamagedTime = Time.time;	// запоминаем время последнего попадания
		}

		/// <summary>
		/// Наносит измененный урон
		/// </summary>
		public void ApplyMultDamage(float damage) {
			MultDamage(damage);
			ApplyDamage(currentDamage);
		}
	}
}