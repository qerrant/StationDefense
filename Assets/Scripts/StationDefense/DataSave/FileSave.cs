using System.IO;
using UnityEngine;

namespace StationDefense.DataSave
{
	public abstract class FileSave<T> : IDataSaver<T> where T : IDataStore
	{
		protected readonly string m_Filename;

		protected FileSave(string filename)
		{
			m_Filename = GetFinalSaveFilename(filename);
		}

		public abstract void Save(T data);

		public abstract bool Load(out T data);

		public void Delete()
		{
			File.Delete(m_Filename);
		}

		public static string GetFinalSaveFilename(string baseFilename)
		{
			return string.Format("{0}/{1}", Application.persistentDataPath, baseFilename);
		}

		protected virtual StreamWriter GetWriteStream()
		{
			return new StreamWriter(new FileStream(m_Filename, FileMode.Create));
		}

		protected virtual StreamReader GetReadStream()
		{
			return new StreamReader(new FileStream(m_Filename, FileMode.Open));
		}
	}
}