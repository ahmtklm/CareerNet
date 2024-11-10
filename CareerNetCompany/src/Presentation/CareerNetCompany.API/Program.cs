using CareerNetCompany.API.Configuration;
using CareerNetCompany.API.Middlewares;
using CareerNetCompany.Application;
using CareerNetCompany.Persistance;
using FluentValidation.AspNetCore;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Validation Filter eklendi�i k�s�m
builder.Services.AddControllers(options => options.Filters.Add<CompanyValidationFilter>()).AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});



// RabbitMQ ayarlar�n� appsettings'ten al�r
var rabbitMQSettings = builder.Configuration.GetSection("RabbitMQSettings").Get<RabbitMQSettings>();

// MassTransit yap�land�rmas�n� ekler
builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(rabbitMQSettings!.Host, h =>
        {
            h.Username(rabbitMQSettings.Username!);
            h.Password(rabbitMQSettings.Password!);
        });
        //cfg.ReceiveEndpoint(rabb.Stock_OrderCreatedEventQueue, e => e.ConfigureConsumer<OrderCreatedEventConsumer>(context));
    });
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

//Environment bilgisine g�re appsettings dosyas�n� y�kleyen k�s�m
var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
builder.Configuration
    .AddJsonFile($"appsettings.{environment}.json", optional: true);

//Application katman�ndaki servisleri IOC'a ekler
builder.Services.AddApplicationServices();

//Persistance katman�ndaki servisleri IOC'a ekler.
builder.Services.AddPersistanceServices(builder.Configuration);

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

app.Run();
