using NServiceBus;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));
builder.Logging.AddConsole();

var endpointConfiguration = new EndpointConfiguration("Platform.Api");
endpointConfiguration.UseSerialization<SystemJsonSerializer>();
endpointConfiguration.Conventions()
    .DefiningEventsAs(type => type.Namespace != null && type.Namespace.EndsWith("Events"))
    .DefiningCommandsAs(type => type.Namespace != null && type.Namespace.EndsWith("Commands"));
    

// Debug: Print all environment variables related to connection strings
foreach (var envVar in Environment.GetEnvironmentVariables().Keys)
{
    if (envVar is string key && key.Contains("AzureServiceBus", StringComparison.OrdinalIgnoreCase))
    {
        Console.WriteLine($"[DEBUG] Env: {key} = '{Environment.GetEnvironmentVariable(key)}'");
    }
}


var connectionString = builder.Configuration.GetConnectionString("AzureServiceBus")
    ?? throw new InvalidOperationException("AzureServiceBus connection string is missing in configuration.");
var routing = endpointConfiguration.UseTransport(new AzureServiceBusTransport(connectionString, TopicTopology.Default));


builder.UseNServiceBus(endpointConfiguration);
// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddControllers(); // Add support for controllers
builder.Services.AddOpenApi();

// Add Swagger/OpenAPI services
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();
    app.MapOpenApi(); // Exposes the OpenAPI document at /openapi/v1.json
if (app.Environment.IsDevelopment())
{

    //http://localhost:5271/swagger/index.html
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "v1");
    });
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
