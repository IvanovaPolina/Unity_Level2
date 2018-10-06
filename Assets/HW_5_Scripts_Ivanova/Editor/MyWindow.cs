using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class MyWindow : EditorWindow
{
	public GameObject ObjectInstantiate;
	string _nameObject = "Hello World";
	bool groupEnabled;
	bool _randomColor = true;
	int _countObject = 1;
	float _radius = 10;
	Color[] _colors = new Color[] { Color.green, Color.black, Color.blue, Color.clear, Color.cyan, Color.red, Color.yellow, Color.white, Color.red };
	float lenght = 1;	// длина спирали
	float density = 1;  // плотность спирали
	List<GameObject> temp = new List<GameObject>();

	[MenuItem("Homework/Create Spiral")]
	public static void ShowWindow() {
		// Отобразить существующий экземпляр окна. Если его нет, создаем
		EditorWindow.GetWindow<MyWindow>("Спираль");
	}

	void OnGUI() {
		// Здесь методы отрисовки схожи с методами в пользовательском интерфейсе, который вы разрабатывали на курсе “Unity3D.Уровень 1”
		GUILayout.Label("Базовые настройки", EditorStyles.boldLabel);
		ObjectInstantiate = EditorGUILayout.ObjectField("Объект который хотим вставить", ObjectInstantiate, typeof(GameObject), true) as GameObject;
		_nameObject = EditorGUILayout.TextField("Имя объекта", _nameObject);
		groupEnabled = EditorGUILayout.BeginToggleGroup("Дополнительные настройки", groupEnabled);
		_randomColor = EditorGUILayout.Toggle("Случайный цвет", _randomColor);
		_countObject = EditorGUILayout.IntSlider("Количество объектов", _countObject, 1, 200);
		_radius = EditorGUILayout.Slider("Радиус окружности", _radius, 10, 50);
		lenght = EditorGUILayout.Slider(new GUIContent("Количество витков спирали"), lenght, 1f, 10f);
		density = EditorGUILayout.Slider(new GUIContent("Плотность", "Насколько растянута спираль? [0.1 - сильно растянута, 5 - сильно сжата]"), density, 0.1f, 5f);
		EditorGUILayout.EndToggleGroup();
		if (GUILayout.Button("Создать объекты")) {
			if (ObjectInstantiate) {
				GameObject root = new GameObject("Root");
				for (int i = 0; i < _countObject; i++) { // Расставляем выбранный объект по окружности
					float angle = i * Mathf.PI * 2 / (_countObject / lenght);
					Vector3 pos = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * _radius;
					pos.y = i / density;
					GameObject temp = Instantiate(ObjectInstantiate, pos, ObjectInstantiate.transform.rotation) as GameObject;
					temp.name = _nameObject + "(" + i + ")";
					temp.transform.parent = root.transform;
					if (temp.GetComponent<Renderer>() && _randomColor)
						temp.GetComponent<Renderer>().material.color = _colors[Random.Range(0, _colors.Length - 1)];
					// Unity предупреждает о возможной утечке памяти и предлагает использовать sharedMaterial
				}
				temp.Add(root);
			}
		}
		if(GUILayout.Button("Удалить последний созданный объект")) {
			if(EditorUtility.DisplayDialog("Удаление последнего созданного объекта", "Вы уверены, что хотите удалить последнюю созданную спираль?", "Да", "Нет")) {
				if(temp.Count > 0) {
					int lastIndex = temp.Count - 1;
					DestroyImmediate(temp[lastIndex]);
					temp.RemoveAt(lastIndex);
				}
			}
		}
		if (GUILayout.Button("Удалить все созданные объекты")) {
			if (EditorUtility.DisplayDialog("Удаление всех созданных объектов", "Вы уверены, что хотите удалить все созданные спирали?", "Да", "Нет")) {
				if (temp.Count > 0) {
					for (int i = 0; i < temp.Count;) {
						DestroyImmediate(temp[i]);
						temp.RemoveAt(i);
					}
				}
			}
		}
	}
}
