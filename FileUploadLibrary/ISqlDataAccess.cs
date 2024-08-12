

namespace FileUploadLibrary
{
    public interface ISqlDataAccess
    {
        Task<List<T>> LoadData<T>(string storeProc, string connectionName, object? parameter);
        Task SaveData(string storeProc, string connectionName, object parameter);
    }
}