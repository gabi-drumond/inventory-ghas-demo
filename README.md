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

## Pipelines (GitHub Actions)

| Workflow | Arquivo | Tipo | Quando roda |
|---|---|---|---|
| Build CI | `.github/workflows/build.yml` | Build + testes | push/PR na `main` |
| CodeQL | `.github/workflows/codeql.yml` | SAST (analisa o seu código) | push/PR na `main` |
| Dependency Review | `.github/workflows/dependency-review.yml` | SCA (analisa dependências) | Pull Request na `main` |

## Ativação do GHAS (passo manual na UI do GitHub)

> Em repositório **privado**, o GHAS precisa ser habilitado nas *Settings* — senão os alertas
> de segurança **não aparecem** no Pull Request.

Em **Settings → Advanced Security** (ou *Code security and analysis*), habilite:

1. **Dependency graph** — mapeia as dependências do projeto.
2. **Dependabot alerts** — avisa sobre dependências vulneráveis (ex.: `Newtonsoft.Json 12.0.3`).
3. **Code scanning (CodeQL)** — mostra os alertas do CodeQL (ex.: SQL Injection).
4. **Secret scanning** + **Push protection** — detecta e **bloqueia** segredos antes do commit.

## Roteiro da demo (~30 min)

1. **(3 min) Contexto** — mostrar o app e a estrutura (código genérico de estoque).
2. **(5 min) CI** — abrir a aba **Actions**, mostrar o *Build CI* verde (build + testes).
3. **(5 min) SAST** — abrir o **PR #1**, mostrar o alerta do **CodeQL** apontando a
   **SQL Injection** em `Data/MovementRepository.cs` (concatenação de string).
4. **(5 min) SCA** — no mesmo PR, mostrar o **Dependency Review** barrando
   `Newtonsoft.Json 12.0.3` (severidade *high*).
5. **(5 min) O fix** — explicar a correção: query **parametrizada** (`@warehouse`) e
   **atualizar** o pacote para uma versão sem falha.
6. **(5 min) Push protection** — tentar commitar um **token de EXEMPLO** numa branch de
   teste e mostrar o GitHub **bloqueando o push** (ver aviso abaixo).
7. **(2 min) Fechamento** — "segurança dentro do fluxo, não auditoria no fim".

## ⚠️ Segredos — NUNCA commite credenciais reais

Para demonstrar o **push protection**, use apenas um valor **fictício/de exemplo** em uma
**branch de teste descartável** — jamais uma credencial real. O objetivo é ver o GitHub
**bloquear o push**, não expor um segredo.

- ❌ Não coloque tokens, senhas ou connection strings reais em nenhum arquivo.
- ✅ Use um placeholder claramente falso e apague a branch depois da demo.
