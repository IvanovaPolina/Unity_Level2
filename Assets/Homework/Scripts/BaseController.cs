using UnityEngine;

namespace Homework
{
	public abstract class BaseController : MonoBehaviour
	{
		/// <summary>
		/// Включен ли базовый контроллер?
		/// </summary>
		public bool IsEnabled { get; private set; }

		/// <summary>
		/// Включает базовый контроллер
		/// </summary>
		public virtual void On() {
			IsEnabled = true;
		}

		/// <summary>
		/// Выключает базовый контроллер
		/// </summary>
		public virtual void Off() {
			IsEnabled = false;
		}
	}
}