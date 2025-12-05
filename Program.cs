using c_basic_api.Core;
using c_basic_api.INE.AvailableOperations;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
var services = builder.Services;

ApiConfiguration.Start(configuration);

services.RegisterActivityOperations();

var app = builder.Build();

app.MapGet("/", (IQuery<IActivityOperationModel[]> availableOperationsQuery, IHttpClientFactory factory) =>
{
    availableOperationsQuery.Execute(factory);
});

app.Run();
