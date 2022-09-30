using System.Text;
using EM.InsurePlus.Data;
using EM.InsurePlus.Data.Contract;
using EM.InsurePlus.Data.Models.Identity;
using EM.InsurePlus.Repository;
using EM.InsurePlus.Repository.Contract;
using EM.InsurePlus.Services;
using EM.InsurePlus.Services.Contract;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "InsurePlus", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//Database
var connectionString = configuration["Data:DefaultConnection:ConnectionString"];

//Indentity
builder.Services.AddDbContext<StorageContext>(options =>
                                              options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddIdentity<User, Role>()
        .AddEntityFrameworkStores<StorageContext>()
        .AddDefaultTokenProviders();
builder.Services.AddAuthorization();
builder.Services.AddAuthentication();
builder.Services.AddTransient<ConfigurationManager>();
builder.Services.AddScoped<IStorageContext, StorageContext>();
builder.Services.AddScoped<IModelRegistrar, ModelRegistrar>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddTransient<IVehiclePolicyService, VehiclePolicyService>();
builder.Services.AddTransient<IVehiclePolicyRepository, VehiclePolicyRepository>();
builder.Services.AddTransient<IUserService, UserService>();

//builder.Services.Configure<AppConfigs>(configuration.GetSection(nameof(AppConfigs)));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
        });
});

//Social Auth
builder.Services.AddAuthentication(o =>
{
    o.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
})
                .AddJwtBearer(o =>
                {
                    o.RequireHttpsMetadata = false;
                    o.SaveToken = false;
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero,
                        ValidIssuer = configuration["JwtToken:Issuer"],
                        ValidAudience = configuration["JwtToken:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtToken:SecurityKey"]))
                    };
                })
.AddCookie()
.AddGoogle(g =>
{
    g.ClientId = configuration["AuthClient:Google:ClientId"];
    g.ClientSecret = configuration["AuthClient:Google:ClientSecret"];
    g.SaveTokens = true;
})
.AddFacebook(f =>
{
    f.AppId = configuration["AuthClient:Facebook:AppId"];
    f.AppSecret = configuration["AuthClient:Facebook:AppSecret"];
    f.SaveTokens = true;
})
.AddInstagram(i =>
{
    i.ClientId = configuration["AuthClient:Instagram:ClientId"];
    i.ClientSecret = configuration["AuthClient:Instagram:ClientSecret"];
    i.SaveTokens = true;
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<StorageContext>();
    dataContext.Database.Migrate();
}

app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();