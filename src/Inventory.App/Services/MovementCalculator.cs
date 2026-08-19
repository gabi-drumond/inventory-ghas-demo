using Inventory.App.Models;

namespace Inventory.App.Services;

/// <summary>Regras de negócio puras (sem I/O) — fáceis de testar.</summary>
public static class MovementCalculator
{
    public const string Inbound = "IN";

    /// <summary>Um pedido é válido quando tem identificação e quantidade positiva.</summary>
    public static bool IsValid(Order order) =>
        order is not null
        && !string.IsNullOrWhiteSpace(order.OrderId)
        && !string.IsNullOrWhiteSpace(order.Warehouse)
        && !string.IsNullOrWhiteSpace(order.Sku)
        && order.Quantity > 0;

    /// <summary>Converte um pedido em um movimento de entrada de estoque.</summary>
    public static StockMovement FromOrder(Order order)
    {
        if (!IsValid(order))
        {
            throw new ArgumentException("Pedido inválido.", nameof(order));
        }

        return new StockMovement(
            MovementType: Inbound,
            Sku: order.Sku,
            Warehouse: order.Warehouse,
            Quantity: order.Quantity,
            OccurredAtUtc: DateTime.UtcNow);
    }
}
