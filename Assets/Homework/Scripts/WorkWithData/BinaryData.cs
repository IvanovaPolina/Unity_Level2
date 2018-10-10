using System.IO;
using UnityEngine;

namespace Homework.Data
{
	public class BinaryData : IDataProvider
	{
		private string path;

		public GameObjectData Load() {
			var objData = new GameObjectData();
			if (!File.Exists(path)) {
				Debug.LogFormat("File is not found! Path: " + path);
				return objData;
			}
			using (BinaryReader br = new BinaryReader(File.Open(path, FileMode.Open))) {
				try {
					while (br.PeekChar() > -1) {
						objData.name = br.ReadString();
						objData.HP = br.ReadSingle();
						objData.position = new GameObjectData.Vector3() {
							x = br.ReadSingle(),
							y = br.ReadSingle(),
							z = br.ReadSingle()
						};
						objData.quaternion = new GameObjectData.Quaternion() {
							x = br.ReadSingle(),
							y = br.ReadSingle(),
							z = br.ReadSingle(),
							w = br.ReadSingle()
						};
						objData.scale = new GameObjectData.Vector3() {
							x = br.ReadSingle(),
							y = br.ReadSingle(),
							z = br.ReadSingle()
						};
					}
				}
				catch (System.Exception e) {
					Debug.LogWarning(e.Message);
				}
			}
			Debug.Log("BinaryData loaded successfully");
			return objData;
		}

		public void Save(GameObjectData objData) {
			using (BinaryWriter bw = new BinaryWriter(File.Open(path, FileMode.Create))) {
				bw.Write(objData.name);
				bw.Write(objData.HP);
				bw.Write(objData.position.x);
				bw.Write(objData.position.y);
				bw.Write(objData.position.z);
				bw.Write(objData.quaternion.x);
				bw.Write(objData.quaternion.y);
				bw.Write(objData.quaternion.z);
				bw.Write(objData.quaternion.w);
				bw.Write(objData.scale.x);
				bw.Write(objData.scale.y);
				bw.Write(objData.scale.z);
			}
			Debug.Log("BinaryData saved successfully");
		}

		public void SetOptions(string path, int filenameIndex) {
			this.path = Path.Combine(path, "BinaryData_" + filenameIndex + ".dat");
		}
	}
}