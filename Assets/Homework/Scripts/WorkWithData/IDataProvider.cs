public interface IDataProvider
{
	void Save(PlayerData playerData);
	PlayerData Load();
	void SetOptions(string path);
}
