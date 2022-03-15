using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Vantage.Web.Services;

namespace Vantage.Web
{
    public class Program
    {
        private const int Port = 44348;
        public static async Task Main(string[] args)
        {
            //await DebugDelayAsync();
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            var services = builder.Services;
            services.AddWebSocketClient(
                ChatClient.ClientName,
                client => client.Uri =
                    new Uri($"wss://localhost:{Port}/graphql"));

            services.AddScoped<StateService>();
            services.AddScoped<CommentService>();
            services.AddSingleton<ReplacementLinkService>();
            services.AddScoped(sp => new HttpClient {BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)})
                    .AddChatClient()
                    .ConfigureHttpClient(x => x.BaseAddress = new Uri($"https://localhost:{Port}/graphql"));
            await builder.Build().RunAsync();
        }
        
        private static async Task DebugDelayAsync()
        {
#if DEBUG
            await Task.Delay(5000);
#endif
        }

    }
}
