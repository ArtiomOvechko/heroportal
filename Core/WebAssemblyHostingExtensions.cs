using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;


namespace Core
{
    static class WebAssemblyHostingExtensions
    {
        public static void AddHeroApi(this WebAssemblyHostBuilder builder) 
        {
            builder.Services.AddSingleton(typeof(IHeroApi), typeof(MockHeroApi));
        }

        public static void AddJSInterop(this WebAssemblyHostBuilder builder) 
        {
            builder.Services.AddSingleton(typeof(IInteropService), typeof(JSInteropService));
        }
    }
}