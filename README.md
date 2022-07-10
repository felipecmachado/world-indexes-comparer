# world-indexes-comparer
An comprehensive world indexes comparer mainly focused on providing economic indexes comparison after covid-19 effects.

### About
This is a project created with the solely purpose of applying several topics that were presented during the [Software Engineering graduate program](https://www.unisinos.br/pos/especializacao/engenharia-de-software/hibrido/porto-alegre).

### Features

- [x] Consume and save Countries data in a relational database using a scheduled job.
- [x] Consume and save Covid data in a relational database using a scheduled job.
- [ ] Consume and save Inflation data in a relational database using a scheduled job.
- [ ] Provide API to compare effects after Covid-19 between selected countries.
- [ ] Architecture documents like C4 Model and ADRs

### Current Architecture

### Architecture Decision Records

### C4 Model

### OpenAPI Specification

### Technologies and patterns used

- [x] [NET 6](https://docs.microsoft.com/pt-br/dotnet/core/whats-new/dotnet-6)
- [x] Rich domain model using DDD (Domain Driven Design)
- [x] CQRS (Command Query Responsibility Segregation pattern)
- [x] ASP.NET WebAPI
- [x] Background workers using [cron expressions](https://github.com/HangfireIO/Cronos)
- [x] EntityFrameworkCore
- [x] UnitOfWork pattern using [EntityFrameworkCore.DataAccess library](https://github.com/ffernandolima/ef-core-data-access/tree/ef-core-6)
- [x] [MediatR](https://github.com/jbogard/MediatR)
- [ ] AutoMapper
- [ ] FluentValidation
- [x] MySql
- [x] Docker (including docker-compose)
- [ ] Redis
- [ ] Apache Kafka
- [ ] Unit tests using AAA pattern ([Arrange, Act, Assert](https://docs.microsoft.com/pt-br/visualstudio/test/unit-test-basics?view=vs-2022)): 

### Getting started

Make sure you have installed and configured docker in your environment. 
After that, you can run the below commands from the /src/ directory and get started with immediately.

```powershell
docker-compose build
docker-compose up
```

### Read further

### References:
    
    Kudos to the following contributors:

License This project is licensed under the MIT license. Copyright (c) 2022 Felipe C Machado.
