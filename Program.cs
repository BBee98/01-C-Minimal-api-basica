using c_basic_api.Core;
using c_basic_api.INE.AvailableOperations;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var configuration = builder.Configuration;
var services = builder.Services;

ApiConfiguration.Start(configuration);

services.RegisterActivityOperations();

app.MapGet("/", () => "Hello World!");

app.Run();
