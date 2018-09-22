using UnityEngine;

namespace Homework
{
	public sealed class FlashlightController : BaseController
	{
		private FlashlightModel _model;

		private void Awake() {
			_model = FindObjectOfType<FlashlightModel>();
			Off();
		}

		private void Update() {
			if (_model.CurrentCharge <= 0) Switch();
		}

		/// <summary>
		/// Выключает базовый контроллер и фонарик
		/// </summary>
		public override void Off() {
			base.Off();
			_model.Off();
		}

		/// <summary>
		/// Включает базовый контроллер и фонарик
		/// </summary>
		public override void On() {
			if (_model.CurrentCharge >= _model.batteryCharge * _model.minChargeInPercent) {
				base.On();
				_model.On();
			}
		}

		/// <summary>
		/// Переключает текущее состояние контроллера и фонарика на противоположное
		/// </summary>
		public void Switch() {
			if (IsEnabled) Off();
			else On();
		}
	}
}