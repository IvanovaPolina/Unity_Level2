using System.Collections;
using UnityEngine;

namespace Homework
{
	public sealed class FlashlightModel : MonoBehaviour
	{
		/// <summary>
		/// Включен или выключен фонарик?
		/// </summary>
		public bool IsOn
		{
			get {
				if (!_light) return false;
				return _light.enabled;
			}
		}

		private Light _light;
		/// <summary>
		/// Максимальный заряд батареи
		/// </summary>
		public float batteryCharge = 10f;
		/// <summary>
		/// Текущий заряд батареи
		/// </summary>
		public float CurrentCharge { get { return currentCharge; } }
		private float currentCharge;
		public float chargeSpeed = 1f;     // скорость зарядки/разрядки батареи
		/// <summary>
		/// Скорость зарядки/разрядки батареи с учетом времени
		/// </summary>
		public float ChargeSpeedInTime { get { return chargeSpeed * Time.deltaTime; } }
		/// <summary>
		/// Сколько процентов заряда должно быть, чтобы фонарик можно было включить
		/// </summary>
		[Range(0, 1)]
		public float minChargeInPercent = 0.3f;

		private void Awake() {
			_light = GetComponent<Light>();
			currentCharge = batteryCharge;      // в начале игры делаем полный заряд батареи
		}

		/// <summary>
		/// Включает фонарик и разряжает батарейку
		/// </summary>
		public void On() {
			_light.enabled = true;
			StartCoroutine(DecrementCharge());
		}

		/// <summary>
		/// Выключает фонарик и заряжает батарейку
		/// </summary>
		public void Off() {
			_light.enabled = false;
			StartCoroutine(IncrementCharge());
		}

		/// <summary>
		/// Заряжает батарейку
		/// </summary>
		IEnumerator IncrementCharge() {
			while(!IsOn && currentCharge < batteryCharge) {
				currentCharge += ChargeSpeedInTime;
				yield return null;
			}
			if (currentCharge >= batteryCharge)
				currentCharge = batteryCharge;
		}

		/// <summary>
		/// Разряжает батарейку
		/// </summary>
		IEnumerator DecrementCharge() {
			while (IsOn && currentCharge > 0) {
				currentCharge -= ChargeSpeedInTime;
				yield return null;
			}
			if (currentCharge <= 0) currentCharge = 0;
		}
	}
}