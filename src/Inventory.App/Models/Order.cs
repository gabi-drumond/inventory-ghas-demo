namespace Inventory.App.Models;

/// <summary>Pedido de entrada de mercancía en el almacén (genérico).</summary>
public sealed record Order(string OrderId, string Warehouse, string Sku, int Quantity);
