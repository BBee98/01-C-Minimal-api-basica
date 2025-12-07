using c_basic_api.Core;
using c_basic_api.INE.AvailableOperations;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

services.RegisterAvailableOperations();

var app = builder.Build();

app.MapGet("/", (IQuery<IAvailableOperationsModel[]> availableOperationsQuery, IHttpClientFactory factory) => availableOperationsQuery.Execute(factory));

app.Run();

