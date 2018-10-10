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
				// Win, Lose
		}
		
		[SerializeField]
		private Type type;
		public Type PanelType { get { return type; } }

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