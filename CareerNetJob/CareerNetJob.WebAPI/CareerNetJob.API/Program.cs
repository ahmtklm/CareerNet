using CareerNetJob.API.Configuration;
using CareerNetJob.API.Middlewares;
using CareerNetJob.BusinessLogic;
using CareerNetJob.BusinessLogic.EventConsumers.ConfirmedJobRight;
using CareerNetJob.BusinessLogic.EventConsumers.DeniedJobRight;
using EventShared.EventsQueue;
using FluentValidation.AspNetCore;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

//Validation Filter eklendiði kýsým
builder.Services.AddControllers(options => options.Filters.Add<JobValidationFilter>()).AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});


#region builder.Configuration.AddJsonFile

//Environment bilgisine göre appsettings dosyasýný yükleyen kýsým
var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
builder.Configuration
    .AddJsonFile($"appsettings.{environment}.json", optional: true);

#endregion

#region Business Logic Layer DI Configuration

//Business katmanýndaki DI inject eder.
builder.Services.AddBusinessLogicServices(builder.Configuration);

#endregion

#region Rabbit MQ Mass Transit Configuration

// RabbitMQ ayarlarýný appsettings'ten alýr
var rabbitMQSettings = builder.Configuration.GetSection("RabbitMQSettings").Get<RabbitMQSettings>();

// MassTransit yapýlandýrmasýný ekler
builder.Services.AddMassTransit(configurator =>
{
    configurator.AddConsumer<CompanyJobRightConfirmedEventConsumer>();
    configurator.AddConsumer<CompanyJobRightDeniedEventConsumer>();

    configurator.UsingRabbitMq((context, _cfg) =>
    {
        _cfg.Host("rabbitmq://" + rabbitMQSettings!.Host, h =>
        {
            h.Username(rabbitMQSettings.Username!);
            h.Password(rabbitMQSettings.Password!);
        });

        _cfg.ReceiveEndpoint(RabbitMqQueue.CompanyJobRightConfirmedEventQueue, e => e.ConfigureConsumer<CompanyJobRightConfirmedEventConsumer>(context));

        _cfg.ReceiveEndpoint(RabbitMqQueue.CompanyJobRightDeniedEventQueue, e => e.ConfigureConsumer<CompanyJobRightDeniedEventConsumer>(context));
    });
});

#endregion

builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});



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
