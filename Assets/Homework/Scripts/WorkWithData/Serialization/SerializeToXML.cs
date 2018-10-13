using System.IO;
using System.Xml.Serialization;

namespace Homework.Data.Serialization
{
	public static class SerializeToXML
	{
		private static XmlSerializer _formatter;

		static SerializeToXML() {
			_formatter = new XmlSerializer(typeof(GameObjectData[]));
		}

		public static void Save(GameObjectData[] objs, string path) {
			if (objs == null && !string.IsNullOrEmpty(path)) return;
			if (objs.Length <= 0) return;
			using (FileStream fs = new FileStream(path, FileMode.Create)) {
				_formatter.Serialize(fs, objs);
			}
		}

		public static GameObjectData[] Load(string path) {
			GameObjectData[] result;
			if (!File.Exists(path)) return default(GameObjectData[]);
			using (FileStream fs = new FileStream(path, FileMode.Open)) {
				result = (GameObjectData[])_formatter.Deserialize(fs);
			}
			return result;
		}
	}
}
