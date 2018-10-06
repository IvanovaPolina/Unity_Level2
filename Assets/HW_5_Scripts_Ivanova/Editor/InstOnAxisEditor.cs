using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(InstOnAxis))]
public class InstOnAxisEditor : Editor
{
	public override void OnInspectorGUI() {
		//DrawDefaultInspector();
		InstOnAxis instOnAxis = (InstOnAxis)target;
		Init();

		GUILayout.BeginHorizontal();
		bool createButton = GUILayout.Button("Создать объекты", EditorStyles.miniButtonLeft, GUILayout.MinHeight(10), GUILayout.MaxHeight(20));
		GUILayout.FlexibleSpace();
		bool destroyButton = GUILayout.Button("Удалить объекты", EditorStyles.miniButtonRight, GUILayout.MinHeight(10), GUILayout.MaxHeight(20));
		GUILayout.EndHorizontal();

		if (createButton) {
			instOnAxis.CreateObj();
			EditorWindow.mouseOverWindow.ShowNotification(new GUIContent("Все объекты созданы"));
		}

		if (destroyButton) {
			if (EditorUtility.DisplayDialog("Удаление объектов", "Вы уверены, что хотите удалить все созданные объекты?", "Да", "Нет")) {
				instOnAxis.DestroyObj();
				EditorWindow.mouseOverWindow.ShowNotification(new GUIContent("Созданные объекты удалены"));
			}
		}
	}

	/// <summary>
	/// Инициализирует и отображает поля скрипта
	/// </summary>
	private void Init() {
		SerializedProperty _countProp = serializedObject.FindProperty("_count");
		EditorGUILayout.IntSlider(_countProp, 2, 50, "Количество объектов");   // для слайдера
		//EditorGUILayout.PropertyField(_countProp, new GUIContent("Количество объектов"));	// для целочисленного значения
		SerializedProperty _offsetProp = serializedObject.FindProperty("_offset");
		EditorGUILayout.IntSlider(_offsetProp, -3, 3, "Отступ");
		SerializedProperty _objProp = serializedObject.FindProperty("_obj");
		EditorGUILayout.PropertyField(_objProp, new GUIContent("Префаб", "Префаб объекта, который хотим создать"));
		SerializedProperty _axisProp = serializedObject.FindProperty("_axis");
		EditorGUILayout.PropertyField(_axisProp, new GUIContent("Ось", "Ось, вдоль которой нужно создать объекты"));
		SerializedProperty _offsetFromAxisProp = serializedObject.FindProperty("offsetFromAxis");
		_offsetFromAxisProp.vector3IntValue = EditorGUILayout.Vector3IntField("Смещение от оси", _offsetFromAxisProp.vector3IntValue);

		serializedObject.ApplyModifiedProperties(); // сохраняем изменения введенных значений
	}
}