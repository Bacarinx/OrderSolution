# 🍽️ OrderSolution

**OrderSolution** é um sistema de gerenciamento de comandas para estabelecimentos como bares e restaurantes. Ele permite controlar serviços abertos, comandas de clientes, produtos consumidos e categorias de produtos, tudo em tempo real.

## 🧱 Tecnologias Utilizadas

### Frontend
- React.js + Vite
- Axios
- Tailwind CSS

### Backend
- .NET 6 (ASP.NET Core Web API)
- Entity Framework Core
- JWT Authentication
- SQL Server

### Integração
- Docker

---

## 🚀 Funcionalidades

### 👨‍🍳 Estabelecimento / Serviço
- Abrir e encerrar um serviço (apenas um serviço pode estar ativo por vez)
- Visualização geral do serviço em tempo real

### 📋 Comandas (Tabs)
- Criar nova comanda
- Buscar comandas por código
- Visualizar detalhes da comanda:
  - Cliente vinculado
  - Itens consumidos
  - Valor total
  - Status (aberta/fechada)
- Filtragem por status (Todas, Abertas, Fechadas)
- Paginação

### 👤 Clientes
- Listagem de clientes
- Criação de novos clientes
- Vincular cliente a uma comanda

### 🍔 Produtos
- Listar produtos por categoria
- Criar novo produto
- Excluir produto
- Formatação automática de preço (R$ e duas casas decimais)

### 🗂️ Categorias
- Listagem de categorias
- Criação de novas categorias
- Acesso aos produtos da categoria

---

## ⚙️ Como Executar o Projeto

### 1. Clone o repositório
git clone https://github.com/Bacarinx/OrderSolution.git
cd OrderSolution

### 2. Build do Docker
docker compose up --build

### 3. Acesso
Acesse a aplicação Web no endereço: http://localhost:5173/
Se esse endereço não funcionar, acesse este: http://172.18.0.4:5173/

## 🔐 Autenticação

### O projeto utiliza JWT para autenticação. Para acessar rotas protegidas:

- Registre-se usando /register
- Faça login com /login para obter um token
- O token será armazenado em cookies e usado em chamadas axios

## 🤝 Contribuições
Contribuições são bem-vindas! Sinta-se livre para abrir issues, pull requests ou sugestões.

