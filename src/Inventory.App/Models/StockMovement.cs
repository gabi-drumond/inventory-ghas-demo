namespace Inventory.App.Models;

/// <summary>Movimiento de stock generado a partir de un pedido.</summary>
public sealed record StockMovement(
    string MovementType,
    string Sku,
    string Warehouse,
    int Quantity,
    DateTime OccurredAtUtc);
