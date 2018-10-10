using UnityEngine;

namespace Homework.Data
{
	public class PlayerPrefsData : IDataProvider
	{
		public GameObjectData Load() {
			var objData = new GameObjectData();
			objData.name = PlayerPrefs.GetString("name", objData.name);
			objData.HP = PlayerPrefs.GetFloat("HP", objData.HP);
			objData.position.x = PlayerPrefs.GetFloat("Pos.x", objData.position.x);
			objData.position.y = PlayerPrefs.GetFloat("Pos.y", objData.position.y);
			objData.position.z = PlayerPrefs.GetFloat("Pos.z", objData.position.z);
			objData.quaternion.x = PlayerPrefs.GetFloat("Quaternion.x", objData.quaternion.x);
			objData.quaternion.y = PlayerPrefs.GetFloat("Quaternion.y", objData.quaternion.y);
			objData.quaternion.z = PlayerPrefs.GetFloat("Quaternion.z", objData.quaternion.z);
			objData.quaternion.w = PlayerPrefs.GetFloat("Quaternion.w", objData.quaternion.w);
			objData.scale.x = PlayerPrefs.GetFloat("Scale.x", objData.scale.x);
			objData.scale.y = PlayerPrefs.GetFloat("Scale.y", objData.scale.y);
			objData.scale.z = PlayerPrefs.GetFloat("Scale.z", objData.scale.z);

			Debug.Log("PlayerPrefsData loaded successfully");
			return objData;
		}

		public void Save(GameObjectData objData) {
			PlayerPrefs.SetString("name", objData.name);
			PlayerPrefs.SetFloat("HP", objData.HP);
			PlayerPrefs.SetFloat("Pos.x", objData.position.x);
			PlayerPrefs.SetFloat("Pos.y", objData.position.y);
			PlayerPrefs.SetFloat("Pos.z", objData.position.z);
			PlayerPrefs.SetFloat("Quaternion.x", objData.quaternion.x);
			PlayerPrefs.SetFloat("Quaternion.y", objData.quaternion.y);
			PlayerPrefs.SetFloat("Quaternion.z", objData.quaternion.z);
			PlayerPrefs.SetFloat("Quaternion.w", objData.quaternion.w);
			PlayerPrefs.SetFloat("Scale.x", objData.scale.x);
			PlayerPrefs.SetFloat("Scale.y", objData.scale.y);
			PlayerPrefs.SetFloat("Scale.z", objData.scale.z);

			PlayerPrefs.Save();
			Debug.Log("PlayerPrefsData saved successfully");
		}

		public void SetOptions(string path, int filenameIndex) {

		}
	}
}