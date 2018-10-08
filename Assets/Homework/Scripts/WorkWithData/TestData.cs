using System.IO;
using UnityEngine;

namespace Homework.Data
{
	public class TestData : MonoBehaviour
	{
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
		private string path;

		private void Start() {
			path = Path.Combine(Application.dataPath, "Saves");
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
		}

		public void Save() {
			var playerData = new PlayerData() {
				name = PlayerModel.LocalPlayer.Name,
				HP = PlayerModel.LocalPlayer.CurrentHealth
			};
			dataManager.SetOptions(path);
			dataManager.Save(playerData);
		}

		public void Load() {
			dataManager.SetOptions(path);
			var playerLoaded = dataManager.Load();
			PlayerModel.LocalPlayer.Name = playerLoaded.name;
			PlayerModel.LocalPlayer.CurrentHealth = playerLoaded.HP;
			//PlayerModel.LocalPlayer.IsVisible = playerLoaded.isVisible; // оружие становится isVisible = false, поэтому заменю вскоре на подстчет очков
			Debug.Log(playerLoaded);
		}
	}
}