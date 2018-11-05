using UnityEngine;

namespace Homework.Interfaces
{
	/// <summary>
	/// Этот интерфейс должен быть реализован у всех предметов, которые может подобрать игрок
	/// </summary>
	public interface ISelectable
	{
		bool CanPush { get; }
		void Push(Vector3 distance);
		float Distance { get; }
	}
}