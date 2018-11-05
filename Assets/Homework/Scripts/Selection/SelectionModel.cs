using Homework.Interfaces;
using UnityEngine;

namespace Homework.Models
{
	/// <summary>
	/// Этот скрипт должен висеть на объектах, с которыми можно взаимодействовать
	/// </summary>
	public sealed class SelectionModel : BaseSceneObject, ISelectable
	{
		[SerializeField]
		[Tooltip("Можно ли толкать данный объект?")]
		private bool canPush;
		/// <summary>
		/// Можно ли толкать данный объект?
		/// </summary>
		public bool CanPush { get { return canPush; } }
		[SerializeField]
		[Tooltip("Дистанция, на которой с этим объектом можно взаимодействовать")]
		private float distance = 3f;
		/// <summary>
		/// Дистанция, на которой с этим объектом можно взаимодействовать
		/// </summary>
		public float Distance { get { return distance; } }
		//[SerializeField]
		//[Tooltip("Слои, которые могут взаимодействовать с этим объектом")]
		//private LayerMask layerMask;
		[SerializeField]
		[Tooltip("Сила, с которой можно толкать данный объект")]
		private float force = 10f;

		/// <summary>
		/// Позволяет толкать объект
		/// </summary>
		public void Push(Vector3 direction) {
			if (Rigidbody)
				Rigidbody.AddForce(direction * force, ForceMode.Impulse);
			else Debug.LogError("You should apply Rigidbody to the selected object");
		}
	}
}