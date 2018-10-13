using UnityEngine;

namespace Homework
{
	public class MenuController : BaseController
	{
		private void Awake() {
			Time.timeScale = 1;
			SwitchGamePanel();
		}

		public void SwitchPausePanel() {
			MenuModel.Instance.SwitchPanel(Panel.Type.Pause);
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;
			Time.timeScale = 0;
		}

		public void SwitchGamePanel() {
			Time.timeScale = 1;
			Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Locked;
			MenuModel.Instance.SwitchPanel(Panel.Type.Game);
		}
	}
}