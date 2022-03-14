using System;
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
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            var services = builder.Services;

            services.AddWebSocketClient(
                ChatClient.ClientName,
                client => client.Uri =
                    new Uri($"wss://localhost:{Port}/graphql"));

            services.AddScoped<StateService>();

            services.AddScoped(sp => new HttpClient {BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)})
                    .AddChatClient()
                    .ConfigureHttpClient(x => x.BaseAddress = new Uri($"https://localhost:{Port}/graphql"));
            await builder.Build().RunAsync();
        }


    }
}
