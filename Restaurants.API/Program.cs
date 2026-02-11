using Restaurants.Infrastructure.Persistance.Seeds.Abstractions;
using Restaurants.IoC;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.RegisterAllServices(builder.Configuration);


var app = builder.Build();

using (var scope = app.Services.CreateAsyncScope())
{
    var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
    await dbInitializer.InitializeAsync();
}

app.MapControllers();

await app.RunAsync();
