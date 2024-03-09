# Dev Freela

[![NPM](https://img.shields.io/github/license/jehveiga/Blog-api)](https://github.com/jehveiga/dev-freela/blob/main/LICENSE)

# Sobre o projeto

Desenvolvimento de uma aplicação Web API Rest usando arquitetura limpa, usando organização de camadas (Core, Infrastructure, Application, API e Test).
Abordando injeção de dependência, persistencia e consultas usando Entity Framework como ORM escolhido, usado DataBase First para criação do banco da aplicação, usando migrations.
Foi aplicado CQRS pattern para persistencias e consultas ao banco na camada Application usando Commands e Querys.
Usado padrão repository para implementação com o banco e a aplicação junto com o padrão CQRS.
Implementado para validação das classes InputModels e ViewModel de entrada/saida de dados na Controllers o FluentValidation para validação de dados, implementado Filter para validação diretamente da Requisição para evitar a validação em cada método da Controller.
Usado um padrão de salvar a senha criptografando a mesma para persistencia no banco, na implementaçào do Login.
Aplicado autenticação e autorização usando JWT para ser adicionado Claims do usuário para validações de permissões.
Adicionado Camada de Teste Unitário utilizando XUnit com a biblioteca NSubstitute.
Implementado serviço de mensageria utilizando RabbitMQ simulando pagamento do serviço da conclusão do projeto pelo desenvolvedor utilizando microserviço para simular serviço de pagamento através de mensageria.
Aplicado Filtragem de dados para consultas de projetos.
Implementado paginação de forma genérica focando o reuso, para facilitar e otimizar o consumo no front-end para apresentação ao cliente.
Uitlizado o padrão Unit Of Work para focalizar as consultas focando no reuzo e organização do código e utlizando nas mesmas classes Transações se algo ocorrer no banco com uma transação mal realizada será feito o Roll Back da transação.

# Apresentação Web API - Dev Freela

## Visualizando os End-Points - demonstração dos end-points do projeto Web API


## Back end

- C#

## Outras Tecnologias

- JWT
- RabbitMQ
- CQRS
- Fluent Validation
- Swagger
- xUnit
- Asp.Net Web API
- Entity Framework Core

## Banco de Dados

- SQL Server

# Autor 

Jefferson Veiga

https://www.linkedin.com/in/jefferson-veiga-dev/
