using UnityEngine;

namespace Homework
{
	public class MenuModel : MonoBehaviour
	{
		public static MenuModel Instance { get; private set; }
		private Panel[] panels;

		private void Awake() {
			if (Instance) DestroyImmediate(this);
			else Instance = this;
			
			panels = transform.GetComponentsInChildren<Panel>(true);
		}

		public void SwitchPanel(Panel.Type panel) {
			foreach (var p in panels)
				p.gameObject.SetActive(p.PanelType == panel);
		}
	}
}