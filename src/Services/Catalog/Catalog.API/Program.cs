var builder = WebApplication.CreateBuilder(args);

// Add services to the container here
builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

var app = builder.Build();

app.MapCarter();
// Configure the HTTP Request pipeline

app.Run();
