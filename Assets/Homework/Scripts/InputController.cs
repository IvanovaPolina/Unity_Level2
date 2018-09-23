using UnityEngine;

namespace Homework
{
	/// <summary>
	/// Контроллер, который отвечает за горячие клавиши
	/// </summary>
	public sealed class InputController : BaseController
	{
		private void Update() {
			if (Input.GetButtonDown("SwitchFlashlight"))
				Main.Instance.FlashlightController.Switch();
			else if (Input.GetKeyDown(KeyCode.E))
				Main.Instance.SelectionController.Interact();
			//else if (Input.GetButtonDown("Inventory"))
			//	Main.Instance.SelectionController.Switch();
		}
	}
}