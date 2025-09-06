## Introdução
Projeto para gerenciamento de produtos e categorias, desenvolvido como teste técnico.  
Funcionalidades incluem cadastro de produtos e categorias, controle de estoque, busca/filtragem e dashboard básico com gráfico.  

[Video de Apresentação do Projeto](https://youtu.be/XXvd-yHBp9k)

Tecnologias principais:
- **Backend:** .NET 9, C#, FluentValidation
- **Banco de dados:** MongoDB (inicialmente em memória)  
- **Frontend:** React, TypeScript, TailwindCSS, Shadcn/ui, React Query, Axios e Zod  
- **Autenticação:** (Inicialmente dados mockados)
- **Outras ferramentas:** Docker, Swagger

---

## O que foi implementado
### Backend
- [x]  CRUD de produtos (GET, POST, PUT, DELETE)
- [x]  CRUD de categorias
- [x]  Listagem de produtos por categoria
- [x]  Busca de produtos por nome
- [x]  Integração com MongoDB
- [x]  Swagger documentando a API
- [ ]  Autenticação com Keycloak
- [ ]  Testes unitários e de integração completos

### Frontend
- [x]  Listagem de produtos e categorias
- [x]  Cadastro de produtos e categorias
- [x]  Edição de produtos e categorias
- [x]  Dashboard com métricas (estoque, total de produtos)
- [x]  Gráfico por categoria
- [ ]  Autenticação e proteção de rotas
- [ ]  Testes de componentes e integração

### Infraestrutura
- [x]  Docker Compose rodando backend, frontend e MongoDB
- [x]  População de dados de exemplo

## Rotas API

#### Produtos

| Método | Rota | Descrição |
|--------|------|-----------|
| GET | `/api/products` | Lista todos os produtos |
| POST | `/api/products` | Cria um novo produto |
| GET | `/api/products/{id}` | Retorna um produto por ID |
| PUT | `/api/products/{id}` | Atualiza um produto existente |
| DELETE | `/api/products/{id}` | Remove um produto |
| GET | `/api/products/search?name=term` | Busca produtos por nome |
| GET | `/api/products/by-category?category=name` | Filtra produtos por categoria |

#### Categorias

| Método | Rota | Descrição |
|--------|------|-----------|
| GET | `/api/categories` | Lista todas as categorias |
| POST | `/api/categories` | Cria uma nova categoria |
| PUT | `/api/categories/{id}` | Atualiza uma categoria |
| DELETE | `/api/categories/{id}` | Remove uma categoria |

---

## Exemplos de requisição
Criar um produto

**POST /api/products**
```json
{
  "name": "Chair",
  "description": "Comfortable chair",
  "price": 120.5,
  "categoryId": 2,
  "stock": 15
}
```

## Rotas Frontend
### Dashboard
| Caminho | Página      | Descrição                                                               |
| ------- | ----------- | ----------------------------------------------------------------------- |
| `/`     | `Dashboard` | Página inicial com métricas gerais (produtos, estoque, categorias etc.) |

### Produtos
| Caminho                | Página          | Descrição                                   |
| ---------------------- | --------------- | ------------------------------------------- |
| `/products`            | `ListProducts`  | Lista todos os produtos                     |
| `/products/new`        | `CreateProduct` | Formulário para cadastrar um novo produto   |
| `/products/:id`        | `ShowProduct`   | Exibe detalhes de um produto específico     |
| `/products/:id/update` | `UpdateProduct` | Formulário para editar um produto existente |
| `/products/low-stock`  | `LowStock`      | Lista produtos com estoque baixo            |


### Categorias
| Caminho                  | Página           | Descrição                                      |
| ------------------------ | ---------------- | ---------------------------------------------- |
| `/categories`            | `ListCategories` | Lista todas as categorias                      |
| `/categories/new`        | `CreateCategory` | Formulário para criar uma nova categoria       |
| `/categories/:id/update` | `UpdateCategory` | Formulário para editar uma categoria existente |


## Instalação e execução

 Pré-requisitos
- Node.js 18+
- npm ou yarn
- .NET 9 SDK
- MongoDB
- Docker

### Instalação e Execução
```bash
# Clone o repositório
git clone https://github.com/WesRamox/hypesoft-challenge.git
cd hypesoft-challenge

# Execute toda a aplicação com Docker Compose com o Docker Desktop rodando
docker-compose up -d

# Aguarde alguns segundos para os serviços iniciarem
# Verifique se todos os containers estão rodando
docker-compose ps
```

## URLs de Acesso
- Frontend: http://localhost:3000
- API: http://localhost:5000
- Swagger: http://localhost:5000/swagger
- MongoDB Express: http://localhost:8081


### Desenvolvimento Local
Para iniciar backend isoladamente
```bash
  # Para desenvolvimento do frontend
  cd frontend
  npm install
  npm run dev

  # Para desenvolvimento do backend
  cd api/src/Hypesoft.API
  dotnet restore
  dotnet run
```

## Próximos Passos

- Página de buscas e paginação de produtos
- Autenticação com Keycloak
- Testes de integração end-to-end
- Cache com Redis para consultas frequentes
- Melhorias de UI/UX (Responsividade)
