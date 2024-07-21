## Iniciar el Proyecto

Para iniciar el proyecto, siga los siguientes pasos:

### 1. Entrar al Directorio del Proyecto

Primero, navegue al directorio del microservicio de cursos:

```sh
cd courses-microservice
```

### 2. Navegar al Directorio `src`

Luego, entre al directorio `src` donde se encuentra el proyecto principal:

```sh
cd src
```

### 3. Ejecutar el Proyecto

Para ejecutar el proyecto, utilice el siguiente comando:

```sh
dotnet run --project src.csproj
```

### 4. Crear las Tablas de la Base de Datos

Para crear las tablas necesarias en la base de datos, ejecute el siguiente comando:

```sh
dotnet ef migrations add Init
```

Para llevar los cambios a la base de datos:

```sh
dotnet ef database update
```


### 5. Añadir Información Inicial

Si necesita añadir información inicial a la base de datos, siga estos pasos:

1. Descomente la línea `InitializeDatabase();` en src/context/MySqlDBIdentityContext.cs

2. Ejecute nuevamente el comando para actualizar la base de datos:
    ```sh
    dotnet ef migrations add Init
    dotnet ef database update
    ```
Siguiendo estos pasos, debería poder iniciar el proyecto, crear las tablas necesarias y añadir información inicial a la base de datos de manera efectiva.

### 6. Realizar test

1. Ingresar a la carpeta test:
```sh
cd test
```

1. Instalar las siguientes dependencias:

```sh
dotnet add package coverlet.msbuild
dotnet tool install --global dotnet-reportgenerator-globaltool
```
2. Generar test con coverage

```sh
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover
```

3. Informe HTML

```sh
reportgenerator "-reports:coverage.opencover.xml" "-targetdir:coverage_report" "-reporttypes:HTML"
```