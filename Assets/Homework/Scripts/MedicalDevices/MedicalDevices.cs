using UnityEngine;

namespace Homework
{
	/// <summary>
	/// Базовый класс для всех медикаментов
	/// </summary>
	public abstract class MedicalDevices : BaseSceneObject, ISelectable
	{
		// Сколько HP восстанавливает данный медикамент
		[SerializeField] protected float treatmentAmount = 10f;

		public virtual void Interact() {
			// прибавляем Player'у HP
			// проигрываем звук взаимодействия с объектом
			Destroy(InstanceObject);
		}
	}
}