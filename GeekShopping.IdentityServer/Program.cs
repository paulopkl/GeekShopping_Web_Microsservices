using Duende.IdentityServer.Services;
using GeekShopping.IdentityServer.DB.Model;
using GeekShopping.IdentityServer.DB.Model.Context;
using GeekShopping.IdentityServer.Initializer;
using GeekShopping.IdentityServer.Models.Configuration;
using GeekShopping.IdentityServer.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
    string connection = builder.Configuration["MySQLConnection:MySQLConnectionString"];

    var conn = builder.Services.AddDbContext<MySQLContext>(
        options => options.UseMySql(connection, new MySqlServerVersion(new Version(15, 1)))
    );

    builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
        .AddEntityFrameworkStores<MySQLContext>()
        .AddDefaultTokenProviders();

    var identityBuilder = builder.Services.AddIdentityServer(options =>
    {
        options.Events.RaiseErrorEvents = true;
        options.Events.RaiseInformationEvents = true;
        options.Events.RaiseFailureEvents = true;
        options.Events.RaiseSuccessEvents = true;
        options.EmitStaticAudienceClaim = true;
    })
        .AddInMemoryIdentityResources(IdentityConfiguration.IdentityResources)
        .AddInMemoryApiScopes(IdentityConfiguration.ApiScopes)
        .AddInMemoryClients(IdentityConfiguration.Clients)
        .AddAspNetIdentity<ApplicationUser>();

    builder.Services.AddScoped<IDbInitializer, DbInitializer>();
    builder.Services.AddScoped<IProfileService, ProfileService>();

    identityBuilder.AddDeveloperSigningCredential();

builder.Services.AddControllersWithViews();

var app = builder.Build();

    //IDbInitializer dbInitializer = app.Services.GetRequiredService<IDbInitializer>();
    var scope = app.Services.CreateScope();
    var service = scope.ServiceProvider.GetService<IDbInitializer>();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

    app.UseHttpsRedirection();

app.UseStaticFiles();

    app.UseRouting();

    app.UseIdentityServer();

app.UseAuthorization();

    //dbInitializer.Initialize();
    service.Initialize();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);
app.Run();
