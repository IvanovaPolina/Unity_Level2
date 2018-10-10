namespace Homework.Data
{
	public interface IDataProvider
	{
		void Save(GameObjectData objectData);
		GameObjectData Load();
		void SetOptions(string path, int filenameIndex);
	}
}