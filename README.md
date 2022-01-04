# MicroServiceBase

Um repositório para estudo/teste de microsserviços com .NET

## Ambiente

* Postgres
* Kafka + Zookeeper
* ElasticSearch + Kibana

Na raiz da solução:

```console
$ cd docker
$ docker-compose -f docker-compose-dev.yml --env-file .env.dev up -d
```

## Roadmap

### Arquitetura / Repositorio

- [ ]  README
  - [x] Ambiente
  - [x] Organizar Roadmap
  - [ ] Documentação Arquitetura
  - [ ] Documentação Core
  - [ ] Documentação Aditoria
  - [ ] Documentação Cadastro
  - [ ] Documentação Email
- [ ] Separar DBs por Service
- [ ] Separar Services em Repositórios
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
- [ ]  Kafka
    - [x]  KafkaBroker com facilitadores
    - [x]  BackgroundService disparando Consumers Kafka
    - [ ]  Criar tópicos no statup da aplicação
- [x]  EF Core
    - [x]  DbContextBase
    - [x]  Repositorios Genéricos
    - [x]  Paginação
- [ ]  Log
    - [x]  Serilog
    - [x]  ConsoleSink
    - [ ]  EllasticSearchSink

### Services

- [x]  Microsserviço de Auditoria
  - [x] Extração de Auditoria no DbContextBase
  - [x] Implementar
- [ ]  Microsserviço de Email
  - [X] Envio de email
  - [ ] Persistência
  - [ ] Retry
- [ ]  Microsservico de Identidade
- [ ]  Autenticação/Autorização em endpoints
- [ ]  Gateway
  - [ ] Definir estratégia de Gateway
  - [ ] Implementar
