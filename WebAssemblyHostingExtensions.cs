using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;


namespace Core
{
    static class WebAssemblyHostingExtensions
    {
        public static void AddHeroApi(this WebAssemblyHostBuilder builder) 
        {
            builder.Services.AddSingleton(typeof(ILogger), typeof(BrowserLogger));
            builder.Services.AddScoped(typeof(IHeroApi), typeof(HeroApi));
        }

        public static void AddJSInterop(this WebAssemblyHostBuilder builder) 
        {
            builder.Services.AddSingleton(typeof(IInteropService), typeof(JSInteropService));
        }
    }
}