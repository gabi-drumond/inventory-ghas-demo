using Inventory.App.Data;
using Inventory.App.Models;
using Inventory.App.Services;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;

// Demo genérica: importa un pedido y "registra" un movimiento de stock.
// La conexión con SQL Server solo se arma (no la abrimos aquí) para mantener
// la app ejecutable sin una base real durante la presentación.

// El pedido llega como JSON (p. ej.: cola/archivo) y se deserializa.
const string orderJson =
    """{"OrderId":"ORD-1001","Warehouse":"WH-CENTRAL","Sku":"SKU-ABC-123","Quantity":10}""";

var order = JsonConvert.DeserializeObject<Order>(orderJson)
    ?? new Order(OrderId: "ORD-1001", Warehouse: "WH-CENTRAL", Sku: "SKU-ABC-123", Quantity: 10);

Console.WriteLine($"Importando el pedido {order.OrderId} ({order.Quantity}x {order.Sku})...");

if (!MovementCalculator.IsValid(order))
{
    Console.WriteLine("Pedido inválido. Finalizando.");
    return;
}

var movement = MovementCalculator.FromOrder(order);

var connectionString = new SqlConnectionStringBuilder
{
    DataSource = "localhost",
    InitialCatalog = "InventoryDemo",
    IntegratedSecurity = true,
    TrustServerCertificate = true
}.ConnectionString;

Console.WriteLine(
    $"Movimiento {movement.MovementType} preparado: {movement.Quantity}x {movement.Sku} " +
    $"en {movement.Warehouse} @ {movement.OccurredAtUtc:o}");
Console.WriteLine($"(Destino: {connectionString})");

// Consulta opcional por almacén indicado vía argumento de línea de comandos.
if (args.Length > 0)
{
    var warehouseFilter = args[0];
    var repository = new MovementRepository(connectionString);
    foreach (var existing in repository.FindByWarehouse(warehouseFilter))
    {
        Console.WriteLine($"  encontrado: {existing.Quantity}x {existing.Sku}");
    }
}
