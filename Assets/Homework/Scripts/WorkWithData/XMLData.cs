using System.IO;
using System.Xml;
using UnityEngine;

namespace Homework.Data
{
	public class XMLData : IDataProvider
	{
		private string path;

		public GameObjectData Load() {
			var objData = new GameObjectData();
			if (!File.Exists(path)) {
				Debug.LogFormat("File is not found! Path: " + path);
				return objData;
			}
			string key;
			using (XmlTextReader reader = new XmlTextReader(path)) {
				while (reader.Read()) {
					key = "name";
					if (reader.IsStartElement(key)) {
						objData.name = reader.GetAttribute("value");
					}
					key = "HP";
					if (reader.IsStartElement(key)) {
						if (!float.TryParse(reader.GetAttribute("value"), out objData.HP)) {
							objData.HP = 100f;
							Debug.LogWarning("Player HP is not float! Check here: " + path);
						}
					}
					key = "position";
					if (reader.IsStartElement(key))
						objData.position = LoadVector3Node(key, reader);
					key = "quaternion";
					if (reader.IsStartElement(key))
						objData.quaternion = LoadQuaternionNode(key, reader);
					key = "scale";
					if (reader.IsStartElement(key))
						objData.scale = LoadVector3Node(key, reader);
				}
			}
			Debug.Log("XMLData loaded successfully");
			return objData;
		}

		private GameObjectData.Vector3 LoadVector3Node(string keyNode, XmlTextReader reader) {
			var objV3Data = new GameObjectData.Vector3();
			LoadCoord(reader, keyNode, "x", out objV3Data.x);
			LoadCoord(reader, keyNode, "y", out objV3Data.y);
			LoadCoord(reader, keyNode, "z", out objV3Data.z);
			return objV3Data;
		}

		private GameObjectData.Quaternion LoadQuaternionNode(string keyNode, XmlTextReader reader) {
			var objQData = new GameObjectData.Quaternion();
			LoadCoord(reader, keyNode, "x", out objQData.x);
			LoadCoord(reader, keyNode, "y", out objQData.y);
			LoadCoord(reader, keyNode, "z", out objQData.z);
			LoadCoord(reader, keyNode, "w", out objQData.w);
			return objQData;
		}

		private void LoadCoord(XmlTextReader reader, string keyNode, string key, out float coord) {
			coord = 1f;
			reader.Read();
			if (reader.IsStartElement(key))
				if (!float.TryParse(reader.GetAttribute("value"), out coord))
					Debug.LogWarningFormat("Object {0}.{1} is not float! Check here: {2}", keyNode, key, path);
		}

		public void Save(GameObjectData objData) {
			var xmlDoc = new XmlDocument();
			XmlNode rootNode = xmlDoc.CreateElement("GameObjectData");
			xmlDoc.AppendChild(rootNode);

			var element = xmlDoc.CreateElement("name");
			element.SetAttribute("value", objData.name);
			rootNode.AppendChild(element);

			element = xmlDoc.CreateElement("HP");
			element.SetAttribute("value", objData.HP.ToString());
			rootNode.AppendChild(element);

			SaveVector3Node(xmlDoc, rootNode, element, objData.position, "position");
			SaveQuaternionNode(xmlDoc, rootNode, element, objData.quaternion, "quaternion");
			SaveVector3Node(xmlDoc, rootNode, element, objData.scale, "scale");

			xmlDoc.Save(path);
			Debug.Log("XMLData saved successfully");
		}

		private void SaveVector3Node(XmlDocument xmlDoc, XmlNode rootNode, XmlElement element, GameObjectData.Vector3 objV3Data, string nameOfNode) {
			XmlNode vector3Node = xmlDoc.CreateElement(nameOfNode);
			rootNode.AppendChild(vector3Node);

			SaveCoord(element, xmlDoc, "x", objV3Data.x, vector3Node);
			SaveCoord(element, xmlDoc, "y", objV3Data.y, vector3Node);
			SaveCoord(element, xmlDoc, "z", objV3Data.z, vector3Node);
		}

		private void SaveQuaternionNode(XmlDocument xmlDoc, XmlNode rootNode, XmlElement element, GameObjectData.Quaternion objQData, string nameOfNode) {
			XmlNode quaternionNode = xmlDoc.CreateElement(nameOfNode);
			rootNode.AppendChild(quaternionNode);

			SaveCoord(element, xmlDoc, "x", objQData.x, quaternionNode);
			SaveCoord(element, xmlDoc, "y", objQData.y, quaternionNode);
			SaveCoord(element, xmlDoc, "z", objQData.z, quaternionNode);
			SaveCoord(element, xmlDoc, "w", objQData.w, quaternionNode);
		}

		private void SaveCoord(XmlElement element, XmlDocument xmlDoc, string key, float coord, XmlNode nodeName) {
			element = xmlDoc.CreateElement(key);
			element.SetAttribute("value", coord.ToString());
			nodeName.AppendChild(element);
		}

		public void SetOptions(string path, int filenameIndex) {
			this.path = Path.Combine(path, "XMLData_" + filenameIndex + ".xml");
		}
	}
}