# Cheat sheet

## LocalDb
- Connection string: `Server=(localdb)\MSSQLLocalDB;Database=<DBNAME>;Trusted_Connection=True;MultipleActiveResultSets=true`

- Connect to local-db (vs2015/2017): 
	- Run as administrator
	- Server explorer -> Data connections -> Add connection -> Microsoft SQL Server -> [Server name] "(localdb)\MSSQLLocalDB"; Select database -> OK


## Entity Framework Core
- Command-line tool: `dotnet ef`
	- Initial migration: `dotnet ef migrations add Initial`
	 

## ASP.NET Core MVC
- Client-side validation: https://github.com/aspnet/jquery-validation-unobtrusive/
	- Fails for float and decimal for non english format: https://github.com/aspnet/jquery-validation-unobtrusive/issues/27

- DI:
	- https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-2.2
	- https://www.stevejgordon.co.uk/asp-net-core-dependency-injection-registering-multiple-implementations-interface
	- Action Injection (inject services directly to actions as arguments): supply argument with [FromServices] attribute
	- DI in views: use the `@inject` keyword

- Example write logs to file: https://andrewlock.net/creating-a-rolling-file-logging-provider-for-asp-net-core-2-0/

## Tips & Tricks
- Performance Tuning for .NET Core: https://reubenbond.github.io/posts/dotnet-perf-tuning
- All About Span: Exploring a New .NET Mainstay: https://msdn.microsoft.com/en-us/magazine/mt814808.aspx

## Manuals and guidelines
- C# frameworks manuals and guidelines on Russian: https://metanit.com/sharp/

## Tools
- Fiddler: standalone HTTP debuging tool www.telerik.com/fiddler
