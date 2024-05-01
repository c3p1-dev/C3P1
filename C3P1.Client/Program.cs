using Blazorise;
using Blazorise.Bootstrap5;
using Blazorise.Icons.FontAwesome;
using Blazr.RenderState.WASM;
using C3P1.Client.Services;
using C3P1.Client.Services.Admin;
using C3P1.Client.Services.Apps;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace C3P1.Client
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            // Add authentication related services
            builder.Services.AddAuthorizationCore();
            builder.Services.AddCascadingAuthenticationState();
            builder.Services.AddSingleton<AuthenticationStateProvider, PersistentAuthenticationStateProvider>();

            // Add blazorise related services
            builder.Services
                .AddBlazorise(options =>
                {
                    options.Immediate = true;
                    // unsafe way to store product token, TODO : figure out how to store secret strings in wasm
                    options.ProductToken = builder.Configuration["Blazorise:ProductToken"] ?? throw new InvalidOperationException("Blazorise license token 'ProductToken' not found."); ;
                })
                .AddBootstrap5Providers()
                .AddFontAwesomeIcons();

            // Add BlazR renderstate service
            builder.AddBlazrRenderStateWASMServices();

            // Add NavBreadCrumbService
            builder.Services.AddScoped<NavBreadcrumbService>();

            // Add HttpClient
            builder.Services.AddScoped(sp => new HttpClient
            {
                BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
            });

            // Add app services
            builder.Services.AddTransient<IManageUserService, ManageUserClientService>();
            builder.Services.AddTransient<ITasklistService, TasklistClientService>();
            builder.Services.AddTransient<ICatService, CatClientService>();

            // Add visualcarnet client service
            builder.Services.AddTransient<IVisualCarnetService, VisualCarnetClientService>();

            await builder.Build().RunAsync();
        }
    }
}
