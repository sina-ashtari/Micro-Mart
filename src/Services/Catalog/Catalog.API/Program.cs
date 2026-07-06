using Catalog.API.Data;

var builder = WebApplication.CreateBuilder(args);

var assembly = typeof(Program).Assembly;

// Add services to the container here
builder.Services.AddMarten(config =>
{
    config.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();

if (builder.Environment.IsDevelopment()) builder.Services.InitializeMartenWith<CatalogInitialData>();

builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));

});
builder.Services.AddValidatorsFromAssembly(assembly);
builder.Services.AddExceptionHandler<CustomExceptionHandler>();
var app = builder.Build();

// Configure the HTTP Request pipeline
app.MapCarter();
app.UseExceptionHandler(config => { });

app.Run();
