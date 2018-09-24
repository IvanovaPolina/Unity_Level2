using UnityEngine;

namespace Homework
{
	/// <summary>
	/// Хранит в себе логику того, как стоит выделять объект
	/// </summary>
	public sealed class SelectionModel : BaseSceneObject
	{
		public float selectingDistance = 3f;    // Дистанция, на которой игрок будет "видеть" объект
		/// <summary>
		/// Обнаруженный игроком объект
		/// </summary>
		public ISelectable SelectedObject
		{
			get {
				if (selectedObj != null) return selectedObj;
				else return null;
			}
		}

		private ISelectable selectedObj;
		private RaycastHit hit;

		/// <summary>
		/// Проверяет, есть ли впереди объект
		/// </summary>
		public void Linecast() {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out hit, selectingDistance))
				selectedObj = hit.transform.GetComponent<ISelectable>();
		}

		/// <summary>
		/// Взаимодействует с объектом
		/// </summary>
		public void Interact() {
			if (selectedObj != null) selectedObj.Interact();
		}
	}
}