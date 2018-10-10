using System.IO;
using UnityEngine;

namespace Homework.Data
{
	public class StreamData : IDataProvider
	{
		private string path;

		public GameObjectData Load() {
			var objData = new GameObjectData();
			if (!File.Exists(path)) {
				Debug.LogFormat("File is not found! Path: " + path);
				return objData;
			}
			using (StreamReader sr = new StreamReader(path)) {
				while (!sr.EndOfStream) {
					objData.name = sr.ReadLine();
					ReadFloat(sr, out objData.HP, 100f);
					ReadFloat(sr, out objData.position.x, 0);
					ReadFloat(sr, out objData.position.y, 5f);
					ReadFloat(sr, out objData.position.z, 0);
					ReadFloat(sr, out objData.quaternion.x, 0);
					ReadFloat(sr, out objData.quaternion.y, 0);
					ReadFloat(sr, out objData.quaternion.z, 0);
					ReadFloat(sr, out objData.quaternion.w, 0);
					ReadFloat(sr, out objData.scale.x, 1f);
					ReadFloat(sr, out objData.scale.y, 1f);
					ReadFloat(sr, out objData.scale.z, 1f);
				}
			}
			Debug.Log("StreamData loaded successfully");
			return objData;
		}

		private void ReadFloat(StreamReader sr, out float value, float defaultValue) {
			if (!float.TryParse(sr.ReadLine(), out value)) {
				value = defaultValue;
				Debug.LogWarningFormat("Data is not float! Check here: {1}", path);
			}
		}

		public void Save(GameObjectData objData) {
			using (StreamWriter sw = new StreamWriter(path)) {
				sw.WriteLine(objData.name);
				sw.WriteLine(objData.HP);
				sw.WriteLine(objData.position.x);
				sw.WriteLine(objData.position.y);
				sw.WriteLine(objData.position.z);
				sw.WriteLine(objData.quaternion.x);
				sw.WriteLine(objData.quaternion.y);
				sw.WriteLine(objData.quaternion.z);
				sw.WriteLine(objData.quaternion.w);
				sw.WriteLine(objData.scale.x);
				sw.WriteLine(objData.scale.y);
				sw.WriteLine(objData.scale.z);
			}
			Debug.Log("StreamData saved successfully");
		}

		public void SetOptions(string path, int filenameIndex) {
			this.path = Path.Combine(path, "StreamData_" + filenameIndex + ".txt");
		}
	}
}