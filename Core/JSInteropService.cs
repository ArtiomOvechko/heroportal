using Microsoft.JSInterop;
using System.Threading.Tasks;


namespace Core
{
    class JSInteropService : IInteropService
    {
        readonly IJSRuntime _jsRuntime;

        public JSInteropService(IJSRuntime jSRuntime) 
        {
            _jsRuntime = jSRuntime;
        }

        public Task<string> GetLocalStorageItem(string key)
        {
            return _jsRuntime.InvokeAsync<string>("getLocalStorageItem", key).AsTask();
        }

        public Task SetLocalStorageItem(string key, string value)
        {
            return _jsRuntime.InvokeVoidAsync("setLocalStorageItem", key, value).AsTask();
        }
    }
}