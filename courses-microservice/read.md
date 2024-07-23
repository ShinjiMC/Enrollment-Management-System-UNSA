```bash
dotnet ef migrations add InitialMigration -p Infrastructure -s Web.API -o Persistence/Migrations --verbose
dotnet ef database update -p Infrastructure/ -s Web.API/

```

```bash
cd test
dotnet new xunit -o Application.Customers.UnitTests
```