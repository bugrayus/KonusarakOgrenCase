using System.Text;
using KonusarakOgrenCase.Application.Abstract;
using KonusarakOgrenCase.Application.Common;
using KonusarakOgrenCase.Application.Concrete;
using KonusarakOgrenCase.Persistence;
using KonusarakOgrenCase.Persistence.Abstract;
using KonusarakOgrenCase.Persistence.Concrete;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
const string corsAll = "all";

var configuration = builder.Configuration;
var appSettingsSection = configuration.GetSection("AppSettings");
builder.Services.Configure<AppSettings>(appSettingsSection);
var key = Encoding.ASCII.GetBytes(builder.Configuration["AppSettings:Secret"]);

builder.Services.AddDbContext<KonusarakOgrenCaseContext>
    (options => options.UseInMemoryDatabase("KonusarakOgren"));

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddScoped<Token>();

builder.Services.AddScoped<ValidateModelAttribute>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(corsAll,
        policy =>
        {
            policy
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod()
                .SetIsOriginAllowed(origin => true);
        });
});

builder.Services.AddControllers();

builder.Services.AddHttpContextAccessor();

builder.Services.AddDataProtection();

builder.Services.AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(x =>
    {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = false; });

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "KonusarakOgrenCase",
        Description = "",
        Contact = new OpenApiContact
        {
            Name = "Bugra Durukan",
            Email = "bugray34@gmail.com"
        }
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description =
            "JWT Authorization header using the Bearer scheme. \r\n\r\n " +
            "Enter 'Bearer' [space] and then user token in the text input below.\r\n\r\n " +
            "Example: \"Bearer 12345abcdef\""
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

var app = builder.Build();

if (app.Environment.IsDevelopment()) app.UseDeveloperExceptionPage();

app.UseSwagger();

app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "KonusarakOgrenCase v1"));

app.UseMiddleware(typeof(ErrorHandler));

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors(corsAll);

app.UseAuthentication();

app.UseAuthorization();

app.UseMiddleware<Jwt>();

app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

app.MapControllers();

using var scope = app.Services.CreateScope();
using var context = scope.ServiceProvider.GetService<KonusarakOgrenCaseContext>();
context?.Database.EnsureCreated();

app.Run();