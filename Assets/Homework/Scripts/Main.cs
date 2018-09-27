using UnityEngine;

namespace Homework
{
	public sealed class Main : MonoBehaviour
	{
		/// <summary>
		/// Точка доступа ко всем контроллерам
		/// </summary>
		public static Main Instance { get; private set; }
		public InputController InputController { get; private set; }
		public FlashlightController FlashlightController { get; private set; }
		public SelectionController SelectionController { get; private set; }
		public WeaponController WeaponController { get; private set; }
		public TeammateController TeammateController { get; private set; }

		private void Awake() {
			// проверяем создание ЕДИНСТВЕННОГО экземпляра данного класса
			if (Instance) DestroyImmediate(this);
			else Instance = this;
		}

		private void Start() {
			InputController = gameObject.AddComponent<InputController>();
			FlashlightController = gameObject.AddComponent<FlashlightController>();
			SelectionController = gameObject.AddComponent<SelectionController>();
			WeaponController = gameObject.AddComponent<WeaponController>();
			TeammateController = gameObject.AddComponent<TeammateController>();
		}
	}
}