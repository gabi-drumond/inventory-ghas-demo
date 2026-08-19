using Inventory.App.Models;
using Inventory.App.Services;
using Xunit;

namespace Inventory.App.Tests;

public class MovementCalculatorTests
{
    [Fact]
    public void FromOrder_MapsFieldsAndCreatesInboundMovement()
    {
        var order = new Order("ORD-1", "WH-1", "SKU-1", 5);

        var movement = MovementCalculator.FromOrder(order);

        Assert.Equal(MovementCalculator.Inbound, movement.MovementType);
        Assert.Equal("SKU-1", movement.Sku);
        Assert.Equal("WH-1", movement.Warehouse);
        Assert.Equal(5, movement.Quantity);
    }

    [Fact]
    public void IsValid_ReturnsTrue_ForWellFormedOrder()
    {
        var order = new Order("ORD-2", "WH-2", "SKU-2", 1);

        Assert.True(MovementCalculator.IsValid(order));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-3)]
    public void IsValid_ReturnsFalse_ForNonPositiveQuantity(int quantity)
    {
        var order = new Order("ORD-3", "WH-3", "SKU-3", quantity);

        Assert.False(MovementCalculator.IsValid(order));
    }
}
