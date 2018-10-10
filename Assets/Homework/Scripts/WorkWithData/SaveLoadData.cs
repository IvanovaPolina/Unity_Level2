using System.IO;
using UnityEngine;

namespace Homework.Data
{
	public class SaveLoadData : MonoBehaviour
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
			#region RightSolve
			PlayerModel player = PlayerModel.LocalPlayer;
			var playerData = new GameObjectData() {
				name = player.Name,
				HP = player.CurrentHealth,
				position = new GameObjectData.Vector3(player.Position.x, player.Position.y, player.Position.z),
				quaternion = new GameObjectData.Quaternion(player.Rotation.x, player.Rotation.y, player.Rotation.z, player.Rotation.w),
				scale = new GameObjectData.Vector3(player.Scale.x, player.Scale.y, player.Scale.z)
			};
			dataManager.SetOptions(path, player.GetInstanceID());
			dataManager.Save(playerData);
			#endregion

			//var allObjects = (ISaveLoadObject[])FindObjectsOfType(typeof(ISaveLoadObject));	// находим все объекты, реализующие интерфейс
			//if (allObjects != null && allObjects.Length > 0) {
			//	string currentPath = Path.Combine(path, "ourSaveFolderIndex");	// прописываем путь текущего сохранения (в дальнейшем брать номер папки для сохранения в слоты)
			//	dataManager.SetOptions(currentPath);
			//	foreach (var obj in allObjects) {
			//		dataManager.Save(obj.ObjectData);	// сюда передавать массив объектов! (или лист, чтобы подстроиться под Json)
			//	}
			//}
		}

		public void Load() {
			var allObjects = (GameObject[])FindObjectsOfType(typeof(ISaveLoadObject)); // находим все объекты, реализующие интерфейс
			if (allObjects != null && allObjects.Length > 0) {
				string currentPath = Path.Combine(path, "ourSaveFolderIndex");  // прописываем путь текущего сохранения (в дальнейшем брать номер папки для сохранения в слоты)
				foreach (var obj in allObjects) {
					dataManager.SetOptions(currentPath, obj.GetInstanceID());
					var loaded = dataManager.Load();
				}
			}

			//dataManager.SetOptions(path, );
			//var loaded = dataManager.Load();

			//PlayerModel player = PlayerModel.LocalPlayer;
			//player.Name = loaded.name;
			//player.CurrentHealth = loaded.HP;
			//player.Position = new Vector3(loaded.position.x, loaded.position.y, loaded.position.z);
			//player.Rotation = new Quaternion(loaded.quaternion.x, loaded.quaternion.y, loaded.quaternion.z, loaded.quaternion.w);
			//player.Scale = new Vector3(loaded.scale.x, loaded.scale.y, loaded.scale.z);
			////PlayerModel.LocalPlayer.IsVisible = playerLoaded.isVisible; // оружие становится isVisible = false, поэтому заменю вскоре на подстчет очков
			//Debug.Log(loaded);
		}
	}
}