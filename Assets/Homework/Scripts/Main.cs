using UnityEngine;

namespace Homework
{
	public class Main : MonoBehaviour
	{
		public static Main Instance { get; private set; }
		public InputController InputController { get; private set; }
		public FlashlightController FlashlightController { get; private set; }

		private void Awake() {
			// проверяем создание ЕДИНСТВЕННОГО экземпляра данного класса
			if (Instance) DestroyImmediate(this);
			else Instance = this;
		}

		private void Start() {
			InputController = gameObject.AddComponent<InputController>();
			FlashlightController = gameObject.AddComponent<FlashlightController>();
		}
	}
}