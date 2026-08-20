# Inventory Demo — GitHub + Actions + GHAS

Aplicación de ejemplo (**consola .NET 8**) que simula importar un pedido y registrar un
movimiento de stock en SQL Server. El objetivo es **demostrar** GitHub Actions (CI),
CodeQL, Dependency Review y funcionalidades de GitHub Advanced Security (GHAS).

> Dominio 100% genérico (gestión de stock / almacén). Sin clientes, marcas ni sistemas reales.

## Estructura

```
inventory-demo/
├─ Inventory.sln
├─ src/
│  └─ Inventory.App/            # App de consola (net8.0), usa Microsoft.Data.SqlClient
│     ├─ Models/                # Order, StockMovement
│     ├─ Services/              # MovementCalculator (reglas puras)
│     └─ Program.cs             # Flujo: importa pedido → genera movimiento
└─ tests/
   └─ Inventory.App.Tests/      # xUnit (pruebas unitarias)
```

## Cómo ejecutar

```bash
dotnet build
dotnet test
dotnet run --project src/Inventory.App
```

## Requisitos

- .NET 8 SDK

## Pipelines (GitHub Actions)

| Workflow | Archivo | Tipo | Cuándo se ejecuta |
|---|---|---|---|
| Build CI | `.github/workflows/build.yml` | Compilación + pruebas | push/PR en `main` |
| CodeQL | `.github/workflows/codeql.yml` | SAST (analiza tu código) | push/PR en `main` |
| Dependency Review | `.github/workflows/dependency-review.yml` | SCA (analiza dependencias) | Pull Request en `main` |
| Release | `.github/workflows/release.yml` | Empaquetado + promoción | tag `v*` o ejecución manual |
