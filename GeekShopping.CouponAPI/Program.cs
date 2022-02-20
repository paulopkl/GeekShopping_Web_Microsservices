using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using GeekShopping.CouponAPI.DB.Model.Context;
using GeekShopping.CouponAPI.Config;
using GeekShopping.CouponAPI.Repository;

var builder = WebApplication.CreateBuilder(args);

    // Establish connection to database
    string connection = builder.Configuration["MySQLConnection:MySQLConnectionString"];

    var conn = builder.Services.AddDbContext<MySQLContext>(
        options => options.UseMySql(connection, new MySqlServerVersion(new Version(15, 1)))
    );

    //services.AddDbContext<MySQLContext>(options => options.
    //    UseMySql(connection, new MySqlServerVersion(new Version(8, 0, 5))));

    // The Auto Mapper Object to Entity
    IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
    builder.Services.AddSingleton(mapper);
    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

    // Dependency injection
    // Injects ProductRepository to application
    builder.Services.AddScoped<ICouponRepository, CouponRepository>();

// Add services to the container.
builder.Services.AddControllers();

    // Configures authentication
    builder.Services.AddAuthentication("Bearer")
        .AddJwtBearer(
            "Bearer",
            options =>
            {
                options.Authority = "https://localhost:4435/";
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false
                };
            }
        );

    // Configures authorization
    builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy("ApiScope", policy =>
        {
            policy.RequireAuthenticatedUser();
            policy.RequireClaim("scope", "geek_shopping");
        });
    });

    // Configuring Swagger
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "GeekShopping.CouponAPI", Version = "v1" });
        c.EnableAnnotations();
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description = @"Enter 'Bearer' [space] and your token!",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer"
        });

        c.AddSecurityRequirement(
            new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Id = "Bearer",
                            Type = ReferenceType.SecurityScheme
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header,
                    },
                    new List<string> ()
                }
            }
        );
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
    app.UseRouting();
    app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
