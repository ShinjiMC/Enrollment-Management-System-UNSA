## Configuración del Proyecto

#### Inicializar el Proyecto

1. **Crear una nueva solución**:
    ```bash
    dotnet new sln -o courses-microservice
    cd courses-microservice
    ```

2. **Crear proyectos**:

    - **Crear librerías de clases**:
      ```bash
      dotnet new classlib -o src/Domain
      dotnet new classlib -o src/Application
      dotnet new classlib -o src/Infrastructure
      ```

    - **Crear API Web**:
      ```bash
      dotnet new webapi -o src/Web.API
      ```

3. **Construir la solución**:
    ```bash
    dotnet build
    ```

4. **Agregar referencias entre proyectos**:
    ```bash
    dotnet add src/Application/Application.csproj reference src/Domain/Domain.csproj
    dotnet add src/Infrastructure/Infrastructure.csproj reference src/Application/Application.csproj
    dotnet add src/Web.API/Web.API.csproj reference src/Application/Application.csproj src/Infrastructure/Infrastructure.csproj
    ```

5. **Agregar proyectos a la solución**:
    ```bash
    dotnet sln add src/Web.API/Web.API.csproj
    dotnet sln add src/Application/Application.csproj
    dotnet sln add src/Infrastructure/Infrastructure.csproj
    dotnet sln add src/Domain/Domain.csproj
    ```

6. **Crear carpeta para pruebas**:

    - **Crear carpeta `test` y mover el proyecto de pruebas**:
      ```bash
      mkdir test
      ```

7. **Construir y ejecutar la API**:
    ```bash
    cd src
    dotnet build
    dotnet run -p Web.API/
    ```

8. **Agregar migraciones de Entity Framework**:
    ```bash
    dotnet ef migrations add InitialMigration -p src/Infrastructure -s src/Web.API -o Persistence/Migrations --verbose
    dotnet ef database update -p src/Infrastructure -s src/Web.API/
    ```
9. **Reporte Owasp**
    ```bash
    docker run --network host -v $(pwd)/zap-wrk:/zap/wrk -v $(pwd)/zap-reports:/zap/reports -t zaproxy/zap-stable zap-baseline.py -t http://localhost:8003 -r SecurityTestReport.html > SecurityTestReport.txt

    mv zap-wrk/SecurityTestReport.html TestReports/SecurityTestReport/SecurityTestReport.html
    mv SecurityTestReport.txt TestReports/SecurityTestReport/SecurityTestReport.txt

    # Delete the directories
    rm -rf zap-wrk
    rm -rf zap-reports

    ```