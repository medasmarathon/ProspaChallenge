using FluentValidation;
using Microsoft.AspNetCore.Builder;
using ProspaChallenge.Application.Validation;
using ProspaChallenge.Business.Interfaces;
using ProspaChallenge.Business.Models;
using ProspaChallenge.Business.Rules;
using ProspaChallenge.Infrastructure.Repositories;
using ProspaChallenge.Services;
using System.IO;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddOutputCache(opt =>
{
    opt.AddPolicy("Assessment", builder =>
    {
        builder.Expire(TimeSpan.FromSeconds(30)).VaryByValue(ctx =>
        {
            return new KeyValuePair<string, string>("RequestBody", (string)(ctx.Request.RouteValues["RequestBody"] ?? ""));
        });
    });
});
builder.Services.AddControllers().AddJsonOptions(option => {
    option.AllowInputFormatterExceptionMessages = true;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IPhoneRule, AustralianPhoneRule>();
builder.Services.AddScoped<ITimeTradingRule, TimeTradingRule>();
builder.Services.AddScoped<IBusinessNumberRule, BusinessNumberRule>();
builder.Services.AddScoped<IIndustryRule, IndustryRule>();
builder.Services.AddScoped<IValidator<Lead>, LeadValidator>();

builder.Services.AddScoped<IIndustryRepository, IndustryRepository>();
builder.Services.AddScoped<IAssessmentService, AssessmentService>();

var app = builder.Build();

app.Use((context, next) =>
{
    context.Request.EnableBuffering();
    return next(context);
});
app.Use(async (context, next) =>
{
    StreamReader reader = new StreamReader(context.Request.Body);
    context.Request.RouteValues["RequestBody"] = (await reader.ReadToEndAsync()).GetHashCode().ToString();
    context.Request.Body.Position = 0;
    await next.Invoke(context);
});
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseOutputCache();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
