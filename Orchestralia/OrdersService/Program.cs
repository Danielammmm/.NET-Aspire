var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddControllers();

// HttpClient para Inventory
builder.Services.AddHttpClient("inventory", client =>
{
    client.BaseAddress = new Uri("http://inventoryservice"); 
});

var app = builder.Build();

app.MapDefaultEndpoints(); 

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
