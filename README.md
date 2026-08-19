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

## Activación de GHAS (paso manual en la UI de GitHub)

> En repositorio **privado**, GHAS debe habilitarse en *Settings* — de lo contrario las alertas
> de seguridad **no aparecen** en el Pull Request.

En **Settings → Advanced Security** (o *Code security and analysis*), habilita:

1. **Dependency graph** — mapea las dependencias del proyecto.
2. **Dependabot alerts** — avisa sobre dependencias vulnerables (ej.: `Newtonsoft.Json 12.0.3`).
3. **Code scanning (CodeQL)** — muestra las alertas de CodeQL (ej.: SQL Injection).
4. **Secret scanning** + **Push protection** — detecta y **bloquea** secretos antes del commit.

## Guion de la demo (~30 min)

1. **(3 min) Contexto** — mostrar la app y la estructura (código genérico de stock).
2. **(5 min) CI** — abrir la pestaña **Actions**, mostrar el *Build CI* en verde (compilación + pruebas).
3. **(5 min) SAST** — abrir el **PR #1**, mostrar la alerta de **CodeQL** señalando la
   **SQL Injection** en `Data/MovementRepository.cs` (concatenación de string).
4. **(5 min) SCA** — en el mismo PR, mostrar el **Dependency Review** bloqueando
   `Newtonsoft.Json 12.0.3` (severidad *high*).
5. **(5 min) El fix** — explicar la corrección: consulta **parametrizada** (`@warehouse`) y
   **actualizar** el paquete a una versión sin la falla.
6. **(5 min) Push protection** — intentar commitear un **token de EJEMPLO** en una rama de
   prueba y mostrar a GitHub **bloqueando el push** (ver aviso abajo).
7. **(2 min) Cierre** — "seguridad dentro del flujo, no auditoría al final".

## ⚠️ Secretos — NUNCA commitees credenciales reales

Para demostrar el **push protection**, usa solo un valor **ficticio/de ejemplo** en una
**rama de prueba descartable** — jamás una credencial real. El objetivo es ver a GitHub
**bloquear el push**, no exponer un secreto.

- ❌ No pongas tokens, contraseñas ni connection strings reales en ningún archivo.
- ✅ Usa un placeholder claramente falso y borra la rama después de la demo.
