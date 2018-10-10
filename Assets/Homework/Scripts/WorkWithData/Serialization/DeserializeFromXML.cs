using System.Collections.Generic;
using UnityEngine;

namespace Homework.Data.Serialization
{
	public class DeserializeFromXML
	{
		private GameObject _prefab;

		public DeserializeFromXML(GameObject prefab) {
			_prefab = prefab;
		}

		public void Save(string path) {
			GameObject[] allObjs = (GameObject[])GameObject.FindObjectsOfType(typeof(ISaveLoadObject)); // находим все объекты, реализующие интерфейс
			List<GameObjectData> dataList = new List<GameObjectData>(); // список объектов, которые будем сохранять
			foreach (var o in allObjs) {    // определяем, 
				//if (o.GetComponent<ISaveLoadObject>().GameObjectScript == _prefab.GetComponent<ISaveLoadObject>().GameObjectScript) {
				if (UnityEditor.PrefabUtility.GetPrefabType(o) == UnityEditor.PrefabType.PrefabInstance) {
					if (UnityEditor.PrefabUtility.GetPrefabObject(o) == _prefab) {   // если объект получен из данного префаба
						var trans = o.transform;
						dataList.Add(new GameObjectData {
							name = o.name,
							HP = o == PlayerModel.LocalPlayer.InstanceObject ? PlayerModel.LocalPlayer.CurrentHealth : 0,
							position = trans.position,
							quaternion = trans.rotation,
							scale = trans.localScale
						});
					}
				}
			}
			SerializeToXML.Save(dataList.ToArray(), path);
		}

		public void Load(string path) {
			var obj = SerializeToXML.Load(path);
			foreach (var o in obj) {
				if (o.HP != 0) {
					PlayerModel.LocalPlayer.CurrentHealth = o.HP;
					PlayerModel.LocalPlayer.Position = o.position;
					PlayerModel.LocalPlayer.Rotation = o.quaternion;
					PlayerModel.LocalPlayer.Scale = o.scale;
					continue;
				}
				var tempObj = GameObject.Instantiate(_prefab, o.position, o.quaternion);
				tempObj.name = o.name;
				tempObj.transform.localScale = o.scale;
			}
		}
	}
}