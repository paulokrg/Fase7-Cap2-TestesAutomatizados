# SmartCities Test Automation

Este repositório contém dois projetos:

- **API** em `SmartCities.Api`: aplicação minimal ASP.NET Core que expõe endpoints de saúde, gestão de cidades e aciedentes.
- **Testes** em `tests/SmartCities.Tests`: testes BDD/BDD-API em C# com SpecFlow, NUnit, RestSharp e validação de contrato via JSON Schema.

---

## 1. Pré-requisitos

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download)
- Visual Studio 2022/VS Code com extensões C# e SpecFlow
- Git (opcional, para controle de versão)

---

## 2. Estrutura do repositório

```
SmartCities.sln
├─ SmartCities.Api
│   ├─ Program.cs
│   └─ Controllers\
│       ├ CitiesController.cs
│       └ HealthController.cs
│       └ AcidenteController.cs
└─ SmartCities-Tests
    └─ tests
        └─ SmartCities.Tests
            ├─ SmartCities.Tests.csproj
            ├─ specflow.json
            ├─ Features\
            │   ├ CitiesApi.feature
            │   └ HealthCheck.feature
            │   └ Acidente.feature
            ├─ Steps\
            │   ├ CommonSteps.cs
            │   ├ CitiesApiSteps.cs
            │   ├ HealthCheckSteps.cs
            │   ├ AcidenteSteps.cs
            │   └ SpecsValidationSteps.cs
            └─ Specs\
                ├ cities-schema.json
                └ health-schema.json
                └ acidentes-schema.json

```

---

## 3. Como executar localmente

### 3.1. Subir a API

```bash
cd SmartCities.Api
dotnet restore
dotnet run --launch-profile https
```

A API ficará disponível em `https://localhost:5000` (e `http://localhost:5215`).
A documentação da API ficará disponível em `https://localhost:5000/swagger/index.html`

| Verbo | Rota              | Descrição                          |
| ----- | ----------------- | ---------------------------------- |
| GET   | `/health`         | Health-check: `{ "status": "UP" }` |
| GET   | `/cities`         | Lista todas as cidades             |
| GET   | `/cities/{id}`    | Busca cidade por ID                |
| GET   | `/acidentes`      | Lista todos os acidentes           |
| GET   | `/acidentes/{id}` | Busca acidente por ID              |


### 3.2. Rodar os testes

Em outro terminal:

```bash
cd tests/SmartCities.Tests
dotnet restore
dotnet test --logger "console;verbosity=detailed"
```

### 3.3. Execução dos testes

CitiesApi.feature
  Listar cidades:
  Buscar cidade inexistente (404)

HealthCheck.feature:
Verificar health (status 200 + JSON válido)

Acidente.feature:
  Listar acidentes
  Buscar acidente inexistente (404)
---

## 4. Geração de relatório LivingDoc

Para produzir um relatório interactivo:

```bash
dotnet specflow livingdoc   --project tests/SmartCities.Tests   --output-path Report.html
```

Você pode então abrir `Report.html` no navegador para ver o resumo dos cenários.

---

## 5. Integração Contínua (GitHub Actions)

Adicione o arquivo em `.github/workflows/ci.yml` com o seguinte conteúdo:

```yaml
name: CI

on:
  push:
    branches: [ main ]
  pull_request:
    branches: [ main ]

jobs:
  build-and-test:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v3

      - name: Setup .NET
        uses: actions/setup-dotnet@v3
        with:
          dotnet-version: '8.x'

      - name: Restore API
        run: dotnet restore SmartCities.Api

      - name: Run API (background)
        run: |
          cd SmartCities.Api
          dotnet run &

      - name: Restore tests
        run: dotnet restore tests/SmartCities.Tests

      - name: Execute tests
        run: dotnet test tests/SmartCities.Tests --no-build --logger trx

      - name: Publish LivingDoc
        run: |
          dotnet tool install --global SpecFlow.Plus.LivingDoc.CLI
          livingdoc test-assembly             --project tests/SmartCities.Tests             --output Report.html

      - name: Upload Report
        uses: actions/upload-artifact@v3
        with:
          name: TestsReport
          path: Report.html
```

---

## 6. Artefatos de entrega

1. **Código-fonte**: ZIP contendo `SmartCities.Api` e `tests/SmartCities.Tests`.
2. **README.md**: este arquivo explicando pré-requisitos e como rodar.
3. **Relatório LivingDoc**: PDF ou HTML gerado com cenários e evidências.
