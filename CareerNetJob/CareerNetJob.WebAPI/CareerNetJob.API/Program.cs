using CareerNetJob.API.Configuration;
using CareerNetJob.API.Middlewares;
using CareerNetJob.BusinessLogic;
using CareerNetJob.BusinessLogic.Abstractions;
using CareerNetJob.BusinessLogic.Concretes;
using CareerNetJob.BusinessLogic.Configuration;
using FluentValidation.AspNetCore;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

#region AddControllers-JobValidationFilter

//Validation Filter eklendiði kýsým
builder.Services.AddControllers(options => options.Filters.Add<JobValidationFilter>()).AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

#endregion

#region builder.Configuration.AddJsonFile

//Environment bilgisine göre appsettings dosyasýný yükleyen kýsým
var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
builder.Configuration
    .AddJsonFile($"appsettings.{environment}.json", optional: true);

#endregion


builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

#region Business Logic Layer DI Configuration

//Business katmanýndaki DI inject eder.
builder.Services.AddBusinessLogicServices(builder.Configuration);

#endregion

#region Rabbit MQ Mass Transit Configuration

// RabbitMQ ayarlarýný appsettings'ten alýr
var rabbitMQSettings = builder.Configuration.GetSection("RabbitMQSettings").Get<RabbitMQSettings>();

// MassTransit yapýlandýrmasýný ekler
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

#endregion

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});


builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Middleware inject eder.
app.UseMiddleware<JobExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
