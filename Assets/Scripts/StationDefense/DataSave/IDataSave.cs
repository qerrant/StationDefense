namespace StationDefense.DataSave
{
	public interface IDataSaver<T> where T : IDataStore
	{
		void Save(T data);

		bool Load(out T data);

		void Delete();
	}
}
