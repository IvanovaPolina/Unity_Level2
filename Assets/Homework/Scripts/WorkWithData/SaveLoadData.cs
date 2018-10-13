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
			if (!Directory.Exists(path)) Directory.CreateDirectory(path);
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
			PlayerModel player = PlayerModel.LocalPlayer;
			var playerData = new GameObjectData() {
				name = player.Name,
				HP = player.CurrentHealth,
				position = new GameObjectData.Vector3(player.Position.x, player.Position.y, player.Position.z),
				quaternion = new GameObjectData.Quaternion(player.Rotation.x, player.Rotation.y, player.Rotation.z, player.Rotation.w),
				scale = new GameObjectData.Vector3(player.Scale.x, player.Scale.y, player.Scale.z)
			};
			dataManager.SetOptions(path, 1);
			dataManager.Save(playerData);
		}

		public void Load() {
			dataManager.SetOptions(path, 1);
			var loaded = dataManager.Load();

			PlayerModel player = PlayerModel.LocalPlayer;
			player.Name = loaded.name;
			player.CurrentHealth = loaded.HP;
			player.Position = new Vector3(loaded.position.x, loaded.position.y, loaded.position.z);
			player.Rotation = new Quaternion(loaded.quaternion.x, loaded.quaternion.y, loaded.quaternion.z, loaded.quaternion.w);
			player.Scale = new Vector3(loaded.scale.x, loaded.scale.y, loaded.scale.z);
			//PlayerModel.LocalPlayer.IsVisible = playerLoaded.isVisible; // оружие становится isVisible = false, поэтому заменю вскоре на подстчет очков
			Debug.Log(loaded);

		}

		#region Old
		//public void Save() {
		//	var allObjects = (GameObject[])FindObjectsOfType(typeof(ISaveLoadObject)); // находим все объекты, реализующие интерфейс
		//	if (allObjects != null && allObjects.Length > 0) {
		//		string currentPath = Path.Combine(path, "ourSaveFolderIndex");  // прописываем путь текущего сохранения (в дальнейшем брать номер папки для сохранения в слоты)
		//		if (!Directory.Exists(currentPath))
		//			Directory.CreateDirectory(currentPath);
		//		foreach (var obj in allObjects) {
		//			dataManager.SetOptions(currentPath, obj.GetInstanceID());
		//			dataManager.Save(obj.GetComponent<ISaveLoadObject>().ObjectData);
		//		}
		//	}
		//}

		//public void Load() {
		//	string currentPath = Path.Combine(path, "ourSaveFolderIndex");  // идем в папку сохранения для текущего слота
		//	DirectoryInfo info = new DirectoryInfo(currentPath);
		//	if (info == null) return;
		//	FileInfo[] files = info.GetFiles();   // берем все файлы, которые есть в этой папке
		//	if (files == null || files.Length == 0) return;
		//	foreach (var file in files) {
		//		string[] filename = file.Name.Split(new char[] { '_' });
		//		int instanceID = int.Parse(filename[1]);		// получаем InstanceID объекта
		//		dataManager.SetOptions(currentPath, instanceID);
		//		var loaded = dataManager.Load();    // загружаем данные объекта
		//		GameObject obj = (GameObject)UnityEditor.EditorUtility.InstanceIDToObject(instanceID);
		//		if (!obj) {	// проверяем существование объекта с данным instanceID на сцене
		//			// берем префаб из ресурсов с таким же именем, как loaded.name

		//			// спавним его с загруженными статами
		//		} else if(obj) {
		//			obj.name = loaded.name;
		//			if (obj == PlayerModel.LocalPlayer.InstanceObject)
		//				PlayerModel.LocalPlayer.CurrentHealth = loaded.HP;
		//			obj.transform.position = new Vector3(loaded.position.x, loaded.position.y, loaded.position.z);
		//			obj.transform.rotation = new Quaternion(loaded.quaternion.x, loaded.quaternion.y, loaded.quaternion.z, loaded.quaternion.w);
		//			obj.transform.localScale = new Vector3(loaded.scale.x, loaded.scale.y, loaded.scale.z);
		//		}
		//	}
		//}
		#endregion
	}
}