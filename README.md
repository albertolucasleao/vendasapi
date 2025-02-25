# vendasapi

# Sale API

Este repositório contém o teste técnico da empresa Taking para a posição de desenvolvedor na Ambev. O projeto consiste no desenvolvimento de um CRUD de Vendas, incluindo testes unitários e um exemplo de publicação em uma fila.

## Tecnologias Utilizadas

- [.NET 8](https://dotnet.microsoft.com/pt-br/download/dotnet/8.0)
- [RabbitMQ](https://www.rabbitmq.com/)
- [Docker](https://www.docker.com/)
- [PostgreSQL](https://www.postgresql.org/)

## Para rodar a aplicação no ambiente Docker

```sh
docker-compose up
```

## Para crair as tabelas no banco de dados PostgreSQL

```sh
dotnet ef migrations add Initial
dotnet ef database update
```


