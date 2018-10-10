using Homework.Data;
using UnityEngine;

namespace Homework
{
	/// <summary>
	/// Этот интерфейс реализуют объекты, чьи данные попадают в файл сохранения
	/// </summary>
	public interface ISaveLoadObject
	{
		GameObjectData ObjectData { get; set; }
		Component GameObjectScript { get; }
	}
}