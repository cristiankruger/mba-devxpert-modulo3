# **DevXpert.Modulo3 - Plataforma de Educação Online**

## :trophy: **1. Apresentação**

Bem-vindo ao repositório do projeto **DevXpert.Modulo3**. Este projeto é uma entrega do MBA DevXpert Full Stack .NET referente ao **módulo 3 - Plataforma de Educação Online**.
O objetivo principal desenvolver uma Plataforma de Educação Online que permite aos usuários logados com perfil Admin manter curso e aulas. 
Permite também aos usuários logados com perfil Aluno, se matricularem em um determinado curso (desde que liberado para matricula), efetuar o pagamento [//TODO]

### :notebook: **Autor**
---

- :white_check_mark: Cristian Kruger Silva - @mr.krug3r

## :gear: **2. Proposta do Projeto**

O projeto consiste em:

- **API RESTful:** Exposição dos recursos da Gestão da Plataforma de Educação Online para integração com outras aplicações ou desenvolvimento de front-ends alternativos.
- **Boas práticas:** Modelagem e desenvolvimento seguindo as boas práticas, como aplicação de princípios SOLID, DDD, Bounded Context, AggregateRoot, TDD etc.
- **Autenticação e Autorização:** Implementação de controle de acesso, diferenciando Admins, Alunos (JWT).
- **Acesso a Dados:** Implementação de acesso ao banco de dados através de ORM Entity Framework Core.

## :gear: **3. Tecnologias Utilizadas**

- **Linguagem de Programação:** C# 13
- **Frameworks:**
  - ASP.NET Core Web API  
  - Entity Framework Core  
  - Xunit
- **Banco de Dados:** SQL Server / SQLite
- **Autenticação e Autorização:**
  - ASP.NET Core Identity
  - JWT (JSON Web Token) para autenticação na API
- **Documentação da API:** Swagger / Postman
- **Testes:** 
  - XUnit
  - Bogus

## :gear: **4. Estrutura do Projeto**

A estrutura do projeto é organizada da seguinte forma:

```
|-- docs
|   |-- postman                                      → Coleção postman com requisições para API
|-- sql
|   |-- script.sql                                   → Script idempotente do banco de dados (exclusivo SQL Server)
|-- src
|   |-- DevXpert.Modulo3.API                         → API RESTful.
|   |-- DevXpert.Modulo3.Aluno.Application           → Service do contexto de Aluno
|   |-- DevXpert.Modulo3.Aluno.Data                  → Acesso à dados do contexto de Aluno
|   |-- DevXpert.Modulo3.Aluno.Domain                → Domínio do Bounded Context de Aluno
|   |-- DevXpert.Modulo3.Curso.Application           → Service do contexto de Curso
|   |-- DevXpert.Modulo3.Curso.Data                  → Acesso à dados do contexto de Curso
|   |-- DevXpert.Modulo3.Curso.Domain                → Domínio do Bounded Context de Curso
|   |-- DevXpert.Modulo3.Pagamento.Application       → Service do contexto de Pagamento
|   |-- DevXpert.Modulo3.Pagamento.Data              → Acesso à dados do contexto de Pagamento
|   |-- DevXpert.Modulo3.Pagamento.Domain            → Domínio do Bounded Context de Pagamento
|-- Tests
|   |-- DevXpert.Modulo3.API.Tests                   → Testes de Integração
|   |-- DevXpert.Modulo3.Aluno.Application.Tests     → Testes de Unidade da service de Aluno
|   |-- DevXpert.Modulo3.Aluno.Domain.Tests          → Testes de Unidade do domínio de Aluno
|-- .gitignore                                       → Confguração de quais arquivos o Git não deve versionar.
|-- FEEDBACK.md                                      → Arquivo para Consolidação dos Feedbacks
|-- DevXpert.Store.sln                               → solution do projeto
|-- README.md                                        → Arquivo de Documentação/Wiki do Projeto
```
## :gear: **5. Funcionalidades Implementadas**

- API:
  - **Autenticação via ASP.NET Core Identity.**
  - **CRUD para Categorias:** Permite ao Admin autenticado criar, editar, visualizar e excluir categorias.
  - **CRUD para Produtos:** Permite ao Vendedor autenticado criar, editar, visualizar e excluir Produtos. Permite ao Admin autenticado (in)ativar um produto de um vendedor.
  - **CRUD para favoritos:** Permite ao cliente autenticado adicionar ou remover um produto à sua lista de favoritos.
  - **API RESTful:** Exposição de endpoints para operações CRUD via API.
  - **Documentação da API:** Documentação automática dos endpoints da API utilizando Swagger.  

## :gear: **6. Como Executar o Projeto**

### **Pré-requisitos**

- .NET SDK 9.0 ou superior
- SQL Server / SQLite
- Visual Studio 2022 ou superior (ou qualquer IDE de sua preferência)
- Git

### **Passos para Execução**
---
### 1. **Clone o Repositório:**
   - `git clone https://github.com/cristiankruger/mba-devxpert-modulo3`
   - `cd mba-devxpert-modulo3`

### 2. Configuração do Banco de Dados:
  
No arquivo `appsettings.json`, configure a string de conexão do SQL Server (caso deseje executar em modo não "development"). Para execução em modo "Development" (debug), basta executar o projeto (irá subir uma instancia do `SQLite`).

Execute o projeto para que a configuração do Seed crie o banco e popule com os dados básicos.

**:warning: As Migrations são aplicadas de forma automática através do método de extensão `MigrateDatabase() => src/DevXpert.Modulo3.API/Configurations/DatabaseConfig.cs`;**<br>
**:warning: Uma carga inicial é feita na base de dados através do método `OnModelCreating()` de cada contexto (Identity, Aluno, Curso e/ou Pagamento), com base no método `Seed(modelBuilder)`.**<br>
**:warning: Credenciais default do banco:**
  - Usuário com perfil Admin &rarr; `admin@teste.com` | senha &rarr; `@Aa12345`<br>
  - Usuário com perfil Aluno &rarr; `vendedor@teste.com` | senha &rarr; `@Aa12345`<br>

### 3. **Executar o projeto:**
   - a partir da pasta clonada do projeto, abra o prompt de comando e digite:
   - `cd src/DevXpert.Modulo3.API/`
   - `dotnet run --environment=Development`
   - Abra o browser e acesse a aplicação em: http://localhost:5062

## :gear: **7. Instruções de Configuração**

**JWT para API:** As chaves de configuração do JWT estão no arquivo `appsettings.{environment}.json`.

**Migrações do Banco de Dados:** As migrações são gerenciadas pelo Entity Framework Core (Não é necessário aplicar o comando update-database devido a configuração do projeto)

## :gear: **8. Documentação da API**

A documentação da API está disponível através do Swagger. Após iniciar a API, acesse a documentação em através do link [http://localhost:5062/swagger](http://localhost:5062/swagger)

**:warning: Obs.: Em ambientes não `development`, é necessário informar usuario e senha para expor a página do swagger, devido à implementação do securityMiddleware. Por default, essas credenciais são `admin` e `123` e podem ser alteradas através do nó `AppCredentials` no `appsettings.[ambiente].json`**

## :white_check_mark: **9. Avaliação**

- Este projeto é parte de um curso acadêmico e não aceita contribuições externas. 
- Para feedbacks ou dúvidas utilize o recurso de Issues
- O arquivo `FEEDBACK.md` é um resumo das avaliações do instrutor e deverá ser modificado apenas por ele.

## :white_check_mark: **Casos de uso**

//TODO:
### Login de Usuário com perfil Admin
- //TOOD: DESCREVER A FEATURE

### Login de Usuário com perfil Aluno
