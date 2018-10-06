using System.IO;
using System.Xml.Serialization;

public static class XML_Serializator
{
	private static XmlSerializer serializer;

	static XML_Serializator() {
		serializer = new XmlSerializer(typeof(SerializableGameObject[]));
	}

	/// <summary>
	/// Сохраняет объекты в XML-файл
	/// </summary>
	/// <param name="levels">Объекты, которые нужно сохранить</param>
	/// <param name="path">Путь к файлу сохранения</param>
	public static void Save(SerializableGameObject[] levelObjects, string path) {
		if (levelObjects == null || levelObjects.Length == 0 || string.IsNullOrEmpty(path)) return;
		using (FileStream fs = new FileStream(path, FileMode.Create)) {
			serializer.Serialize(fs, levelObjects);
		}
	}

	/// <summary>
	/// Загружает на сцену все объекты из XML-файла
	/// </summary>
	/// <param name="path">Путь к файлу</param>
	/// <returns></returns>
	public static SerializableGameObject[] Load(string path) {
		if (!File.Exists(path)) return null;
		SerializableGameObject[] result;
		using (FileStream fs = new FileStream(path, FileMode.Open)) {
			result = (SerializableGameObject[])serializer.Deserialize(fs);
		}
		return result;
	}
}
