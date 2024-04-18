# UNSA System 

By: Dexo Corp

Members:
- Mogollon Caceres Sergio Daniel
- Davis Coropuna Leon Felipe
- Apaza Apaza Nelzon Jorge
- Lupo Condori Avelino
- Maldonado Casilla Braulio Nayap
- Parizaca Mozo Paul Antony
- Huaman Coaquira Luciana Julissa



# Create ASP Project With NUnit Test

## Project Setup

### **1. Create Solution**

```bash
mkdir users-microservice && cd users-microservice 
dotnet new sln
```

### **2. Create Source Project**
```bash
mkdir src && cd src && dotnet new web
```

### **3. Create Test Project**
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


### **4. Link Subprojects to Base Project**
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

### **5. Run App**
```bash
dotnet run --project src/src.csproj
```

### **6. Run Test**
```bash
dotnet test
```
