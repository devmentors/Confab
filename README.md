**What is Confab?**
----------------

Confab is an open source project using **modular monolith architecture** written in **[.NET 5.0](https://dotnet.microsoft.com/download/dotnet/5.0)**. The domain revolves around the conference organization including the activities such as managing users, speakers, tickets, call for papers, submissions, agendas and many more.

Depending on the module complexity, **different architectural styles are being used**, including simple **CRUD** approach, along with **CQRS**, **Clean Architecture** and **Domain-Driven Design** with the usage of so-called building blocks such as **aggregates** or domain services.

The modules are integrated with each other based on **event-driven** architecture, using the **local contracts** approach (instead of the shared projects which result in dependencies amongst the independent modules).

The database being used is [PostgreSQL](https://www.postgresql.org) + [EF Core](https://docs.microsoft.com/en-us/ef/core/) as ORM.

![Confab overview](https://raw.githubusercontent.com/devmentors/Confab/master/assets/confab_overview.png)

**Is there Web UI?**
----------------

Yes, there's [Confab.Web](https://github.com/devmentors/Confab.Web) written in **Blazor**  that integrates with exposed Web API.

**How to start the solution?**
----------------

Type the following command:

```
docker-compose up -d
```

It will start the required infrastructure using [Docker](https://docs.docker.com/get-docker/) in the background. Then, you can start the application under `src/Bootstrapper/Confab.Bootstrapper/` using your favorite IDE or CLI.

```
cd src/Bootstrapper/Confab.Bootstrapper
dotnet run
```

**What HTTP requests can be sent to the API?**
----------------

You can find the list of all HTTP requests in [Confab.rest](https://github.com/devmentors/Confab/blob/master/Confab.rest) file placed in the root folder.

This file is compatible with [REST Client](https://marketplace.visualstudio.com/items?itemName=humao.rest-client) plugin for [Visual Studio Code](https://code.visualstudio.com).

**Is this project being actively developed?**
----------------

Yes, the overall solution (backend + frontend) is under an active development, as we do plan to add more cool features in the future and also provide the sample transition to the microservices.