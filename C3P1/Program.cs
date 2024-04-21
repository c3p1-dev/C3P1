using Blazorise;
using C3P1.Client.Pages;
using C3P1.Components;
using C3P1.Components.Account;
using C3P1.Data;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Blazorise.Bootstrap5;
using Blazorise.Icons.FontAwesome;
using Blazr.RenderState.Server;
using C3P1.Client.Services;
using C3P1.Client.Services.Apps;
using C3P1.Services.Apps;
using C3P1.Client.Components.Admin.ManageUser;
using C3P1.Client.Services.Admin;
using C3P1.Services.Admin;

namespace C3P1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add webapi controllers services
            builder.Services.AddControllers();

            // Add Blazor related services
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents()
                .AddInteractiveWebAssemblyComponents();

            // Add authentication related services
            builder.Services.AddCascadingAuthenticationState();
            builder.Services.AddScoped<IdentityUserAccessor>();
            builder.Services.AddScoped<IdentityRedirectManager>();
            builder.Services.AddScoped<AuthenticationStateProvider, PersistingRevalidatingAuthenticationStateProvider>();

            builder.Services.AddAuthentication(options =>
                {
                    options.DefaultScheme = IdentityConstants.ApplicationScheme;
                    options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
                })
                .AddIdentityCookies();

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddIdentityCore<AppUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddSignInManager()
                .AddDefaultTokenProviders();

            builder.Services.AddSingleton<IEmailSender<AppUser>, IdentityNoOpEmailSender>();

            // Add Blazorise related services
            builder.Services
                .AddBlazorise(options =>
                {
                    options.Immediate = true;
                })
                .AddBootstrap5Providers()
                .AddFontAwesomeIcons();

            // Add Blazr Render state service
            builder.AddBlazrRenderStateServerServices();

            // Add NavBreadcrumbService
            builder.Services.AddScoped<NavBreadcrumbService>();

            // Add HttpClient
            builder.Services.AddHttpClient();

            // Add app services
            builder.Services.AddTransient<IManageUserService, ManageUserServerService>();
            builder.Services.AddTransient<ITasklistService, TasklistServerService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseWebAssemblyDebugging();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // Https redirection is not needed with Apache reverse proxy
            //app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            // Map webapi controllers
            app.MapControllers();

            // Map blazor
            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode()
                .AddInteractiveWebAssemblyRenderMode()
                .AddAdditionalAssemblies(typeof(Client._Imports).Assembly);

            // Add additional endpoints required by the Identity /Account Razor components.
            app.MapAdditionalIdentityEndpoints();

            // Create and seed database on first run
            using IServiceScope scope = app.Services.CreateScope();
            IServiceProvider services = scope.ServiceProvider;
            try
            {
                AppDbContext context = services.GetRequiredService<AppDbContext>();
                context.Database.Migrate();
                SeedData.InitializeAsync(services).Wait();
            }
            catch (Exception ex)
            {
                ILogger<Program> logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "Error occured creating or seeding the database");
            }

            // Run
            app.Run();
        }
    }
}
