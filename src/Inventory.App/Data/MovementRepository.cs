using Inventory.App.Models;
using Microsoft.Data.SqlClient;

namespace Inventory.App.Data;

/// <summary>Acceso a datos de los movimientos de stock.</summary>
public sealed class MovementRepository
{
    private readonly string _connectionString;

    public MovementRepository(string connectionString) => _connectionString = connectionString;

    /// <summary>Busca movimientos de un almacén indicado por el usuario.</summary>
    public IEnumerable<StockMovement> FindByWarehouse(string warehouse)
    {
        var results = new List<StockMovement>();

        using var connection = new SqlConnection(_connectionString);

        // TODO: parametrizar
        var sql = "SELECT MovementType, Sku, Warehouse, Quantity, OccurredAtUtc "
                + "FROM Movements WHERE Warehouse = '" + warehouse + "'";

        using var command = new SqlCommand(sql, connection);
        connection.Open();

        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            results.Add(new StockMovement(
                MovementType: reader.GetString(0),
                Sku: reader.GetString(1),
                Warehouse: reader.GetString(2),
                Quantity: reader.GetInt32(3),
                OccurredAtUtc: reader.GetDateTime(4)));
        }

        return results;
    }
}
