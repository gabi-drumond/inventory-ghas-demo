using Inventory.App.Models;

namespace Inventory.App.Services;

/// <summary>Reglas de negocio puras (sin I/O) — fáciles de probar.</summary>
public static class MovementCalculator
{
    public const string Inbound = "IN";

    /// <summary>Un pedido es válido cuando tiene identificación y cantidad positiva.</summary>
    public static bool IsValid(Order order) =>
        order is not null
        && !string.IsNullOrWhiteSpace(order.OrderId)
        && !string.IsNullOrWhiteSpace(order.Warehouse)
        && !string.IsNullOrWhiteSpace(order.Sku)
        && order.Quantity > 0;

    /// <summary>Convierte un pedido en un movimiento de entrada de stock.</summary>
    public static StockMovement FromOrder(Order order)
    {
        if (!IsValid(order))
        {
            throw new ArgumentException("Pedido no válido.", nameof(order));
        }

        return new StockMovement(
            MovementType: Inbound,
            Sku: order.Sku,
            Warehouse: order.Warehouse,
            Quantity: order.Quantity,
            OccurredAtUtc: DateTime.UtcNow);
    }
}
