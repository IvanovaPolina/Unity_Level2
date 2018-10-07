using UnityEngine;

public class PlayerPrefsData : IDataProvider
{
	public PlayerData Load() {
		var playerData = new PlayerData();
		playerData.name = PlayerPrefs.GetString("name", playerData.name);
		playerData.HP = PlayerPrefs.GetFloat("HP", playerData.HP);
		playerData.isVisible = bool.Parse(PlayerPrefs.GetString("isVisible", playerData.isVisible.ToString()));

		Debug.Log("PlayerPrefsData loaded successfully");
		return playerData;
	}

	public void Save(PlayerData playerData) {
		PlayerPrefs.SetString("name", playerData.name);
		PlayerPrefs.SetFloat("HP", playerData.HP);
		PlayerPrefs.SetString("isVisible", playerData.isVisible.ToString());

		PlayerPrefs.Save();
		Debug.Log("PlayerPrefsData saved successfully");
	}

	public void SetOptions(string path) {
		
	}
}
