using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Homework
{
	public sealed class FlashlightModel : MonoBehaviour
	{
		/// <summary>
		/// Подписываемся, если хотим отслеживать текущий заряд фонарика
		/// </summary>
		public static UnityAction<float, float> OnChargeChanged;
		[Range(0.1f, 1f)]
		/// <summary>
		/// Максимальный заряд батареи
		/// </summary>
		public float batteryCharge = 1f;
		/// <summary>
		/// Текущий заряд батареи
		/// </summary>
		public float CurrentCharge { get; private set; }
		[Range(0.01f, 0.2f)]
		public float changedCharge = 0.05f;	// сколько заряда будет уходить/приходить со скоростью chargeSpeed
		public float chargeSpeed = 1f;     // скорость зарядки/разрядки батареи
		/// <summary>
		/// Сколько процентов заряда должно быть, чтобы фонарик можно было включить
		/// </summary>
		[Range(0, 1)]
		public float minChargeInPercent = 0.3f;
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

		private void Awake() {
			_light = GetComponent<Light>();
			CurrentCharge = batteryCharge;      // в начале игры делаем полный заряд батареи
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
			while (!IsOn && CurrentCharge < batteryCharge) {
				CurrentCharge += changedCharge;
				if(OnChargeChanged != null) OnChargeChanged.Invoke(CurrentCharge, minChargeInPercent);
				yield return new WaitForSeconds(chargeSpeed);
			}
			if (CurrentCharge >= batteryCharge)
				CurrentCharge = batteryCharge;
		}

		/// <summary>
		/// Разряжает батарейку
		/// </summary>
		IEnumerator DecrementCharge() {
			while (IsOn && CurrentCharge > 0) {
				CurrentCharge -= changedCharge;
				if (OnChargeChanged != null) OnChargeChanged.Invoke(CurrentCharge, minChargeInPercent);
				yield return new WaitForSeconds(chargeSpeed);
			}
			if (CurrentCharge <= 0) CurrentCharge = 0;
		}
	}
}