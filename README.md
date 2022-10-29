# Locatudo Api EFCore

Solução criada a partir do repositório [https://github.com/ferfujikawa/locatudo-fail-fast-validation](https://github.com/ferfujikawa/locatudo-fail-fast-validation).

## 1 Banco de Dados

A API utiliza um banco de dados PostgreSQL inicializado em um container Docker.

### 1.1 Inicialização do Container

Para inicializar o container, acesse o diretório `Docker/locatudo-postgresql` e execute o comando abaixo:

```bash
docker-compose up -d
```

### 1.2 Tabelas e registros

Ao subir o container, o script de criação do banco (`Docker/locatudo-postgresql/db-init-scripts`) será executado e criará o banco de dados e tabelas da API, além de alguns registros iniciais em algumas tabelas.

## 2 Execução da API

A API possui documentação *Swagger* portanto pode ser testada facilmente após inicialização através da CLI do .NET.

```bash
cd Locatudo.Api
dotnet watch run
```
