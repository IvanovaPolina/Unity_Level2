using System.IO;
using UnityEngine;

public class JsonData : IDataProvider {

	private string path;

	public PlayerData Load() {
		var playerData = new PlayerData();
		if (!File.Exists(path)) {
			Debug.LogFormat("File is not found! Path: " + path);
			return playerData;
		}
		var str = File.ReadAllText(path);
		playerData = JsonUtility.FromJson<PlayerData>(str);
		Debug.Log("JsonData loaded successfully");
		return playerData;
	}

	public void Save(PlayerData playerData) {
		var str = JsonUtility.ToJson(playerData);
		File.WriteAllText(path, str);
		Debug.Log("JsonData saved successfully");
	}

	public void SetOptions(string path) {
		this.path = Path.Combine(path, "JsonData.txt");
	}
}
