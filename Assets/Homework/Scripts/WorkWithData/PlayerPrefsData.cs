using UnityEngine;

namespace Homework.Data
{
	public class PlayerPrefsData : IDataProvider
	{
		public PlayerData Load() {
			var playerData = new PlayerData();
			playerData.name = PlayerPrefs.GetString("name", playerData.name);
			playerData.HP = PlayerPrefs.GetFloat("HP", playerData.HP);

			Debug.Log("PlayerPrefsData loaded successfully");
			return playerData;
		}

		public void Save(PlayerData playerData) {
			PlayerPrefs.SetString("name", playerData.name);
			PlayerPrefs.SetFloat("HP", playerData.HP);

			PlayerPrefs.Save();
			Debug.Log("PlayerPrefsData saved successfully");
		}

		public void SetOptions(string path) {

		}
	}
}