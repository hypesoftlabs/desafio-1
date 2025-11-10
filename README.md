# Shop Manager

Aplicação **fullstack** para gestão de produtos, categorias e estoque, com:

- Backend em **.NET 9**, MongoDB, Redis, Keycloak e Nginx
- Frontend em **React + Vite + TypeScript**, com Tailwind, shadcn/ui, React Query e Recharts

---

## 🧱 Arquitetura do projeto

Estrutura geral de pastas:

- **backend/**
- API em ASP.NET 9 (DDD)
- MongoDB como banco de dados
- Redis para cache
- Autenticação/autorização com Keycloak (OpenID Connect)
- Swagger protegido por Keycloak
- Nginx como reverse proxy (`/api` → ShopAPI)

- **frontend/**
- React + Vite
- TypeScript
- Tailwind CSS
- shadcn/ui
- React Query + Axios
- Recharts para gráficos
- Integração com Keycloak (login/logout, usuário exibido no header)

---

## 🚀 Pré-requisitos

- [Docker](https://www.docker.com/)
- [Docker Compose](https://docs.docker.com/compose/)
- [Node.js 20+](https://nodejs.org/) (para rodar o frontend localmente)
- NPM ou Yarn (ex.: `npm`)

---

## ⚙️ Backend (API + Keycloak + Mongo + Redis + Nginx)

### Subir os serviços com Docker

No diretório **backend/**:

```bash
cd backend
docker compose up -d --build
```

## 💻 Frontend (React + Vite)
Importante: Para o frontend funcionar, o Backend deve estar rodando (via Docker).

### 1. Instalar dependências
Navegue até o diretório frontend/ e instale os pacotes (faça isso apenas na primeira vez):

```bash
cd frontend
npm install
```
### 2. Rodar o servidor de desenvolvimento
Ainda no diretório frontend/, inicie o servidor do Vite:

``
npm run dev
``
A aplicação estará disponível no endereço local indicado no terminal.
