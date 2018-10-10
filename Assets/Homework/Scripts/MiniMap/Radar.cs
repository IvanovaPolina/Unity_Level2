using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Homework.MiniMap
{
	public class Radar : MonoBehaviour
	{
		private Transform playerPos;	// Позиция игрока
		private readonly float mapScale = 2;
		public static List<RadarObject> RadObjects = new List<RadarObject>();
		private RectTransform rectTransform;
		private Vector3 iconRectScale = new Vector3(1, 1, 1);

		private void Start() {
			playerPos = PlayerModel.LocalPlayer.Transform;
			rectTransform = GetComponent<RectTransform>();
		}

		public static void RegisterRadarObject(GameObject o, Image i) {
			Image image = Instantiate(i);
			RadObjects.Add(new RadarObject { Owner = o, Icon = image });
		}

		public static void RemoveRadarObject(GameObject o) {
			List<RadarObject> newList = new List<RadarObject>();
			foreach (RadarObject t in RadObjects) {
				if (t.Owner == o) {		// если хотим удалить объект, и он имеется в списке
					Destroy(t.Icon);	// удаляем иконку этого объекта
					continue;		// двигаемся дальше по циклу, пропуская добавление в новый список
				}
				newList.Add(t);	// добавляем объект в новый список, если доходим до этого пункта
			}
			RadObjects.RemoveRange(0, RadObjects.Count);	// чистим старый список с объектами
			RadObjects.AddRange(newList);	// и формируем новый
		}

		private void DrawRadarDots() {	// Синхронизирует значки на миникарте с реальными объектами
			foreach (RadarObject radObject in RadObjects) {	// и содержит абсолютно непонятные вычисления...
				Vector3 radarPos = (radObject.Owner.transform.position - playerPos.position);
				float distToObject = Vector3.Distance(playerPos.position, radObject.Owner.transform.position) * mapScale;
				float deltay = Mathf.Atan2(radarPos.x, radarPos.z) * Mathf.Rad2Deg - 270 - playerPos.eulerAngles.y;
				radarPos.x = distToObject * Mathf.Cos(deltay * Mathf.Deg2Rad) * -1;
				radarPos.z = distToObject * Mathf.Sin(deltay * Mathf.Deg2Rad);
				radObject.Icon.rectTransform.SetParent(rectTransform);	// последние 3 строки поправила, т.к. оригинал оказался нерабочим, либо я сделала префаб не так...
				radObject.Icon.rectTransform.localScale = iconRectScale;
				radObject.Icon.rectTransform.anchoredPosition3D = new Vector3(radarPos.x, radarPos.z, 0);
			}
		}

		private void Update() {
			if (Time.frameCount % 3 == 0)
				DrawRadarDots();
		}
	}

	public class RadarObject
	{
		public Image Icon;
		public GameObject Owner;
	}
}