using desafio.Data;
using FluentValidation.AspNetCore;
using Hangfire;
using System;
using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.Extensions.DependencyInjection;
using apiDesafio.Services;
using System.Text;
using apiDesafio;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Program>());
builder.Services.AddDbContext<AppDbContext>();

builder.Services.AddTransient<TokenServices>();

//builder

var key = Encoding.ASCII.GetBytes(Configuration.JwtKey);
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(x=>
{
    x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        //isso e pra multipls api
        ValidateIssuer = false,
        ValidateAudience = false,
    }; 
});

builder.Services.AddHangfire(config => config.UseMemoryStorage());

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

//autentication
//qm eh
app.UseAuthentication();
//oq pode fazer
app.UseAuthorization();

//hangfire

app.UseHangfireDashboard();
app.UseHangfireServer();
//RecurringJob.AddOrUpdate(() => ProductUpdateChecker.ProductLogUpdateJobAsync(), Cron.Minutely(2);

RecurringJob.AddOrUpdate(() => ProductUpdateChecker.ProductLogUpdateJobAsync(), "*/30 * * * *");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

//--------------------------------

void ConfigureSwaggerDocumentation(SwaggerGenOptions genOptions)
{
    genOptions.SwaggerDoc(
        "v1",
        new OpenApiInfo
        {
            Title = "Produção API",
            Description = "API utilizada para fornecer os dados utilizados no apontamento de matérias primas no aplicativo de produção.",
            Version = Assembly.GetEntryAssembly().GetName().Version.ToString(),
        });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    genOptions.IncludeXmlComments(xmlPath);
}

void ConfigureSwaggerSecurity(SwaggerGenOptions genOptions)
{
    genOptions.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description =
                "Os endpoints dessa API utilizam JWT Authorization como forma de autenticação, através de Tokens Bearer." +
                "\r\n\r\n Digite 'Bearer' [espaço] e cole o token logo após.",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    genOptions.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,

            },
            new List<string>()
        }
    });
}
