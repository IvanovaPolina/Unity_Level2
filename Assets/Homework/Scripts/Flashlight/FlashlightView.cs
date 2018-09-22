using UnityEngine;
using UnityEngine.UI;

namespace Homework
{
	public sealed class FlashlightView : MonoBehaviour
	{
		public Slider slider;
		public Image fillArea;
		public Color green;
		public Color red;
		
		private FlashlightModel _model;

		private void Awake() {
			_model = FindObjectOfType<FlashlightModel>();
			if(slider) {
				slider.minValue = 0;
				slider.maxValue = _model.batteryCharge;
				slider.wholeNumbers = true;
			}
		}

		private void Update() {
			SetSliderValue();
		}

		private void SetSliderValue() {
			slider.value = _model.CurrentCharge;
			if (_model.CurrentCharge >= _model.batteryCharge * _model.minChargeInPercent)
				fillArea.color = green;
			else fillArea.color = red;
		}
	}
}