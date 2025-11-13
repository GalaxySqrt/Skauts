<div align="center">
  <h1>⚽ Skauts API ⚽</h1>
  <p>
    <strong>Uma API RESTful robusta em .NET 8 para gerenciamento de ligas, times e estatísticas esportivas.</strong>
  </p>
  <p>
    <em>Pronta para produção, containerizada com Docker e construída com arquitetura limpa.</em>
  </p>
  <p>
    <img src="[https://img.shields.io/badge/.NET-8.0-blueviolet?style=for-the-badge&logo=.net](https://img.shields.io/badge/.NET-8.0-blueviolet?style=for-the-badge&logo=.net)" alt=".NET 8">
    <img src="[https://img.shields.io/badge/SQL%20Server-blue?style=for-the-badge&logo=microsoftsqlserver](https://img.shields.io/badge/SQL%20Server-blue?style=for-the-badge&logo=microsoftsqlserver)" alt="SQL Server">
    <img src="[https://img.shields.io/badge/Docker-gray?style=for-the-badge&logo=docker](https://img.shields.io/badge/Docker-gray?style=for-the-badge&logo=docker)" alt="Docker">
    <img src="[https://img.shields.io/badge/licen%C3%A7a-MIT-green?style=for-the-badge](https://img.shields.io/badge/licen%C3%A7a-MIT-green?style=for-the-badge)" alt="Licença MIT">
  </p>
</div>

## 📖 Sobre o Projeto

**Skauts** não é apenas mais uma API de CRUD. É uma plataforma de back-end completa, projetada para servir como o cérebro por trás de uma aplicação de gerenciamento esportivo (como um "Football Manager" ou um "fantasy game").

Ela gerencia entidades complexas, desde Organizações (ligas) e Usuários até Jogadores, Partidas e os Eventos que ocorrem nelas (como gols, cartões e assistências). A arquitetura é focada em serviços, desacoplada e utiliza um sistema inteligente de **multi-tenancy por organização** para garantir que os dados de uma liga nunca se misturem com os de outra.

-----

## 🚀 Principais Features

  * **Autenticação JWT:** Sistema de login seguro com JSON Web Tokens.
  * **Multi-Organização (Multi-Tenancy):** Um usuário pode pertencer a várias organizações. A API filtra dados dinamicamente baseado na organização selecionada no token.
  * **Gerenciamento Completo:** CRUDs para todas as entidades principais:
      * `Organizações`
      * `Campeonatos`
      * `Times`
      * `Jogadores` (com atributos como `Skill` e `Physique`)
      * `Partidas`
  * **Estatísticas Detalhadas:** Rastreie `Eventos` por partida (Gols, Cartões) e `Prêmios` por jogador (Melhor em Campo).
  * **Banco de Dados:** Utiliza EF Core com Migrations e um script de `Seed.sql` para popular o banco com dados de exemplo (jogadores, times, admin, etc.).
  * **Containerização Total:** 100% pronto para rodar com `docker-compose`, incluindo o banco de dados SQL Server.

-----

## 🛠️ Stack Tecnológica

O projeto foi construído utilizando as seguintes tecnologias:

  * **.NET 8:** A mais recente plataforma de desenvolvimento da Microsoft.
  * **ASP.NET Core 8:** Para a construção da API RESTful.
  * **Entity Framework Core 8:** ORM para interação com o banco de dados.
  * **SQL Server:** Banco de dados relacional (configurado para rodar no Docker).
  * **Docker & Docker Compose:** Para criação de ambientes de desenvolvimento e produção consistentes.
  * **AutoMapper:** Para mapeamento limpo entre Entidades e DTOs.
  * **BCrypt.Net-Next:** Para hashing seguro de senhas.
  * **Swagger (OpenAPI):** Para documentação e teste interativo da API.

-----

## 🏛️ Arquitetura e Conceitos-Chave

O design da API segue uma abordagem de **Arquitetura Limpa** (ou similar), separando claramente as responsabilidades:

  * **`Models/`:** Entidades puras do EF Core.
  * **`DTOs/`:** Objetos de Transferência de Dados, incluindo DTOs específicos `Salvar...Dto` para criação e atualização (semelhante ao padrão CQS).
  * **`Services/Interfaces/`:** Contratos que definem a lógica de negócio.
  * **`Services/Implementations/`:** Implementação concreta da lógica, injetada nos controllers.
  * **`Controllers/`:** Camada de API, responsável apenas por receber requisições e retornar DTOs.
  * **`Data/Context/`:** Configuração do `DbContext` do EF Core.

### Destaque: O Fluxo de Multi-Organização

A funcionalidade mais poderosa do Skauts é seu filtro de dados dinâmico.

1.  **Login Base:** O usuário faz login com email/senha em `/api/auth/login`.
2.  **Retorno:** A API retorna um *token base* e a lista de organizações que aquele usuário pode acessar (com base na tabela `UsersOrganizations`).
3.  **Seleção de Organização:** O front-end (ou o usuário) deve então chamar o endpoint `/api/auth/select-organization/{orgId}`.
4.  **Token Final:** A API gera um **novo token JWT** que contém a *claim* `"org_id"`.
5.  **Mágica no DbContext:** O `SkautsDbContext` é injetado com `IHttpContextAccessor`. Ele lê a claim `"org_id"` do token em *cada* requisição e aplica um `HasQueryFilter` global. Isso significa que qualquer consulta (ex: `_context.Players.ToListAsync()`) será *automaticamente* filtrada para a organização selecionada, garantindo isolamento total dos dados.

-----

## 🏁 Como Executar (Docker)

A forma mais fácil de rodar o projeto (com o banco de dados incluído) é usando o Docker Compose.

### Pré-requisitos

  * [Docker Desktop](https://www.docker.com/products/docker-desktop/) instalado.
  * Um editor de código (como VS Code).

### 1\. Configuração do Ambiente

O `docker-compose.yml` espera variáveis de ambiente. Crie um arquivo `.env` na raiz do projeto (onde o `docker-compose.yml` está) e preencha-o:

```.env
# Senha super secreta para o banco de dados SQL Server
DB_PASSWORD=my_StrongP@ssw0rd!

# Ambiente do ASP.NET Core
ASPNETCORE_ENVIRONMENT=Development

# Configurações do JWT (Use valores fortes e secretos em produção)
Jwt__Key=MINHA_CHAVE_SECRETA_SUPER_LONGA_PARA_HS256
Jwt__Issuer=https://api.skauts.com
Jwt__Audience=https://app.skauts.com
```

### 2\. Subindo os Contêineres

Abra um terminal na raiz do projeto e execute:

```bash
# O --build é importante na primeira vez para construir a imagem da API
docker-compose up -d --build
```

O Docker irá:

1.  Baixar a imagem do SQL Server e iniciar o banco `skauts-db` na porta `1433`.
2.  Construir a imagem da API `skauts-api`.
3.  Iniciar a API `skauts-api` na porta `8080`.
4.  A API aplicará as *migrations* e executará o *seed* do banco automaticamente na inicialização.

### 3\. Acessando a API

  * **Swagger (UI):** `http://localhost:8080/swagger`
  * **Base URL da API:** `http://localhost:8080`

### 4\. Testando o Login

O script `Seed.sql` cria usuários de exemplo:

  * **Usuário 1:**

      * **Email:** `admin@skauts.com`
      * **Senha:** `admin123`
      * **Acesso:** "Liga Skauts de Exemplo" (Org 1) e "AFR" (Org 2).

  * **Usuário 2:**

      * **Email:** `admin-afr@skauts.com`
      * **Senha:** `real123`
      * **Acesso:** Apenas "AFR" (Org 2).

Use o endpoint `/api/auth/login` no Swagger para obter seu token e, em seguida, o `/api/auth/select-organization/{orgId}` para começar a explorar.

-----

## 🗺️ Principais Endpoints

  * `POST /api/auth/login` - Autentica o usuário.
  * `POST /api/auth/select-organization/{orgId}` - Seleciona a organização e obtém o token final.
  * `GET /api/organizations` - Obtém as organizações (requer filtro desabilitado ou admin).
  * `GET /api/players` - Obtém os jogadores DA SUA organização.
  * `GET /api/teams` - Obtém os times DA SUA organização.
  * `GET /api/championships` - Obtém os campeonatos DA SUA organização.
  * `GET /api/matches` - Obtém as partidas DA SUA organização.
  * `POST /api/events` - Adiciona um novo evento (Gol, Cartão) a uma partida.

...e muito mais, explore pelo Swagger\!

-----

## 📜 Licença

Este projeto é distribuído sob a licença MIT. Veja o arquivo `LICENSE.txt` para mais detalhes.
