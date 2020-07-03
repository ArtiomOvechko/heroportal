using System.Threading.Tasks;

namespace Core
{
    interface ILogger 
    {
        Task Info(string value);
    }   
}