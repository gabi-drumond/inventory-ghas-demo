using Inventory.App.Data;
using Inventory.App.Models;
using Inventory.App.Services;
using Microsoft.Data.SqlClient;

// Demo genérica: importa um pedido e "grava" um movimento de estoque.
// A conexão com SQL Server é apenas montada (não abrimos aqui) para manter
// o app executável sem um banco real durante a apresentação.

var order = new Order(OrderId: "ORD-1001", Warehouse: "WH-CENTRAL", Sku: "SKU-ABC-123", Quantity: 10);

Console.WriteLine($"Importando pedido {order.OrderId} ({order.Quantity}x {order.Sku})...");

if (!MovementCalculator.IsValid(order))
{
    Console.WriteLine("Pedido inválido. Encerrando.");
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
    $"Movimento {movement.MovementType} preparado: {movement.Quantity}x {movement.Sku} " +
    $"em {movement.Warehouse} @ {movement.OccurredAtUtc:o}");
Console.WriteLine($"(Destino: {connectionString})");

// Consulta opcional por armazém informado via argumento de linha de comando.
if (args.Length > 0)
{
    var warehouseFilter = args[0];
    var repository = new MovementRepository(connectionString);
    foreach (var existing in repository.FindByWarehouse(warehouseFilter))
    {
        Console.WriteLine($"  encontrado: {existing.Quantity}x {existing.Sku}");
    }
}
