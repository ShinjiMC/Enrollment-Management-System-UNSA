# Enrollment Management System UNSA (By Dexo Corp)

By: Dexo Corp

Members:
- Mogollon Caceres Sergio Daniel
- Davis Coropuna Leon Felipe
- Apaza Apaza Nelzon Jorge
- Lupo Condori Avelino
- Maldonado Casilla Braulio Nayap
- Parizaca Mozo Paul Antony
- Huaman Coaquira Luciana Julissa

## Create ASP Project With NUnit Test

### Project Setup

#### **1. Create Solution**

```bash
mkdir users-microservice && cd users-microservice 
dotnet new sln
```

#### **2. Create Source Project**
```bash
mkdir src && cd src && dotnet new web
```

#### **3. Create Test Project**
```bash
mkdir test && cd test && dotnet new nunit
```
- Install Moq Dependency `(Mocks)`:
    ```bash
    dotnet add package Moq 
    ```
- Link Test Project with Source Project 
    ```bash
    dotnet add reference ../src/src.csproj
    ```


#### **4. Link Subprojects to Base Project**
- Go to the root of the `base project`
- Link Source Project with Base Project    
    ```bash
    dotnet sln add src/src.csproj
    ```
- Link Test Project with Base Project
    ```bash
    cd ..
    dotnet sln add test/test.csproj
    ```

#### **5. Install Dependencies in /src**

```bash
cd src/
```

- For EntityFramework Core CLI:
    ```bash
    dotnet tool install --global dotnet-ef
    ```

- For EntityFrameworkCore.Design for migrations:
    ```bash
    dotnet add package Microsoft.EntityFrameworkCore.Design --version 9.0.0-preview.1.24081.2
    ```

- For Pomelo.EntityFrameworkCore.MySql dependency:
    ```bash
    dotnet add package Pomelo.EntityFrameworkCore.MySql --version 9.0.0-preview.1
    ```

#### **6. Run App**
```bash
dotnet run --project src/src.csproj
```

#### **7. Run Test**
```bash
dotnet test
```

#### **8. Build App**
```bash
dotnet build src/src.csproj --configuration Release
```

#### **9. Coverage**
```bash
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover /p:CoverletOutput=../../coverage # no func
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover --output coverage # si func pero warning
```
