# AgendaApp

AgendaApp é uma aplicação de gerenciamento de contatos desenvolvida com .NET e Vue.js. O objetivo deste projeto é fornecer uma interface simples e intuitiva para adicionar, editar e remover contatos, além de autenticação de usuários.

## Tecnologias Utilizadas

- **Backend**: 
  - .NET 9
  - Entity Framework Core
  - RabbitMQ para mensageria
  - FluentValidation para validação de dados
  - Swagger para documentação da API

- **Frontend**: 
  - Vue.js
  - PrimeVue para componentes UI
  - Vuelidate para validação de formulários

## Funcionalidades

- Cadastro de contatos com nome, email e telefone.
- Listagem de contatos.
- Edição e remoção de contatos.
- Autenticação de usuários.
- Validação de dados de entrada.
- Documentação da API com Swagger.

## Instalação

### Backend

1. Clone o repositório:
   ```bash
   git clone https://github.com/mgmenezes/AgendaApp.git
   cd AgendaApp/backend
   ```

2. Restaure as dependências:
   ```bash
   dotnet restore
   ```

3. Configure a string de conexão no arquivo `appsettings.json`:
   ```json
   "ConnectionStrings": {
       "DefaultConnection": "Server=.\\SQLEXPRESS;Database=AgendaDB;Trusted_Connection=True;TrustServerCertificate=True;Integrated Security=True;MultipleActiveResultSets=true"
   }
   ```

4. Execute as migrações para criar o banco de dados:
   ```bash
   dotnet ef database update
   ```

5. Inicie a aplicação:
   ```bash
   dotnet run
   ```

### Frontend

1. Navegue até a pasta do frontend:
   ```bash
   cd AgendaApp/frontend
   ```

2. Instale as dependências:
   ```bash
   npm install
   ```

3. Inicie a aplicação:
   ```bash
   npm run serve
   ```

## Uso

- Acesse a aplicação no navegador em `http://localhost:5173`.
