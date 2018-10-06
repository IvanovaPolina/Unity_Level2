using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class SaveLoadLevelObjects
{
	[MenuItem("Homework/Save Level")]
	private static void Save() {
		string path = EditorUtility.SaveFilePanelInProject("Сохранить сцену", SceneManager.GetActiveScene().name, "xml", "");
		GameObject[] gameObjects = GameObject.FindObjectsOfType<GameObject>();
		List<SerializableGameObject> levelObjects = new List<SerializableGameObject>();
		foreach (var obj in gameObjects) {
			SerializableGameObject s = new SerializableGameObject() {
				name = obj.name,
				position = obj.transform.position,
				rotation = obj.transform.rotation,
				scale = obj.transform.localScale,
				layer = obj.layer
			};
			if (obj.GetComponent<Renderer>())
				s.color = obj.GetComponent<Renderer>().material.color;
			if (obj.GetComponent<Collider>())
				s.colliderType = obj.GetComponent<Collider>().GetType().FullName + ", UnityEngine";
			levelObjects.Add(s);
		}
		XML_Serializator.Save(levelObjects.ToArray(), path);
		Debug.Log("Сцена успешно сохранена!");
	}

	[MenuItem("Homework/Load Level")]
	private static void Load() {
		string path = EditorUtility.OpenFilePanel("Открыть сцену", Application.dataPath, "xml");
		var objects = XML_Serializator.Load(path);
		foreach (var obj in objects) {
			GameObject prefab = Resources.Load<GameObject>(obj.name);
			if(prefab == null) {
				Debug.LogWarningFormat("Объект {0} не найден в папке Resources и не был загружен", obj.name);
				continue;
			}
			var tempObj = GameObject.Instantiate(prefab, obj.position, obj.rotation);
			tempObj.transform.localScale = obj.scale;
			tempObj.name = obj.name;
			tempObj.layer = obj.layer;
			if (obj.color != Color.clear)
				tempObj.GetComponent<Renderer>().material.color = obj.color;
			AddCollider(tempObj, obj);
		}
		Debug.Log("Сцена успешно загружена!");
	}

	static void AddCollider(GameObject tempObj, SerializableGameObject obj) {
		System.Type colliderType = null;
		try {
			colliderType = System.Type.GetType(obj.colliderType);
		}
		catch (System.Exception) {
			Debug.LogFormat("Объект {0} не имеет коллайдера", obj.name);
		}
		if (colliderType != null)
			tempObj.AddComponent(colliderType);
	}
}
