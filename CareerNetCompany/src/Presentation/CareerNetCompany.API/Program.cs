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

//Validation Filter eklendiði kýsým
builder.Services.AddControllers(options => options.Filters.Add<CompanyValidationFilter>()).AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});



// RabbitMQ ayarlarýný appsettings'ten alýr
var rabbitMqSettings = builder.Configuration.GetSection("RabbitMqSettings").Get<RabbitMqSettings>();

// MassTransit yapýlandýrmasýný ekler
builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(rabbitMqSettings!.Host, h =>
        {
            h.Username(rabbitMqSettings.Username!);
            h.Password(rabbitMqSettings.Password!);
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

//Environment bilgisine göre appsettings dosyasýný yükleyen kýsým
var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
builder.Configuration
    .AddJsonFile($"appsettings.{environment}.json", optional: true);

//Application katmanýndaki servisleri IOC'a ekler
builder.Services.AddApplicationServices();

//Persistance katmanýndaki servisleri IOC'a ekler.
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
