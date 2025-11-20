using c_basic_api.Core.Configuration;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var configuration = builder.Configuration;

ApiConfiguration.Start(configuration);

builder.Services.AddControllers();

app.MapGet("/", () => "Hello World!");

app.Run();
