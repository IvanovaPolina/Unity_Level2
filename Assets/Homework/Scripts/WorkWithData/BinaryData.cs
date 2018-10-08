using System.IO;
using UnityEngine;

public class BinaryData : IDataProvider
{
	private string path;

	public PlayerData Load() {
		var playerData = new PlayerData();
		if (!File.Exists(path)) {
			Debug.LogFormat("File is not found! Path: " + path);
			return playerData;
		}
		using (BinaryReader br = new BinaryReader(File.Open(path, FileMode.Open))) {
			try {
				while (br.PeekChar() > -1) {
					playerData.name = br.ReadString();
					playerData.HP = br.ReadSingle();
					playerData.isVisible = br.ReadBoolean();
				}
			}
			catch (System.Exception e) {
				Debug.LogWarning(e.Message);
			}
		}
		Debug.Log("BinaryData loaded successfully");
		return playerData;
	}

	public void Save(PlayerData playerData) {
		using (BinaryWriter bw = new BinaryWriter(File.Open(path, FileMode.OpenOrCreate))) {
			bw.Write(playerData.name);
			bw.Write(playerData.HP);
			bw.Write(playerData.isVisible);
		}
		Debug.Log("BinaryData saved successfully");
	}

	public void SetOptions(string path) {
		this.path = Path.Combine(path, "BinaryData.dat");
	}
}
