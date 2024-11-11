using CareerNetCompany.API.Configuration;
using CareerNetCompany.API.EventConsumers.CheckCompanyJobRight;
using CareerNetCompany.API.Middlewares;
using CareerNetCompany.Application;
using CareerNetCompany.Application.EventConsumers.HasExceptionJobCreate;
using CareerNetCompany.Persistance;
using EventShared.EventsQueue;
using FluentValidation.AspNetCore;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers(options => options.Filters.Add<CompanyValidationFilter>()).AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

//Environment bilgisine göre appsettings dosyasýný yükleyen kýsým
var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
builder.Configuration
    .AddJsonFile($"appsettings.{environment}.json", optional: true);

//Application katmanýndaki servisleri IOC'a ekler
builder.Services.AddApplicationServices();

//Persistance katmanýndaki servisleri IOC'a ekler.
builder.Services.AddPersistanceServices(builder.Configuration);
builder.Services.AddHealthChecks();

// RabbitMQ ayarlarýný appsettings'ten alýr
var rabbitMQSettings = builder.Configuration.GetSection("RabbitMQSettings").Get<RabbitMQSettings>();

// MassTransit yapýlandýrmasýný ekler
builder.Services.AddMassTransit(configurator =>
{
    configurator.AddConsumer<CheckCompanyJobRightEventConsumer>();
    configurator.AddConsumer<HasExceptionJobCreateEventConsumer>();
    configurator.UsingRabbitMq((context, _cfg) =>
    {
        _cfg.Host("rabbitmq://" + rabbitMQSettings!.Host, h =>
        {
            h.Username(rabbitMQSettings.Username!);
            h.Password(rabbitMQSettings.Password!);
        });

        _cfg.ReceiveEndpoint(RabbitMqQueue.HasExceptionJobCreateEventQueue, e => e.ConfigureConsumer<HasExceptionJobCreateEventConsumer>(context));

        _cfg.ReceiveEndpoint(RabbitMqQueue.CheckCompanyJobRightEventQueue, e => e.ConfigureConsumer<CheckCompanyJobRightEventConsumer>(context));
    });
});

//Fluent Validation
builder.Services.AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Global Exception Handling Middleware'i ekler.
app.UseMiddleware<CompanyExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapHealthChecks("/health");
app.Run();
