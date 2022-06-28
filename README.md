# MicroServiceBase

Um repositório para estudo/teste de microsserviços com .NET

## Ambiente

* Postgres Auditoria
* Postgres Cadastro
* RabbitMQ
* ElasticSearch + Kibana

Na raiz da solução:

```console
$ docker-compose -f docker-compose.env.yml up -d
```

## Roadmap

### Arquitetura / Repositorio

- [ ]  README
  - [x] Ambiente
  - [x] Organizar Roadmap
  - [ ] Documentações ([#11](https://github.com/tuliopaim/MicroServiceBase/issues/11))
     - [ ] Arquitetura
     - [ ] Core
     - [ ] Aditoria
     - [ ] Cadastro
     - [ ] Email
- [x] Separar DBs por Service ([#12](https://github.com/tuliopaim/MicroServiceBase/issues/12))
- [ ] Separar Services em Repositórios ([#13](https://github.com/tuliopaim/MicroServiceBase/issues/13))
  - [ ] Repositório Pai com Docker e Docs
  - [ ] Services em Submodules
  
### Core

- [x]  EntidadeBase / EntidadeAuditavel
- [x]  Suporte a Hateoas
- [x]  Mediator / CQRS
    - [x]  Command, Query, Event
    - [x]  ValidationPipeline
    - [x]  ExceptionPipeline
    - [x]  LogPipeline
- [x]  RabbitMQ ([#17](https://github.com/tuliopaim/MicroServiceBase/issues/17))
    - [x]  Estrutura rabbit
    - [x]  Lógica de retry
    - [x]  Utilizar em serviços
- [x]  EF Core
    - [x]  DbContextBase
    - [x]  Repositorios Genéricos
    - [x]  Paginação
    - [x]  Corrigir tipo de colunas DataCriacao/DataAlteracao ([#18](https://github.com/tuliopaim/MicroServiceBase/issues/18))
- [x]  Log
    - [x]  Serilog
    - [x]  ConsoleSink
    - [x]  EllasticSearchSink ([#6](https://github.com/tuliopaim/MicroServiceBase/issues/6))
- [x] Refatorar "Environment" para utilizar IConfiguration ([#19](https://github.com/tuliopaim/MicroServiceBase/issues/19))
- [ ] Remover Environment
- [ ] Implementar IDateTimeProvider

### Services

- [ ]  Microsserviço de Auditoria
  - [x] Extração de Auditoria no DbContextBase
  - [x] Implementar
  - [ ] Queries ([#15](https://github.com/tuliopaim/MicroServiceBase/issues/15))
- [ ]  Microsserviço de Email ([#7](https://github.com/tuliopaim/MicroServiceBase/issues/7))
  - [X] Envio de email
  - [ ] Persistência
  - [ ] Retry
- [ ]  Microsservico de Identidade ([#8](https://github.com/tuliopaim/MicroServiceBase/issues/8))
- [ ]  Autenticação/Autorização em endpoints ([#9](https://github.com/tuliopaim/MicroServiceBase/issues/9))
- [ ]  Gateway ([#10](https://github.com/tuliopaim/MicroServiceBase/issues/10))
  - [ ] Definir estratégia de Gateway
  - [ ] Implementar
