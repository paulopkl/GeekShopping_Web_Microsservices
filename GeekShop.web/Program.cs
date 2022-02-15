using GeekShop.web.Services;
using GeekShop.web.Services.IServices;
using Microsoft.AspNetCore.Authentication;

var builder = WebApplication.CreateBuilder(args);

    // Adding client HttpClient to service ProductService
    builder.Services.AddHttpClient<IProductService, ProductService>(
        c => c.BaseAddress = new Uri(builder.Configuration["ServiceUrls:ProductAPI"])
    );

    // Adding client HttpClient to service CartService
    builder.Services.AddHttpClient<ICartService, CartService>(
        c => c.BaseAddress = new Uri(builder.Configuration["ServiceUrls:CartAPI"])
    );

    // Adding client HttpClient to service CartService
    builder.Services.AddHttpClient<ICouponService, CouponService>(
        c => c.BaseAddress = new Uri(builder.Configuration["ServiceUrls:CouponAPI"])
    );


// Add services to the container.
builder.Services.AddControllersWithViews();

    builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = "Cookies";
        options.DefaultChallengeScheme = "oidc";
    })
        .AddCookie("Cookies", c => c.ExpireTimeSpan = TimeSpan.FromMinutes(10))
        .AddOpenIdConnect(
            "oidc",
            options =>
            {
                options.Authority = builder.Configuration["ServiceUrls:IdentityServer"];
                options.GetClaimsFromUserInfoEndpoint = true;
                options.ClientId = builder.Configuration["ClientId"];
                options.ClientSecret = "[SecretKey]";
                options.ResponseType = "code";
                options.ClaimActions.MapJsonKey("role", "role", "role");
                options.ClaimActions.MapJsonKey("sub", "sub", "sub");
                options.TokenValidationParameters.NameClaimType = "name";
                options.TokenValidationParameters.RoleClaimType = "role";
                options.Scope.Add(builder.Configuration["ClientId"]);
                options.SaveTokens = true;
            }
        );

var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }
    else 
    {
        app.UseExceptionHandler("/Home/Error");
    }

app.UseHttpsRedirection();
app.UseStaticFiles();
    app.UseRouting();
    app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
