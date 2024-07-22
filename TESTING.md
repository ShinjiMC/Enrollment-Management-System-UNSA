# Plan de Pruebas - Sistema de Gestión de Matrícula

Este documento describe el plan de pruebas para el Sistema de Gestión de Matrícula, cubriendo pruebas de API, pruebas unitarias, pruebas de rendimiento y pruebas de seguridad. Asimismo se detalla los pasos instalacion, configuracion y ejecucion para cada tipo de prueba

## Pruebas de API

### Escenarios de Prueba

#### ***Escenario 1: Creación de un Nuevo Usuario***

```gherkin
Feature: Creación de usuarios en el sistema

  Scenario: Crear un nuevo usuario válido
    Given que el usuario tiene acceso al sistema
    And tiene permisos de Administrador
    When el usuario envía una solicitud POST a /api/users con los siguientes datos:
      | Nombre    | Email                | Rol      |
      | Juan Pérez | juan@example.com     | Estudiante |
    Then la respuesta debe ser 201 Created
    And el usuario con permisos de Administrador debe poder ver el nuevo usuario en la lista de estudiantes
```

#### ***Escenario 2: Actualización de un Curso***

```gherkin
Feature: Actualización de cursos en el sistema

  Scenario: Actualizar un curso existente
    Given que el usuario tiene permisos de administrador
    And que existe un curso con el ID 1234 en el sistema
    When el usuario envía una solicitud PUT a /api/courses/1234 con los siguientes datos:
      | Nombre      | Descripción          |
      | Matemáticas | Curso avanzado de matemáticas |
    Then la respuesta debe ser 200 OK
    And los detalles del curso deben reflejar los cambios realizados
```

## Pruebas de Rendimiento

### Escenarios de Prueba

#### ***Escenario 1: Carga de Usuarios Concurrentes***

```gherkin
Feature: Rendimiento del sistema bajo carga

  Scenario: Carga de 1000 usuarios concurrentes
    Given que el sistema está en un estado estable
    When 1000 usuarios concurrentes realizan solicitudes GET a /api/users
    Then todas las solicitudes deben completarse dentro de 2 segundos en promedio
    And el sistema debe mantener un uso de CPU inferior al 80% durante la prueba
```

#### ***Escenario 2: Evaluación de Tiempo de Respuesta***

```gherkin
Feature: Evaluación del tiempo de respuesta del sistema

  Scenario: Medición del tiempo de respuesta de una solicitud crítica
    Given que el sistema está en un estado normal
    When el usuario realiza una solicitud GET a /api/courses/5678
    Then la respuesta debe ser devuelta en menos de 500 milisegundos
```

## Pruebas de Seguridad

### Escenarios de Prueba

#### ***Escenario 1: Escaneo de Vulnerabilidades con OWASP ZAP***

```gherkin
Feature: Escaneo de vulnerabilidades de seguridad

  Scenario: Escaneo de vulnerabilidades con OWASP ZAP
    Given que el sistema está desplegado y accesible
    When se ejecuta un escaneo de vulnerabilidades usando OWASP ZAP
    Then el escaneo debe identificar y reportar todas las vulnerabilidades de alto riesgo
    And el equipo de seguridad debe analizar y mitigar las vulnerabilidades encontradas
```

#### ***Escenario 2: Autenticación Segura con JWT***

```gherkin
Feature: Autenticación segura utilizando JWT

  Scenario: Inicio de sesión y generación de token JWT
    Given que el usuario tiene credenciales válidas
    When el usuario realiza una solicitud POST a /api/auth/login con sus credenciales
    Then la respuesta debe ser 200 OK
    And un token JWT válido debe ser generado y devuelto en la respuesta
```


# Instalación, Configuración y Ejecución de Pruebas

## Pruebas Unitarias (NUnit + Moq en .NET)

### _1. Instalación_

- Instala .NET SDK 8.

- Crea un proyecto de pruebas unitarias:
    ```bash
    dotnet new nunit -n MyUnitTests
    cd MyUnitTests
    ```

- Agrega Moq al proyecto:
    ```bash
    dotnet add package Moq
    ```

### 2. Configuración

Ejemplo de una prueba unitaria con NUnit y Moq (`MyUnitTests/UnitTest1.cs`):
```csharp
using NUnit.Framework;
using Moq;

namespace MyUnitTests
{
    public interface ICalculator
    {
        int Add(int a, int b);
    }

    public class Calculator : ICalculator
    {
        public int Add(int a, int b) => a + b;
    }

    public class Tests
    {
        private Mock<ICalculator> _calculatorMock;

        [SetUp]
        public void Setup()
        {
            _calculatorMock = new Mock<ICalculator>();
        }

        [Test]
        public void TestAddition()
        {
            _calculatorMock.Setup(c => c.Add(It.IsAny<int>(), It.IsAny<int>())).Returns(5);
            Assert.AreEqual(5, _calculatorMock.Object.Add(2, 3));
        }
    }
}
```

### 3. Ejecución

Para ejecutar las pruebas unitarias:
```bash
dotnet test
```

## Pruebas de API (Postman CLI)

### 1. Instalación

- Instala Node.js y npm:
    ```bash
    sudo apt install nodejs npm
    ```

- Instala Newman (CLI de Postman):
    ```bash
    npm install -g newman
    ```

### 2. Configuración

Exporta tu colección de Postman a un archivo JSON (`TestsCollection.json`).

### 3. Ejecución

Para ejecutar las pruebas de la colección de Postman:
```bash
newman run TestsCollection.json
```

## Pruebas de Rendimiento (JMeter)

### 1. Instalación

- Instala Java 17 (LTS).

- Descarga y extrae JMeter:
    ```bash
    wget https://downloads.apache.org//jmeter/binaries/apache-jmeter-5.4.3.tgz
    tar -xvzf apache-jmeter-5.4.3.tgz
    cd apache-jmeter-5.4.3
    ```

### 2. Configuración

Crea un plan de pruebas en el GUI de JMeter y guarda el archivo (`performance-test-plan.jmx`).

### 3. Ejecución

Para ejecutar el plan de pruebas:
```bash
./bin/jmeter -n -t performance-test-plan.jmx -l results.jtl
```

## Pruebas de Seguridad (OWASP ZAP)

### 1. Instalación

- Instala OWASP ZAP:
    ```bash
    sudo apt install zaproxy
    ```

### 2. Configuración

Configura OWASP ZAP a través de su GUI para definir los escaneos que deseas realizar y guarda la configuración en un archivo.

### 3. Ejecución

Para ejecutar un escaneo usando la línea de comandos:
```bash
zap.sh -cmd -quickurl http://localhost:8001 -quickout report.html
```


