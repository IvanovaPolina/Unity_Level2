using System.IO;
using UnityEngine;

namespace Homework.Data
{
	public class StreamData : IDataProvider
	{
		private string path;

		public PlayerData Load() {
			var playerData = new PlayerData();
			if (!File.Exists(path)) {
				Debug.LogFormat("File is not found! Path: " + path);
				return playerData;
			}
			using (StreamReader sr = new StreamReader(path)) {
				while (!sr.EndOfStream) {
					playerData.name = sr.ReadLine();
					if (!float.TryParse(sr.ReadLine(), out playerData.HP)) {
						playerData.HP = 100f;
						Debug.LogWarning("Player HP is not float! Check here: " + path);
					}
				}
			}
			Debug.Log("StreamData loaded successfully");
			return playerData;
		}

		public void Save(PlayerData playerData) {
			using (StreamWriter sw = new StreamWriter(path)) {
				sw.WriteLine(playerData.name);
				sw.WriteLine(playerData.HP);
			}
			Debug.Log("StreamData saved successfully");
		}

		public void SetOptions(string path) {
			this.path = Path.Combine(path, "StreamData.txt");
		}
	}
}