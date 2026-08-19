# Inventory Demo — GitHub + Actions + GHAS

Aplicação de exemplo (**console .NET 8**) que simula importar um pedido e gravar um
movimento de estoque em SQL Server. O objetivo é **demonstrar** GitHub Actions (CI),
CodeQL, Dependency Review e recursos do GitHub Advanced Security (GHAS).

> Domínio 100% genérico (gestão de estoque / almoxarifado). Sem clientes, marcas ou sistemas reais.

## Estrutura

```
inventory-demo/
├─ Inventory.sln
├─ src/
│  └─ Inventory.App/            # Console app (net8.0), usa Microsoft.Data.SqlClient
│     ├─ Models/                # Order, StockMovement
│     ├─ Services/              # MovementCalculator (regras puras)
│     └─ Program.cs             # Fluxo: importa pedido → gera movimento
└─ tests/
   └─ Inventory.App.Tests/      # xUnit (testes de unidade)
```

## Como rodar

```bash
dotnet build
dotnet test
dotnet run --project src/Inventory.App
```

## Requisitos

- .NET 8 SDK
