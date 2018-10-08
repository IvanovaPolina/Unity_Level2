using System.IO;
using System.Xml;
using UnityEngine;

namespace Homework.Data
{
	public class XMLData : IDataProvider
	{
		private string path;

		public PlayerData Load() {
			var playerData = new PlayerData();
			if (!File.Exists(path)) {
				Debug.LogFormat("File is not found! Path: " + path);
				return playerData;
			}
			string key;
			using (XmlTextReader reader = new XmlTextReader(path)) {
				while (reader.Read()) {
					key = "name";
					if (reader.IsStartElement(key)) {
						playerData.name = reader.GetAttribute("value");
					}
					key = "HP";
					if (reader.IsStartElement(key)) {
						if (!float.TryParse(reader.GetAttribute("value"), out playerData.HP)) {
							playerData.HP = 100f;
							Debug.LogWarning("Player HP is not float! Check here: " + path);
						}
					}
				}
			}
			Debug.Log("XMLData loaded successfully");
			return playerData;
		}

		public void Save(PlayerData playerData) {
			var xmlDoc = new XmlDocument();
			XmlNode rootNode = xmlDoc.CreateElement("PlayerData");
			xmlDoc.AppendChild(rootNode);

			var element = xmlDoc.CreateElement("name");
			element.SetAttribute("value", playerData.name);
			rootNode.AppendChild(element);

			element = xmlDoc.CreateElement("HP");
			element.SetAttribute("value", playerData.HP.ToString());
			rootNode.AppendChild(element);

			xmlDoc.Save(path);
			Debug.Log("XMLData saved successfully");
		}

		public void SetOptions(string path) {
			this.path = Path.Combine(path, "XMLData.xml");
		}
	}
}