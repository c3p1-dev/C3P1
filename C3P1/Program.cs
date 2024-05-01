using Blazorise;
using Blazorise.Bootstrap5;
using Blazorise.Icons.FontAwesome;
using Blazr.RenderState.Server;
using C3P1.Client.Services;
using C3P1.Client.Services.Admin;
using C3P1.Client.Services.Apps;
using C3P1.Components;
using C3P1.Components.Account;
using C3P1.Data;
using C3P1.Services.Admin;
using C3P1.Services.Apps;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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

            // Default Authentication setup
            /*builder.Services.AddAuthentication(options =>
                {
                    options.DefaultScheme = IdentityConstants.ApplicationScheme;
                    options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
                })*/

            // Add Authentication with smart DefaultScheme, determined on runtime.
            builder.Services.AddAuthentication(sharedOptions =>
                {
                    sharedOptions.DefaultScheme = "smart";
                    sharedOptions.DefaultChallengeScheme = "smart";
                })
                .AddPolicyScheme("smart", "Authorization Bearer or Identity cookies", options =>
                {
                    options.ForwardDefaultSelector = context =>
                    {
                        var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();
                        if (authHeader?.StartsWith("Bearer ") == true)
                        {
                            return JwtBearerDefaults.AuthenticationScheme;
                        }
                        return IdentityConstants.ApplicationScheme;
                    };
                })
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = true;

                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidAudience = builder.Configuration["JWT:ValidAudience"],
                        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]!)),
                        RoleClaimType = ClaimTypes.Role
                    };

                })
                .AddIdentityCookies();

            // Not needed with smart determining of AuthenticationScheme on runtime
            // make [Authorize] use JwtBearer and IdentityCookies
            /*builder.Services.AddAuthorization(options =>
                {
                    options.DefaultPolicy = new AuthorizationPolicyBuilder()
                        .RequireAuthenticatedUser()
                        .AddAuthenticationSchemes(IdentityConstants.ApplicationScheme, JwtBearerDefaults.AuthenticationScheme)
                        .Build();
                });
            */

            // Add main app db context
            var connectionString = builder.Configuration["ConnectionStrings:DefaultConnection"] ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddIdentityCore<AppUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddSignInManager()
                .AddDefaultTokenProviders();

            builder.Services.AddSingleton<IEmailSender<AppUser>, IdentityNoOpEmailSender>();

            // Add Visual Carnet context
            var visualCarnetConnectionString = builder.Configuration["ConnectionStrings:VisualCarnetConnection"] ?? throw new InvalidOperationException("Connection string 'VisualCarnetConnection' not found.");
            builder.Services.AddDbContext<VisualCarnetContext>(options =>
                options.UseSqlite(visualCarnetConnectionString));

            // Add Blazorise related services
            builder.Services
                .AddBlazorise(options =>
                {
                    options.Immediate = true;
                    options.ProductToken = builder.Configuration["Blazorise:ProductToken"] ?? throw new InvalidOperationException("Blazorise license token 'ProductToken' not found.");
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
            builder.Services.AddTransient<ICatService, CatServerService>();

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
            app.UseAuthentication();
            app.UseAuthorization();

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

            // Manage 404
            app.UseStatusCodePagesWithReExecute("/error/errnotfound", "?statusCode={0}").UseAntiforgery();

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
