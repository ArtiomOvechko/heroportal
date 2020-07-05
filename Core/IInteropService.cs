using System.Threading.Tasks;


namespace Core
{
    interface IInteropService 
    {
        Task<string> GetLocalStorageItem(string key);

        Task SetLocalStorageItem(string key, string value);

        Task ConsoleLog(string value);

        Task InitUIComponents();
    }
}