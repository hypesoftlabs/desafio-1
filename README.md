# Desafio Técnico Hypesoft - Sistema de Gestão de Produtos

## Visão Geral

Bem-vindo ao desafio técnico da Hypesoft! Este projeto consiste no desenvolvimento de um sistema completo de gestão de produtos, demonstrando suas habilidades em arquitetura moderna, boas práticas de desenvolvimento e tecnologias de ponta.

## 📊 Status do Projeto

### ✅ Concluído
- [x] Configuração do monorepo
- [x] Docker Compose com todos os serviços
- [x] Estrutura Clean Architecture (.NET 9)
- [x] Frontend Next.js 14 com TypeScript
- [x] Configuração MongoDB com dados de exemplo
- [x] Keycloak configurado com realm e usuários
- [x] Nginx como reverse proxy
- [x] Estrutura de testes

### 🚧 Em Desenvolvimento
- [ ] Entidades de domínio (Categories, Products)
- [ ] Implementação CQRS + MediatR
- [ ] APIs REST com Swagger
- [ ] Integração Keycloak no backend
- [ ] Componentes frontend
- [ ] Autenticação no frontend

### 📝 Próximos Passos
- [ ] Dashboard com métricas
- [ ] CRUD completo de produtos
- [ ] Sistema de categorias
- [ ] Controle de estoque
- [ ] Testes automatizados
- [ ] Documentação completa

## Referência Visual

O design da aplicação deve seguir o padrão visual moderno demonstrado neste protótipo:

**ShopSense Dashboard - Product Page**: https://dribbble.com/shots/24508262-ShopSense-Dashboard-Product-Page

## Requisitos do Sistema

### Funcionalidades Principais

#### Gestão de Produtos
- Criar, listar, editar e excluir produtos
- Cada produto deve conter: nome, descrição, preço, categoria, quantidade em estoque
- Validação básica de dados obrigatórios
- Busca simples por nome do produto

#### Sistema de Categorias
- Criar e gerenciar categorias de produtos (lista simples)
- Associar produtos a uma categoria
- Filtrar produtos por categoria

#### Controle de Estoque
- Controlar quantidade em estoque de cada produto
- Atualização manual de estoque
- Exibir produtos com estoque baixo (menor que 10 unidades)

#### Dashboard Simples
- Total de produtos cadastrados
- Valor total do estoque
- Lista de produtos com estoque baixo
- Gráfico básico de produtos por categoria

#### Sistema de Autenticação
- Integração com **Keycloak** para autenticação
- Login via Keycloak (OAuth2/OpenID Connect)
- Proteção de rotas no frontend
- Autorização baseada em roles do Keycloak
- Logout integrado com Keycloak

### Requisitos Técnicos

#### Performance
- Resposta da API em menos de 500ms para consultas simples
- Paginação eficiente para grandes volumes
- Cache para consultas frequentes
- Otimização de queries no banco

#### Escalabilidade
- Arquitetura preparada para crescimento horizontal
- Separação clara entre camadas
- Padrões que facilitem manutenção e evolução
- Código limpo e bem estruturado

#### Segurança
- Rate limiting para prevenir abuso
- Validação e sanitização de entradas
- Headers de segurança adequados
- Tratamento seguro de dados sensíveis

#### Disponibilidade
- Health checks implementados
- Tratamento adequado de erros
- Mensagens de erro claras e úteis
- Logs estruturados para monitoramento

#### Usabilidade
- Interface responsiva (desktop e mobile)
- Validação em tempo real nos formulários
- Feedback visual para ações do usuário
- Experiência intuitiva e consistente

## Stack Tecnológica

### Frontend
- **React 18** com TypeScript
- **Vite** ou **Next.js 14** (App Router)
- **TailwindCSS** + **Shadcn/ui** para estilização
- **React Query/TanStack Query** para gerenciamento de estado
- **React Hook Form** + **Zod** para validação
- **Recharts** ou **Chart.js** para dashboards
- **React Testing Library** + **Vitest** para testes

### Backend
- **.NET 9** com C#
- **Clean Architecture** + **DDD** (Domain-Driven Design)
- **CQRS** + **MediatR** pattern
- **Entity Framework Core** com MongoDB provider
- **FluentValidation** para validação
- **AutoMapper** para mapeamento
- **Serilog** para logging estruturado
- **xUnit** + **FluentAssertions** para testes

### Infraestrutura
- **MongoDB** como banco principal
- **Keycloak** para autenticação e autorização
- **Docker** + **Docker Compose** para containerização
- **Nginx** como reverse proxy

## Arquitetura do Sistema

### Backend - Clean Architecture + DDD

```
src/
├── Hypesoft.Domain/              # Camada de Domínio
│   ├── Entities/                 # Entidades do domínio
│   ├── ValueObjects/             # Objetos de valor
│   ├── DomainEvents/            # Eventos de domínio
│   ├── Repositories/            # Interfaces dos repositórios
│   └── Services/                # Serviços de domínio
├── Hypesoft.Application/         # Camada de Aplicação
│   ├── Commands/                # Comandos CQRS
│   ├── Queries/                 # Consultas CQRS
│   ├── Handlers/                # Handlers MediatR
│   ├── DTOs/                    # Data Transfer Objects
│   ├── Validators/              # Validadores FluentValidation
│   └── Interfaces/              # Interfaces da aplicação
├── Hypesoft.Infrastructure/      # Camada de Infraestrutura
│   ├── Data/                    # Contexto e configurações EF
│   ├── Repositories/            # Implementação dos repositórios
│   ├── Services/                # Serviços externos
│   └── Configurations/          # Configurações de DI
└── Hypesoft.API/                # Camada de Apresentação
    ├── Controllers/             # Controllers da API
    ├── Middlewares/             # Middlewares customizados
    ├── Filters/                 # Filtros de ação
    └── Extensions/              # Extensões de configuração
```

### Frontend - Arquitetura Modular

```
src/
├── components/                   # Componentes reutilizáveis
│   ├── ui/                      # Componentes base (shadcn/ui)
│   ├── forms/                   # Componentes de formulário
│   ├── charts/                  # Componentes de gráficos
│   └── layout/                  # Componentes de layout
├── pages/                       # Páginas da aplicação
├── hooks/                       # Custom hooks
├── services/                    # Serviços de API
├── stores/                      # Stores de estado global
├── types/                       # Definições de tipos
├── utils/                       # Funções utilitárias
└── lib/                         # Configurações de bibliotecas
```

## Diferenciais

#### Testes Abrangentes
- Cobertura mínima de 85% no backend
- Testes E2E com Playwright ou Cypress
- Testes de integração para todos os endpoints
- Testes unitários para regras de negócio
- Testes de mutação para validar qualidade

#### Observabilidade Completa
- Logs estruturados com correlationId
- Métricas customizadas para monitoramento
- Health checks detalhados para todos os serviços
- Tratamento adequado de erros com contexto
- Monitoring de performance da aplicação

#### Performance e Otimização
- Server-side rendering (Next.js)
- Code splitting e lazy loading
- Estratégias de caching (Redis + HTTP cache)
- Indexação otimizada do banco de dados
- Otimização de imagens e assets
- Compressão de responses

#### Segurança Avançada
- Integração completa com Keycloak
- Proteção de rotas baseada em roles
- Token JWT validado adequadamente
- CORS configurado adequadamente
- Headers de segurança implementados
- Validação em múltiplas camadas

#### Qualidade de Código
- Princípios SOLID aplicados consistentemente
- Clean Code em todas as camadas
- Padrões de design bem implementados
- Documentação inline adequada
- Tratamento de exceções robusto

#### Documentação Excepcional
- OpenAPI/Swagger com exemplos detalhados
- Documentação de arquitetura (C4 Model)
- ADRs (Architecture Decision Records)
- Guias de instalação e execução completos
- Collection do Postman atualizada

### Pontos Extras (Opcionais)

- **Roles avançadas no Keycloak** (Admin, Manager, User)
- **GraphQL** como alternativa à REST API
- **Real-time updates** via SignalR/WebSockets
- **Exportação de relatórios** em PDF
- **Internacionalização** (i18n) básica
- **PWA** com capacidades offline
- **Docker multi-stage builds** otimizados

## Como Executar

### Pré-requisitos
- Docker Desktop 4.0+
- Node.js 18+
- .NET 9 SDK
- Git

### Instalação e Execução

```bash
# Clone o repositório
git clone https://github.com/seu-usuario/hypesoft-challenge.git
cd hypesoft-challenge

# Copie as variáveis de ambiente
cp .env.example .env

# Execute toda a aplicação com Docker Compose
docker-compose up -d

# Aguarde alguns segundos para os serviços iniciarem
# Verifique se todos os containers estão rodando
docker-compose ps
```

### URLs de Acesso
- **Frontend**: http://localhost:3000
- **API**: http://localhost:5000
- **Swagger**: http://localhost:5000/swagger
- **MongoDB Express**: http://localhost:8081
- **Keycloak**: http://localhost:8080

### Desenvolvimento Local

```bash
# Para desenvolvimento do frontend
cd frontend
npm install
npm run dev

# Para desenvolvimento do backend
cd backend
dotnet restore
dotnet run

# Para executar testes
dotnet test
cd ../frontend
npm test
```

## Padrões de Commit

Este projeto utiliza [Conventional Commits](https://conventionalcommits.org/):

```bash
# Exemplos de commits
feat(products): add bulk import functionality
fix(api): resolve pagination issue in products endpoint
docs(readme): update installation instructions
test(products): add unit tests for product service
refactor(auth): improve JWT token validation
perf(database): optimize product search queries
style(frontend): apply consistent spacing in components
chore(deps): update dependencies to latest versions
```

### Tipos de Commit
- `feat`: Nova funcionalidade
- `fix`: Correção de bug
- `docs`: Documentação
- `style`: Formatação, ponto e vírgula, etc
- `refactor`: Refatoração de código
- `test`: Adição ou correção de testes
- `chore`: Tarefas de manutenção
- `perf`: Melhorias de performance
- `build`: Build e dependências

## Critérios de Avaliação

### Técnico (60%)
- **Arquitetura**: Clean Architecture, DDD, CQRS implementados corretamente
- **Qualidade de Código**: SOLID, Clean Code, padrões consistentes
- **Testes**: Cobertura, qualidade dos testes, cenários bem cobertos
- **Performance**: Otimizações, caching, queries eficientes
- **Segurança**: Implementação adequada de autenticação/autorização

### Funcional (25%)
- **Completude**: Todas as funcionalidades implementadas
- **UX/UI**: Interface intuitiva e responsiva
- **Validações**: Tratamento adequado de erros
- **Regras de Negócio**: Implementação correta dos requisitos

### Profissional (15%)
- **Documentação**: README completo, código bem documentado
- **Git Flow**: Commits organizados, branches bem estruturadas
- **Docker**: Compose funcionando perfeitamente
- **Extras**: Funcionalidades que demonstram expertise avançada

## Entregáveis

### Código Fonte
- Repositório GitHub público
- README detalhado (este arquivo)
- Docker Compose funcional
- Testes automatizados com boa cobertura

### Aplicação Funcionando
- Todos os serviços rodando via Docker Compose
- Banco de dados populado com dados de exemplo
- Interface funcional e responsiva

### Documentação
- API documentada com Swagger
- Guia de instalação e execução
- Documentação das decisões arquiteturais

### Apresentação
- Vídeo de 5-10 minutos demonstrando a aplicação
- Explicação das decisões técnicas tomadas
- Showcase das funcionalidades implementadas
- Demonstração dos diferenciais implementados


---

**Boa sorte e mostre do que você é capaz!**

---

*Este desafio foi criado para identificar desenvolvedores excepcionais que compartilham nossa paixão por tecnologia e excelência técnica. Estamos ansiosos para ver sua solução!*
