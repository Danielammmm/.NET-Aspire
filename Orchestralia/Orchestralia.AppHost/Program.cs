var builder = DistributedApplication.CreateBuilder(args);

// Servicio API central
var apiService = builder.AddProject<Projects.Orchestralia_ApiService>("apiservice")
    .WithHttpEndpoint(name: "api-http", targetPort: 5050);

// Servicio de inventario
var inventoryService = builder.AddProject<Projects.InventoryService>("inventoryservice")
    .WithHttpEndpoint(name: "inventory-http", targetPort: 5199);

// Servicio de autenticación
var authService = builder.AddProject<Projects.AuthService>("authservice")
    .WithHttpEndpoint(name: "auth-http", targetPort: 5177)
    .WithReference(apiService)
    .WaitFor(apiService);

// Servicio de órdenes 
var ordersService = builder.AddProject<Projects.OrdersService>("ordersservice")
    .WithHttpEndpoint(name: "orders-http", targetPort: 5190)
    .WithReference(apiService)
    .WithReference(inventoryService) 
    .WaitFor(apiService)
    .WaitFor(inventoryService);      

builder.Build().Run();
