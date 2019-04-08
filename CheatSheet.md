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
	- To prevent memory leaks ASP.NET Core performs checks when scoped services are requested from the global scope  
	  (it's like singletone object obtains service lifetime of which must enclosed within the HTTP request scope and this  
	  causes scoped service will be never disposed). So, for such situations the explicit scope of the services is needed:  
	  `using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope()) {...}`


- Example write logs to file: https://andrewlock.net/creating-a-rolling-file-logging-provider-for-asp-net-core-2-0/

- Tag helpers (overview, built-in tag helpers are described in the other articles of the group): https://docs.microsoft.com/en-us/aspnet/core/mvc/views/tag-helpers/intro?view=aspnetcore-2.2

## Tips & Tricks
- Performance Tuning for .NET Core: https://reubenbond.github.io/posts/dotnet-perf-tuning
- All About Span: Exploring a New .NET Mainstay: https://msdn.microsoft.com/en-us/magazine/mt814808.aspx

## Manuals and guidelines
- C# frameworks manuals and guidelines on Russian: https://metanit.com/sharp/

## Tools
- Fiddler: standalone HTTP debuging tool www.telerik.com/fiddler

## CSS
- The CSS `only-child` pseudo-class (https://css-tricks.com/almanac/selectors/o/only-child/) can be used to create placeholder row:  
```
<style>
.placeholder { visibility: collapse; display: none }
.placeholder:only-child { visibility: visible; display: flex }
</style>
<div class="<OUTER>">
  <div class="row placeholder">
    <div class="col text-center p-2">No Data</div>
  </div>
  @foreach (var entry in Model) { ... }
</div>
```