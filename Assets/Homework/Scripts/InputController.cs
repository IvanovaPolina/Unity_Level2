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
			if (Input.GetKeyDown(KeyCode.E))
				Main.Instance.SelectionController.Interact();
			//else if (Input.GetButtonDown("Inventory"))
			//	Main.Instance.SelectionController.Switch();
			if (Input.GetButtonDown("ChangeWeapon"))
				Main.Instance.WeaponController.ChangeWeapon();
			if (Input.mouseScrollDelta.y != 0)
				Main.Instance.WeaponController.ChangeWeapon(Input.mouseScrollDelta.y);
			if (Input.GetButton("Fire1"))
				Main.Instance.WeaponController.Fire();
			if (Input.GetButtonDown("FollowPlayer"))
				Main.Instance.TeammateController.Follow();
			if (Input.GetButtonDown("TeammateCommand"))
				Main.Instance.TeammateController.MoveCommand();
			if (Input.GetKeyDown(KeyCode.P))
				Main.Instance.MenuController.SwitchPausePanel();
		}
	}
}