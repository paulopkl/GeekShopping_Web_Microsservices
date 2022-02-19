using GeekShopping.OrderAPI.DB.Model.Context;
using GeekShopping.OrderAPI.MessageConsumer;
using GeekShopping.OrderAPI.RabbitMQSender;
using GeekShopping.OrderAPI.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

    // Establish connection to database
    string connection = builder.Configuration["MySQLConnection:MySQLConnectionString"];
    var mysqlVersion = new MySqlServerVersion(new Version(15, 1));

    var conn = builder.Services.AddDbContext<MySQLContext>(
        options => options.UseMySql(connection, mysqlVersion)
    );

    // The Auto Mapper Object to Entity
    //IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
    //builder.Services.AddSingleton(mapper);
    //builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

    // Dependency injection
    // Injects ProductRepository to application
    //builder.Services.AddScoped<ICartRepository, CartRepository>();

    var builderContext = new DbContextOptionsBuilder<MySQLContext>();
    builderContext.UseMySql(connection, mysqlVersion);

    builder.Services.AddSingleton(new OrderRepository(builderContext.Options));

    builder.Services.AddHostedService<RabbitMQCheckoutConsumer>();
    builder.Services.AddHostedService<RabbitMQPaymentConsumer>();
    builder.Services.AddSingleton<IRabbitMQMessageSender, RabbitMQMessageSender>();

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
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "GeekShopping.OrderAPI", Version = "v1" });
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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GeekShopping.OrderAPI v1"));
}

app.UseHttpsRedirection();
    app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();