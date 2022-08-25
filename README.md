# AdviceSlipService

Sample web api that replicates Advice Slip JSON API

## Requirements

Implement an application that provides a list of phrases related to a topic it inserts.

The application provides a REST API method called **GiveMeAdvice**.

The method shall accept the following parameters:
- **topic**, required, a string containing the topic
- **amount**, optional, an integer indicating the maximum amount of phrases to return

The response must be a list of advices related to the given parameter, each represented
by a string.

The Advice Slip JSON public API (https://api.adviceslip.com/) should be used as the data
source.

More infos in the pdf doc [Software Engineer Assignment](/Software%20Engineer%20Assignment%20.pdf)

> There is no limit on the usage of external frameworks or libraries


## Execution

Per realizzare questo progetto ho utilizzato asp dotnet core + dotnet6 + C#, il tutto tramite Visual Studio 2022.

Per lanciare il progetto è necessario fare il clone del repository ed aprire con VS2022 la solution `AdviceSlip.sln` presente nel folder `src`.

All'interno della solution sono presenti due progetti:
- AdviceSlipService : web api che implementa i requisiti
- AdviceSlipServiceTests : test di integrazione sulla web api

Il progetto utilizza un controller per esporre l'endpoint `GiveMeAdvice`.
Il controller a sua volta utilizza un service che implementa l'interfaccia `IAdviceSlipProviderService`.

Il service utilizza la classe `MemoryCache` messa a disposizione del framework per gestire una cache locale in memoria relativa alle chiamate all'endpoint.

Per il logging ho utilizzato [Serilog](https://serilog.net/) che consente di scrivere i log oltre che su console anche su file, su database o su servizi centralizzati tipo [Seq](https://datalust.co/seq).


## Riferimenti

Di seguito i post che ho consultato per chiarirmi le idee sull'utilizzo della cache e sull'implementazione dei test.

- [Cache in-memory in ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/performance/caching/memory?view=aspnetcore-6.0)
- [In-memory caching in ASP.NET Core](https://blexin.com/it/blog/in-memory-caching-in-asp-net-core/)
- [How to test your C# Web API](https://timdeschryver.dev/blog/how-to-test-your-csharp-web-api)
- [Should you unit-test API/MVC controllers in ASP.NET Core?](https://andrewlock.net/should-you-unit-test-controllers-in-aspnetcore/)
- [Getting Started with xUnit.net](https://xunit.net/docs/getting-started/netcore/visual-studio)
- [Serilog Best Practices](https://benfoster.io/blog/serilog-best-practices/)



