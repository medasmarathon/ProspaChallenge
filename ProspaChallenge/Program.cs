using FluentValidation;
using ProspaChallenge.Application.Validation;
using ProspaChallenge.Business.Interfaces;
using ProspaChallenge.Business.Models;
using ProspaChallenge.Business.Rules;
using ProspaChallenge.Infrastructure.Repositories;
using ProspaChallenge.Services;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
