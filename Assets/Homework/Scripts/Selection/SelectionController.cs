using System.Collections;

namespace Homework
{
	public sealed class SelectionController : BaseController
	{
		private SelectionModel model;

		private void Awake() {
			model = FindObjectOfType<SelectionModel>();
			On();
		}

		/// <summary>
		/// Позволяет обнаружать объекты
		/// </summary>
		public override void On() {
			base.On();
			StartCoroutine(Linecast());
		}

		/// <summary>
		/// Не позволяет обнаружать объекты
		/// </summary>
		public override void Off() {
			base.Off();
			StopCoroutine(Linecast());
		}

		/// <summary>
		/// Переключает режим активации контроллера на противоположный
		/// </summary>
		public void Switch() {
			if (IsEnabled) Off();
			else On();
		}

		private IEnumerator Linecast() {
			while (true) {
				model.Linecast();
				yield return null;
			}
		}

		/// <summary>
		/// Взаимодействует с выделенным объектом
		/// </summary>
		public void Interact() {
			model.Interact();
		}
	}
}