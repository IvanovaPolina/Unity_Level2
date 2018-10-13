using System.IO;
using UnityEngine;

namespace Homework.Data
{
	public class JsonData : IDataProvider
	{
		private string path;

		public GameObjectData Load() {
			var objData = new GameObjectData();
			if (!File.Exists(path)) {
				Debug.LogFormat("File is not found! Path: " + path);
				return objData;
			}
			var str = File.ReadAllText(path);
			objData = JsonUtility.FromJson<GameObjectData>(str);
			Debug.Log("JsonData loaded successfully");
			return objData;
		}

		public void Save(GameObjectData objData) {
			var str = JsonUtility.ToJson(objData, true);
			File.WriteAllText(path, str);
			Debug.Log("JsonData saved successfully");
		}

		public void SetOptions(string path, int filenameIndex) {
			this.path = Path.Combine(path, "JsonData_" + filenameIndex + ".txt");
		}
	}
}