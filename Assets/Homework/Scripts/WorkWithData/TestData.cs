using Homework;
using System.IO;
using UnityEngine;

public class TestData : MonoBehaviour {

	public enum DataProviders
	{
		TXT,
		XML,
		JSON,
		PLAYER_PREFS,
		DAT
	}

	[SerializeField]
	private DataProviders provider = DataProviders.JSON;
	private DataManager dataManager;

	private void Start() {
		string path = Path.Combine(Application.dataPath, "Saves");
		var playerData = new PlayerData() {
			name = PlayerModel.LocalPlayer.Name,
			HP = PlayerModel.LocalPlayer.CurrentHealth,
			isVisible = PlayerModel.LocalPlayer.IsVisible
		};

		dataManager = new DataManager();
		switch (provider) {
			case DataProviders.TXT:
				dataManager.SetData<StreamData>();
				break;
			case DataProviders.XML:
				dataManager.SetData<XMLData>();
				break;
			case DataProviders.JSON:
				dataManager.SetData<JsonData>();
				break;
			case DataProviders.PLAYER_PREFS:
				dataManager.SetData<PlayerPrefsData>();
				break;
			case DataProviders.DAT:
				dataManager.SetData<BinaryData>();
				break;
		}

		if (dataManager == null) return;
		dataManager.SetOptions(path);
		dataManager.Save(playerData);

		var playerLoaded = dataManager.Load();
		Debug.Log(playerLoaded);
	}
}
