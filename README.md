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
  - [ ] Documentações ([#11](https://github.com/tuliopaim/MicroServiceBase/issues/11))
     - [ ] Arquitetura
     - [ ] Core
     - [ ] Aditoria
     - [ ] Cadastro
     - [ ] Email
- [ ] Separar DBs por Service ([#12](https://github.com/tuliopaim/MicroServiceBase/issues/12))
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
- [x]  Kafka
    - [x]  KafkaBroker com facilitadores
    - [x]  BackgroundService disparando Consumers Kafka
    - [x]  Criar tópicos no statup da aplicação ([#14](https://github.com/tuliopaim/MicroServiceBase/issues/14))
- [x]  EF Core
    - [x]  DbContextBase
    - [x]  Repositorios Genéricos
    - [x]  Paginação
- [ ]  Log
    - [x]  Serilog
    - [x]  ConsoleSink
    - [ ]  EllasticSearchSink ([#6](https://github.com/tuliopaim/MicroServiceBase/issues/6))

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
