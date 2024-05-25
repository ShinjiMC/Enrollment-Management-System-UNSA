# Dexo Corp UNSA System

By:

- Mogollon Caceres Sergio Daniel
- Davis Coropuna Leon Felipe
- Apaza Apaza Nelzon Jorge
- Lupo Condori Avelino
- Maldonado Casilla Braulio Nayap
- Parizaca Mozo Paul Antony
- Huaman Coaquira Luciana Julissa

## Backend Dependencies
- Move to src:
```bash
cd src
``` 
- EF Core CLI:
```bash
dotnet tool install --global dotnet-ef
```
- EF Core CLI:
```bash
dotnet add package Microsoft.EntityFrameworkCore.Design
```



- Add EntityFrameworkCore.MySql dependency:
```bash
dotnet add package Pomelo.EntityFrameworkCore.MySql --version 9.0.0-preview.1
```


## Run project

```bash
dotnet run --project src/src.csproj
```

## Build project

```bash
dotnet build src/src.csproj --configuration Release
```