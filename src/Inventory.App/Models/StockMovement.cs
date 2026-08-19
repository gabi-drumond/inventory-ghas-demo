namespace Inventory.App.Models;

/// <summary>Movimento de estoque gerado a partir de um pedido.</summary>
public sealed record StockMovement(
    string MovementType,
    string Sku,
    string Warehouse,
    int Quantity,
    DateTime OccurredAtUtc);
