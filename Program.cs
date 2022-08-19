using System.Text;
using Agoda.Core.IConfiguration;
using Agoda.Data;
using Agoda.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var _allowClientsCors = "_allowClientsCors";
// Add services to the container.
builder.Services.AddControllers()
.AddJsonOptions(opt =>
{
    opt.JsonSerializerOptions.PropertyNamingPolicy = null;
});
// SQL DB
builder.Services.AddDbContext<AgodaDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration["dbConstr"]);
});
var tokenValidator = new TokenValidationParameters
{
    ValidateIssuer = true,
    ValidateAudience = true,
    ValidateLifetime = true,
    ValidateIssuerSigningKey = true,
    ValidIssuer = builder.Configuration["JwtConfig:Issuer"],
    ValidAudience = builder.Configuration["JwtConfig:Audience"],
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtConfig:Key"])),
    ClockSkew = TimeSpan.Zero
};

var jwtEvents = new JwtBearerEvents()
{
    OnAuthenticationFailed = context =>
    {
        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
        {
            context.Response.Headers.Add("Token-Expired", "true");
        }
        return Task.CompletedTask;
    },
    OnMessageReceived = context =>
    {
        var act = context.Request.Query["access_token"];
        if (string.IsNullOrEmpty(act) == false)
        {
            context.Token = act;
        }
        return Task.CompletedTask;
    }
};

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(config =>
{
    config.TokenValidationParameters = tokenValidator;
    config.Events = jwtEvents;
});

builder.Services.AddAuthorization(config =>
{
    config.AddPolicy("SiteUser", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("IsSiteUser", bool.TrueString);
    });
});

builder.Services.DIConfig();

builder.Services.AddCors(opt =>
{
    opt.AddPolicy(_allowClientsCors, policy =>
    {
        policy.WithOrigins("http://localhost:3000", "http://localhost:4200")
        .AllowAnyHeader().AllowAnyMethod().AllowCredentials();
    });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(setup =>
{
    setup.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    setup.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(_allowClientsCors);

app.UseAuthentication();
app.UseAuthorization();

app.useRequestLog();

app.MapControllers();

app.Run();
