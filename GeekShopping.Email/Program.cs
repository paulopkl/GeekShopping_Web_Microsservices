using GeekShopping.Email.DB.Model.Context;
using GeekShopping.Email.Repository;
using GeekShopping.Email.MessageConsumer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

    // Establish connection to database
    string connection = builder.Configuration["MySQLConnection:MySQLConnectionString"];
    var mysqlVersion = new MySqlServerVersion(new Version(15, 1));

    var conn = builder.Services.AddDbContext<MySQLContext>(
        options => options.UseMySql(connection, mysqlVersion)
    );

    var builderContext = new DbContextOptionsBuilder<MySQLContext>();
    builderContext.UseMySql(connection, mysqlVersion);

    // Add services to the container.
    builder.Services.AddSingleton(new EmailRepository(builderContext.Options));
    builder.Services.AddScoped<IEmailRepository, EmailRepository>();

    builder.Services.AddHostedService<RabbitMQPaymentConsumer>();

    // Add services to the container.
    builder.Services.AddControllers();

    // Configures authentication
    builder.Services.AddAuthentication("Bearer")
        .AddJwtBearer(
            "Bearer",
            options =>
            {
                    //options.Authority = builder.Configuration["Authority"];
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

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();

    // Configuring Swagger
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "GeekShopping.Email", Version = "v1" });
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description = @"Enter 'Bearer' [space] and your token!",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer"
        });

        c.AddSecurityRequirement(new OpenApiSecurityRequirement {
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
        });
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
    app.UseRouting();
    app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
