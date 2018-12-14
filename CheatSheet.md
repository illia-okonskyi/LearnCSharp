# Cheat sheet

## LocalDb
- Connection string: `Server=(localdb)\MSSQLLocalDB;Database=<DBNAME>;Trusted_Connection=True;MultipleActiveResultSets=true`

- Connect to local-db (vs2015/2017): 
	- Run as administrator
	- Server explorer -> Data connections -> Add connection -> Microsoft SQL Server -> [Server name] "(localdb)\MSSQLLocalDB"; Select database -> OK


## Entity Framework Core
- Command-line tool: `dotnet ef`
	- Initial migration: `dotnet ef migrations add Initial`
	 
