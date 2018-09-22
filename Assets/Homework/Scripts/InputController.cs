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
		}
	}
}