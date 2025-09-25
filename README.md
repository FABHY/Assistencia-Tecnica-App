# 🛠️ Sistema de Gestão de Assistência Técnica (C# .NET & MySQL)

Este projeto é um sistema de console desenvolvido em C# (.NET) para simular a gestão completa (CRUD) de ordens de serviço e clientes para uma assistência técnica. Ele demonstra a implementação de persistência de dados utilizando o **Entity Framework Core (EF Core)** conectado a um banco de dados **MySQL**.

## ✨ Funcionalidades Principais

* **Persistência com MySQL:** Utiliza EF Core e Pomelo.EntityFrameworkCore.MySql para salvar e carregar dados de forma segura.
* **Gestão de Clientes (CRUD):** Cadastro, Listagem, **Edição** (parcialmente implementada) e **Remoção** de clientes.
* **Gestão de Serviços (CRUD):** Adicionar, Listar, Marcar como Concluído, Remover e Editar serviços.
* **Relações de Dados:** Cada serviço é vinculado a um `ClienteId` (Chave Estrangeira), garantindo a integridade referencial.

## 🚀 Como Executar o Projeto

Para rodar este projeto, você precisará ter o **.NET SDK** e um servidor **MySQL** instalados.

### 1. Pré-requisitos

* **[.NET SDK 6.0 ou superior](https://dotnet.microsoft.com/download)**
* **Servidor MySQL** (ex: MySQL Community Server, XAMPP, Docker)
* **Ferramenta `dotnet ef` CLI** (Para gerenciar migrações)
    ```bash
    dotnet tool install --global dotnet-ef
    ```

### 2. Configuração do Banco de Dados

**Atenção:** Você deve configurar sua string de conexão para que o projeto se comunique com seu MySQL.

1.  Crie uma base de dados vazia no seu servidor MySQL (ex: `assistenciatecnica_db`).
2.  Abra o arquivo **`Program.cs`** e localize a classe `AppDbContext`.
3.  **Atualize a `connectionString`** com suas credenciais reais (senha, usuário e porta):

    ```csharp
    // Exemplo em Program.cs:
    string connectionString = "Server=localhost;Port=3306;Database=assistenciatecnica_db;Uid=root;Pwd=SUA_SENHA_CORRETA;";
    ```

### 3. Criar a Estrutura do Banco (Migrations)

No terminal, navegue até a pasta raiz do projeto (`AssistenciaTecnicaApp`) e execute os comandos para o EF Core criar as tabelas no seu MySQL:

```bash
# Cria um novo arquivo de migração (se houver alterações no modelo)
dotnet ef migrations add InitialSetup
# Aplica todas as migrações no banco de dados MySQL
dotnet ef database update

CONTATO
E-MAIL: fabiohypolito2021@outlook.com
TEL./WHATSAPP: (11) 93218-5499 
