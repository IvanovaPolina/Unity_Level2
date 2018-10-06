using UnityEngine;
using UnityEngine.UI;

namespace Homework
{
	public sealed class FlashlightViewSlider : MonoBehaviour
	{
		public Slider slider;
		public Image fillArea;
		public Color green;
		public Color red;
		
		private FlashlightModel _model;

		private void Awake() {
			_model = FindObjectOfType<FlashlightModel>();
			FlashlightModel.OnChargeChanged += SetSliderValue;
			slider.minValue = 0;
			slider.maxValue = _model.batteryCharge;
			slider.value = slider.maxValue;
		}

		private void SetSliderValue(float currentCharge, float minChargeInPercent) {
			slider.value = currentCharge;
			if (currentCharge >= minChargeInPercent)
				fillArea.color = green;
			else fillArea.color = red;
		}

		private void OnDestroy() {
			FlashlightModel.OnChargeChanged -= SetSliderValue;
		}
	}
}