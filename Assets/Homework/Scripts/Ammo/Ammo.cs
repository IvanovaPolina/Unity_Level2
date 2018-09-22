using UnityEngine;

namespace Homework
{
	public abstract class Ammo : BaseSceneObject
	{
		// Количество урона
		[SerializeField] protected float damage = 10f;
		// Скорость полёта при выстреле
		[SerializeField] protected float speed = 10f;
		// Направление движения (пример: граната кидается вверх и падает под воздействием силы тяжести, а пуля летит прямо)
		protected Vector3 direction;
	}
}