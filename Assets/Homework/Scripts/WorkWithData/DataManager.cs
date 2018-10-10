namespace Homework.Data
{
	public class DataManager
	{
		private IDataProvider dataProvider;

		public void SetData<T>() where T : IDataProvider, new() {
			dataProvider = new T();
		}

		public void Save(GameObjectData objData) {
			if (dataProvider != null)
				dataProvider.Save(objData);
		}

		public GameObjectData Load() {
			if (dataProvider == null) return default(GameObjectData);
			return dataProvider.Load();
		}

		public void SetOptions(string path, int filenameIndex) {
			if (dataProvider != null)
				dataProvider.SetOptions(path, filenameIndex);
		}
	}
}