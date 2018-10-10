using UnityEngine;
using UnityEngine.UI;

namespace Homework.MiniMap
{
	public class MakeRadarObject : MonoBehaviour
	{
		[SerializeField]
		private Image icon;

		private void Start() {
			Radar.RegisterRadarObject(gameObject, icon);
		}

		private void OnDestroy() {
			Radar.RemoveRadarObject(gameObject);
		}
	}
}