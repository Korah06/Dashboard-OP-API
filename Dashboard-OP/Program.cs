using System.Text.Json.Serialization;
using Dashboard_OP.src.api.Services.Implementations;
using Dashboard_OP.src.api.Services.Interfaces;
using Dashboard_OP.src.api.UseCases.LoggingApp;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

// LoggingApp: mock service is a singleton so its in-memory data persists between requests.
builder.Services.AddSingleton<ILoggingAppService, MockLoggingAppService>();
builder.Services.AddScoped<IGetLogsUseCase, GetLogsUseCase>();
builder.Services.AddScoped<IGetLogByIdUseCase, GetLogByIdUseCase>();
builder.Services.AddScoped<ICreateLogUseCase, CreateLogUseCase>();
builder.Services.AddScoped<IUpdateLogUseCase, UpdateLogUseCase>();
builder.Services.AddScoped<IDeleteLogUseCase, DeleteLogUseCase>();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
