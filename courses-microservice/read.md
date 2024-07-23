## Configuración del Proyecto

Sigue estos pasos para configurar y construir tu proyecto desde la línea de comandos.

### 1. Crear la Solución y Directorio Principal

```bash
dotnet new sln -o courses-microservice
cd courses-microservice
```

### 2. Crear los Proyectos de Biblioteca

```bash
dotnet new classlib -o Domain
dotnet new classlib -o Application
dotnet new classlib -o Infrastructure
```

### 3. Crear el Proyecto Web API

```bash
dotnet new webapi -o Web.API
```

### 4. Construir la Solución

```bash
dotnet build
```

### 5. Agregar Referencias entre Proyectos

```bash
dotnet add Application/Application.csproj reference Domain/Domain.csproj
dotnet add Infrastructure/Infrastructure.csproj reference Application/Application.csproj
dotnet add Web.API/Web.API.csproj reference Application/Application.csproj Infrastructure/Infrastructure.csproj
```

### 6. Agregar Proyectos a la Solución

```bash
dotnet sln add Web.API/Web.API.csproj
dotnet sln add Application/Application.csproj
dotnet sln add Infrastructure/Infrastructure.csproj
dotnet sln add Domain/Domain.csproj
```

### 7. Construir y Ejecutar la Aplicación

```bash
cd src
dotnet build
dotnet run -p Web.API/
```

### 8. Migraciones de Entity Framework Core

```bash
dotnet ef migrations add InitialMigration -p Infrastructure -s Web.API -o Persistence/Migrations --verbose
dotnet ef database update -p Infrastructure/ -s Web.API/
```

<!--
### 9. Crear Proyectos de Prueba (opcional)

```bash
cd test
dotnet new xunit -o Application.Customers.UnitTests
```
-->
