# 📋 Idea Manager

Aplicação web fullstack para captura, organização e evolução de ideias até sua transformação em projetos executáveis.

[![Status](https://img.shields.io/badge/status-estável-brightgreen)]()
[![.NET](https://img.shields.io/badge/.NET-10.0-purple)]()
[![PostgreSQL](https://img.shields.io/badge/PostgreSQL-16-336791)]()
[![License](https://img.shields.io/badge/license-MIT-blue)]()

---

💡 **Problema:** ideias surgem constantemente, mas sem organização acabam sendo esquecidas ou nunca executadas.

🚀 **Solução:** o Idea Manager centraliza, organiza e acompanha ideias desde a criação até sua execução como projetos.

🎯 **Objetivo:** transformar ideias em ações de forma estruturada e rastreável.
---

## 🚀 Visão Geral

O **Idea Manager** é uma aplicação web para captura, organização e evolução de ideias até sua transformação em projetos executáveis.

Projetado com foco em produtividade e clareza, o sistema permite:

- Registrar ideias de forma rápida  
- Organizar por categorias e status  
- Acompanhar o ciclo de vida das ideias  
- Converter ideias em projetos estruturados  
- Monitorar progresso de execução  
- Reverter conversões quando necessário  

👉 O objetivo é reduzir a perda de ideias e facilitar sua execução de forma estruturada.

## 🧱 Estrutura do Projeto

```text
RepositorioIdeias/
├── IdeaManager.Core/                # Domínio e Regras de Negócio
│   ├── Entities/
│   │   ├── Idea.cs                 # Entidade Ideia
│   │   └── Project.cs              # Entidade Projeto
│   ├── Enums/
│   │   ├── IdeaStatus.cs           # Draft, Active, Converted, Discarded
│   │   └── ProjectStatus.cs        # Planning, InProgress, Paused, Completed, Cancelled
│   └── IdeaManager.Core.csproj
│
├── IdeaManager.Infrastructure/     # Acesso a Dados
│   ├── Data/
│   │   └── AppDbContext.cs         # DbContext do EF Core
│   ├── Migrations/                # Migrations do banco
│   └── IdeaManager.Infrastructure.csproj
│
├── IdeaManager.Web/               # Apresentação (Razor Pages)
│   ├── Pages/
│   │   ├── Ideas/                # CRUD Completo
│   │   │   ├── Index.cshtml      # Listagem em cards
│   │   │   ├── Create.cshtml     # Nova ideia
│   │   │   ├── Edit.cshtml       # Editar ideia
│   │   │   ├── Delete.cshtml     # Excluir ideia
│   │   │   ├── Details.cshtml    # Detalhes (toggle texto/tabela)
│   │   │   └── ConvertToProject.cshtml # Conversão para projeto
│   │   ├── Shared/
│   │   │   └── _Layout.cshtml    # Layout base com Bootstrap
│   │   ├── Index.cshtml          # Home page
│   │   └── Privacy.cshtml        # Política de privacidade
│   ├── wwwroot/css/
│   │   └── site.css              # Estilos customizados
│   ├── Program.cs                # Entry point
│   ├── appsettings.Example.json  # Template de configuração
│   └── IdeaManager.Web.csproj
│
├── IdeaManager.Tests/            # Testes Unitários
│   ├── UnitTest1.cs
│   └── IdeaManager.Tests.csproj
│
├── .gitignore                   # Arquivos ignorados
└── IdeaManager.slnx             # Arquivo de solução
---
```

## 📊 Modelo de Dados

### 💡 Idea (Ideia)

```
| Campo       | Tipo       | Descrição                                   |
|------------|------------|--------------------------------------------|
| Id         | Guid       | Identificador único                        |
| Title      | string     | Título da ideia                            |
| Description| string     | Descrição detalhada                        |
| Category   | string     | Tecnologia, Negócios, Pessoal, etc.        |
| Status     | IdeaStatus | Draft, Active, Converted, Discarded        |
| CreatedAt  | DateTime   | Data de criação (UTC)                      |
| UpdatedAt  | DateTime?  | Última atualização                         |
```

### 🚀 Project (Projeto)

```
| Campo               | Tipo          | Descrição                                      |
|--------------------|---------------|-----------------------------------------------|
| Id                 | Guid          | Identificador único                           |
| IdeaId             | Guid          | FK para a ideia original (1:1)               |
| Name               | string        | Nome do projeto                              |
| Description        | string        | Descrição do projeto                         |
| StartDate          | DateTime      | Data de início                               |
| EndDate            | DateTime?     | Data de término                              |
| Status             | ProjectStatus | Planning, InProgress, Paused, Completed, Cancelled |
| ProgressPercentage | int           | Progresso (0-100%)                           |
```

### 🔗 Relacionamento

```text
Idea (1) ────────── (1) Project
```

---

## ✨ Funcionalidades

### 📝 Gestão de Ideias
- ✅ Criar, editar, visualizar e excluir ideias
- ✅ Organização por categoria
- ✅ Status: Rascunho, Em avaliação, Convertida, Descartada
- ✅ Visualização detalhada com toggle entre Texto e Tabela

### 🔄 Conversão de Ideias
- ✅ Transformar ideia em projeto com 1 clique
- ✅ Reversão de projeto para ideia
- ✅ Validação para evitar duplicidade

### 📊 Gestão de Projetos
- ✅ Listagem de projetos convertidos
- ✅ Barra de progresso visual (0-100%)
- ✅ Status de execução
- ✅ Vinculação com ideia original

### 🎨 UI/UX
- ✅ Cards com efeito hover
- ✅ Badges coloridos por status
- ✅ Barra de progresso dinâmica
- ✅ Layout responsivo
- ✅ Feedback visual com alerts
- ✅ Fuso horário local (UTC-3)

---

## 🛠️ Tecnologias

```
| Categoria        | Tecnologia             | Versão |
|------------------|------------------------|--------|
| Framework        | .NET (ASP.NET Core)    | 10.0   |
| Arquitetura      | Razor Pages            | -      |
| ORM              | Entity Framework Core  | 9.0    |
| Banco de Dados   | PostgreSQL             | 16+    |
| Frontend         | Bootstrap 5            | Nativo |
| Ícones           | Bootstrap Icons        | 1.11.0 |
| Testes           | xUnit                  | -      |
```
---

## ▶️ Como Executar

### Pré-requisitos
- .NET 10 SDK
- PostgreSQL 16+ (ou Docker)

### Passos

```bash
# 1. Clonar repositório
git clone https://github.com/Devmoises79/RepositorioIdeias.git
cd RepositorioIdeias
```

# 2. Configurar banco de dados

```bash
cd IdeaManager.Web
copy appsettings.Example.json appsettings.json
# Editar appsettings.json com sua senha do PostgreSQL
```
# 3. Restaurar pacotes

```bash
cd ..
dotnet restore
```

# 4. Criar banco e aplicar migrations
```bash
dotnet ef database update --context AppDbContext --project IdeaManager.Infrastructure --startup-project IdeaManager.Web
```

# 5. Executar

```bash
dotnet run --project IdeaManager.Web
```

# 6. Acessar

```csharp
http://localhost:5114
```

## 📄 Roadmap (Próximos Passos)

O projeto continuará evoluindo com as seguintes funcionalidades:

- 🔐 Autenticação e autorização com ASP.NET Identity  
- 📊 Dashboard com métricas e visualização de dados  
- 🏷️ Sistema de tags e filtros avançados  
- 📄 Exportação de dados (PDF e Excel)  
- 🌐 API REST para integração externa  
- 🧾 Auditoria de logs (monitoramento de erros)  
- 🔔 Sistema de notificações  
- 🔊 Notificações sonoras  

### 🎯 Bônus

- 📧 Notificações por e-mail


## 👨‍💻 Autor

**Moisés Aniceto**  

---

## 📄 Licença

Este projeto está sob a licença **MIT**.


