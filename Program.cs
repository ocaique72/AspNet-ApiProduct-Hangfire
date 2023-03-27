using desafio.Data;
using FluentValidation.AspNetCore;
using Hangfire;
using System;
using Hangfire;
using Hangfire.MemoryStorage;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Program>());
builder.Services.AddDbContext<AppDbContext>();

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

//hangfire

app.UseHangfireDashboard();
app.UseHangfireServer();
//RecurringJob.AddOrUpdate(() => ProductUpdateChecker.ProductLogUpdateJobAsync(), Cron.Minutely(2);

RecurringJob.AddOrUpdate(() => ProductUpdateChecker.ProductLogUpdateJobAsync(), "*/1 * * * *");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
