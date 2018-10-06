using UnityEngine;
using UnityEngine.UI;

namespace Homework
{
	public sealed class FlashlightViewImage : MonoBehaviour
	{
		private Image fillArea;
		public Color green;
		public Color red;

		private void Awake() {
			fillArea = GetComponent<Image>();
			FlashlightModel.OnChargeChanged += ChangeCharge;
		}

		private void OnDestroy() {
			FlashlightModel.OnChargeChanged -= ChangeCharge;
		}

		private void ChangeCharge(float currentCharge, float minChargeInPercent) {
			fillArea.fillAmount = currentCharge;
			if (currentCharge < minChargeInPercent)
				fillArea.color = red;
			else fillArea.color = green;
		}
	}
}