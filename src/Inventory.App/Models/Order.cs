namespace Inventory.App.Models;

/// <summary>Pedido de entrada de mercadoria no almoxarifado (genérico).</summary>
public sealed record Order(string OrderId, string Warehouse, string Sku, int Quantity);
