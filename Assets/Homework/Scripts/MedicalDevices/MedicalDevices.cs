using UnityEngine;

namespace Homework
{
	public abstract class MedicalDevices : BaseSceneObject
	{
		// Сколько HP восстанавливает данный медикамент
		[SerializeField] protected float treatmentAmount = 10f;


	}
}