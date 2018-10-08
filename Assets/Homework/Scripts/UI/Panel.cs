using UnityEngine;

namespace Homework
{
	/// <summary>
	/// В данном классе перечислены основные методы для кнопок в панелях
	/// </summary>
	public class Panel : MonoBehaviour
	{
		public enum Type
		{
			Game,
			Pause
		}
		
		[SerializeField]
		private Type type;
		public Type TypeOfPanel { get { return type; } }

		/// <summary>
		/// Для продолжения игры
		/// </summary>
		public void Continue() {
			Main.Instance.MenuController.SwitchGamePanel();
		}

		/// <summary>
		/// Для остановки игры на паузу
		/// </summary>
		public void Pause() {
			Main.Instance.MenuController.SwitchPausePanel();
		}
	}
}