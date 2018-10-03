using UnityEngine;

namespace Homework
{
	/// <summary>
	/// Базовый класс для всех медикаментов
	/// </summary>
	public class MedicalDevices : BaseSceneObject
	{
		// Сколько HP восстанавливает данный медикамент
		[SerializeField]
		private float treatmentAmount = 10f;

		private void OnTriggerEnter(Collider other) {
			ISetDamage obj = other.GetComponent<ISetDamage>();
			if (obj != null) {
				obj.ApplyDamage(-treatmentAmount);
				Destroy(InstanceObject);
			}
		}
	}
}