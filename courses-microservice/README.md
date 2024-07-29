# Arquitectura DDD y Pruebas del Microservicio de Cursos

## 1. Descripcion
El microservicio de cursos es responsable de la gestión integral de los cursos ofrecidos por todas las escuelas, incluyendo la creación y administración de horarios. Facilita operaciones CRUD (Crear, Leer, Actualizar, Eliminar) para cursos, requisitos de cursos, y horarios.
- **Contexto Delimitado:** Gestión de Cursos y Horarios

## 2. Arquitectura DDD

### 2.1 Capas de la Arquitectura

<details>
<summary>2.1.1. `Application`</summary>

Esta capa contiene la lógica de la aplicación y maneja los casos de uso del sistema. Se organiza en carpetas específicas por entidad, como `Courses`, `CoursePrerequisites`, `Customers`, y `Schedules`, cada una conteniendo comandos, manejadores de comandos y consultas.

- **`CoursePrerequisites`:** 
  - `Common`: Respuestas comunes.
  - `Create`, `Delete`, `GetAll`: Comandos y manejadores de comandos para operaciones CRUD.
  
- **`Courses`:**
  - `Common`: Respuestas comunes.
  - `Create`, `Delete`, `GetAll`, `GetById`, `Update`: Comandos y manejadores de comandos para operaciones CRUD.

- **`Customers`:**
  - `Create`, `Delete`, `GetAll`, `GetById`, `Update`: Comandos y manejadores de comandos para operaciones CRUD.

- **`Schedules`:**
  - `Common`: Respuestas comunes.
  - `Create`, `GetAll`, `GetById`, `Update`, `GetScheduleByYearSemesterSchool`, `GetSchedulesByCourseId`, `UpdateAvailableSeats`: Comandos y manejadores de comandos para operaciones CRUD.

</details>

<details>
<summary>2.1.2. `Domain`</summary>

Esta capa contiene las entidades del dominio, objetos de valor, repositorios, servicios y eventos del dominio.

- **`Entities`:** Define las entidades principales como `Course`, `CoursePrerequisite`, `Customer`, y `Schedule`.
- **`ValueObjects`:** Define los objetos de valor como `Address`, `PhoneNumber`, `ScheduleDetails`, `ScheduleEntry`, y `Semester`.
- **`Repositories`:** Interfaces para repositorios.
- **`Services`:** Define servicios de dominio e interfaces.
- **`Primitives`:** Define elementos básicos como `AggregateRoot`, `DomainEvent`, e `IUnitOfWork`.
- **`DomainErrors`:** Define errores específicos del dominio.

</details>

<details>
<summary>2.1.3. `Infrastructure`</summary>

Esta capa implementa las interfaces definidas en la capa de dominio y maneja la comunicación con la base de datos y otros servicios externos.

- **`Persistence`:** Configuraciones y contexto de base de datos.
  - `ApplicationDbContext.cs`: Contexto de base de datos.
  - `Configuration`: Configuraciones para entidades.
  - `Migrations`: Migraciones de base de datos.
  - `Repositories`: Implementaciones de los repositorios.

</details>

<details>
<summary>2.1.4. `Web.API`</summary>

Esta capa expone la funcionalidad de la aplicación a través de controladores y configura la inyección de dependencias.

- **`Controllers`:** Define controladores API como `CourseController`, `CoursePrerequisiteController`, `CustomerController`, y `ScheduleController`.
- **`Common`:** Manejo de errores y configuración HTTP.
- **`Program.cs`:** Configuración principal de la aplicación.
- **`Properties`:** Configuraciones de entorno.

</details>

### 2.2 Diagrama de la Arquitectura

![](resources/CourseArquitecture.png)

## 3. Pruebas

### 3.1 Pruebas de API

#### 3.1.1. Herramientas y Tecnologías
Descripción de las herramientas utilizadas para las pruebas de API (por ejemplo, Postman, Swagger, etc.).

<details>
  <summary style="font-size: 1.3em; font-weight: bold;">3.1.2. Escenarios de Prueba de API</summary>


<details open>
  <summary><b><i>Escenario 1: </i></b> Creación de curso con horas inválidas.</summary>

 ```gherkin
Scenario: El endpoint "api/v1/courses" está disponible
Given el endpoint "api/payments/" requiere autenticación
When se envía una solicitud POST al endpoint con el siguiente cuerpo:
    {
        "name": "Anime II",
        "credits": 3,
        "hours": 0, // Valor inválido
        "active": true,
        "semester": "B",
        "schoolId": "321"
    }
Then la respuesta debe tener un código de estado 400
And el cuerpo de la respuesta debe contener un error para las horas inválidas
And el campo "title" debe tener el valor "One or more validation errors occurred."
  ```
</details>

<details open>
  <summary><b><i>Escenario 2: </i></b> Creación de curso con créditos inválidos.</summary>

 ```gherkin
Scenario: el endpoint "api/v1/courses" está disponible
Given el endpoint "api/v1/courses" está disponible
When se envía una solicitud POST al endpoint con el siguiente cuerpo:
    {
        "name": "Anime II",
        "credits": 0, // Valor inválido
        "hours": 3, 
        "active": true,
        "semester": "B",
        "schoolId": "321"
    }
Then la respuesta debe tener un código de estado 400
And la respuesta debe estar en formato JSON
And el cuerpo de la respuesta debe contener un error para los créditos inválidos
And el campo "title" debe tener el valor "One or more validation errors occurred."
  
  ```
</details>

<details open>
  <summary><b><i>Escenario 3: </i></b> Creación de curso con datos válidos.</summary>

 ```gherkin
Scenario: el endpoint "api/v1/courses" está disponible
Given el endpoint "api/v1/courses" está disponible
When se envía una solicitud POST al endpoint con el siguiente cuerpo:
    {
      "name": "Anime II",
      "credits": 4,
      "hours": 3,
      "active": true,
      "semester": "A",
      "schoolId": "123"
    }
Then la respuesta debe tener un código de estado 201
  And la respuesta debe estar en formato JSON
  And el cuerpo de la respuesta debe contener el campo "courseId"
  And el campo "name" debe tener el valor "Anime II"
  And el campo "credits" debe tener el valor 4
  And el campo "hours" debe tener el valor 3
  And el campo "active" debe tener el valor true
  And el campo "semester" debe tener el valor "A"
  And el campo "schoolId" debe tener el valor "123"
  
  ```
</details>


<details open>
  <summary><b><i>Escenario 4: </i></b> Creación de curso con datos válidos.</summary>

 ```gherkin
Scenario: el endpoint "api/v1/courses" está disponible
Given el endpoint "api/v1/courses" está disponible
When se envía una solicitud POST al endpoint con el siguiente cuerpo:
    {
      "name": "Anime II",
      "credits": 4,
      "hours": 3,
      "active": true,
      "semester": "A",
      "schoolId": "123"
    }
Then la respuesta debe tener un código de estado 201
  And la respuesta debe estar en formato JSON
  And el cuerpo de la respuesta debe contener el campo "courseId"
  And el campo "name" debe tener el valor "Anime II"
  And el campo "credits" debe tener el valor 4
  And el campo "hours" debe tener el valor 3
  And el campo "active" debe tener el valor true
  And el campo "semester" debe tener el valor "A"
  And el campo "schoolId" debe tener el valor "123"
  
  ```
</details>


<details open>
  <summary><b><i>Escenario 5: </i></b> Obtener curso por ID válido.</summary>

 ```gherkin
Given un curso válido ha sido creado y el ID del curso está almacenado en la variable de entorno "courseId"
When se envía una solicitud GET al endpoint "api/v1/courses/{{courseId}}"
 Then la respuesta debe tener un código de estado 200
  And la respuesta debe estar en formato JSON
  And el cuerpo de la respuesta debe contener el campo "courseId"
  ```
</details>


<details open>
  <summary><b><i>Escenario 6: </i></b> Obtener curso con ID no encontrado.</summary>

 ```gherkin
Given un ID de curso que no existe
  When se envía una solicitud GET al endpoint "api/v1/courses/..."
  Then la respuesta debe tener un código de estado 404
  And la respuesta debe estar en formato JSON
  And el cuerpo de la respuesta debe contener el campo "status" con el valor 404
  ```
</details>

<details open>
  <summary><b><i>Escenario 7: </i></b> Obtener curso con ID inválido.</summary>

 ```gherkin
Given un ID de curso inválido
  When se envía una solicitud GET al endpoint "api/v1/courses/374650ac-962a-4f73-982d-a"
  Then la respuesta debe tener un código de estado 400
  And la respuesta debe estar en formato JSON
  And el cuerpo de la respuesta debe contener el campo "status" con el valor 400
  ```
</details>

<details open>
  <summary><b><i>Escenario 8: </i></b> Obtener todos los cursos.</summary>

 ```gherkin
Given hay cursos disponibles en la base de datos
  When se envía una solicitud GET al endpoint "api/v1/courses"
  Then la respuesta debe tener un código de estado 200
  And la respuesta debe estar en formato JSON
  And la respuesta debe contener una lista de cursos
  And la lista de cursos no debe estar vacía
  And el primer curso en la lista debe contener los campos "courseId", "name", "credits", "hours", "active", "semester" y "schoolId"
  ```
</details>

<details open>
  <summary><b><i>Escenario 9: </i></b> Eliminación de curso válido.</summary>

 ```gherkin
Given un curso válido ha sido creado y el ID del curso está almacenado en la variable de entorno "courseId"
  When se envía una solicitud DELETE al endpoint "api/v1/courses/{{courseId}}"
  Then la respuesta debe tener un código de estado 204
  And el curso con el ID "courseId" ya no debe existir
  And se debe recibir una respuesta con un código de estado 404 cuando se intente obtener el curso eliminado
  ```
</details>

<details open>
  <summary><b><i>Escenario 10: </i></b> Eliminación de curso con ID no encontrado.</summary>

 ```gherkin
Given un ID de curso que no existe
  When se envía una solicitud DELETE al endpoint "api/v1/courses/{{courseId}}"
  Then la respuesta debe tener un código de estado 404
  And la respuesta debe estar en formato JSON
  And el cuerpo de la respuesta debe contener el campo "status" con el valor 404
  ```
</details>

<details open>
  <summary><b><i>Escenario 11: </i></b>  Actualización de curso válido.</summary>

 ```gherkin
Given un curso válido ha sido creado y el ID del curso está almacenado en la variable de entorno "courseId"
  When se envía una solicitud PUT al endpoint "api/v1/courses/{{courseId}}" con el cuerpo actualizado:
    {
      "id": "{{courseId}}",
      "name": "asd",
      "credits": 2,
      "hours": 3,
      "active": true,
      "semester": "A",
      "schoolId": "21"
    }
  Then la respuesta debe tener un código de estado 204
  ```
</details>

<details open>
  <summary><b><i>Escenario 12: </i></b>  Actualización de curso con ID no encontrado.</summary>

 ```gherkin
Given un ID de curso que no existe
  When se envía una solicitud PUT al endpoint "api/v1/courses/37302ffc-5432-4431-6eb2-d23d52b9a5bd" con el cuerpo:
    {
      "id": "37302ffc-5432-4431-6eb2-d23d52b9a5bd",
      "name": "asd",
      "credits": 2,
      "hours": 3,
      "active": true,
      "semester": "A",
      "schoolId": "1"
    }
  Then la respuesta debe tener un código de estado 404
  And la respuesta debe contener detalles del error
  And el cuerpo de la respuesta debe contener el campo "status" con el valor 404
  ```
</details>

<details open>
  <summary><b><i>Escenario 13: </i></b> Creación de un prerequisito de curso válido..</summary>

 ```gherkin
Given un cuerpo de solicitud válido para la creación de un prerequisito de curso
  When se envía una solicitud POST al endpoint "api/v1/course/prerequisite" con el cuerpo:
    """
    {
      "courseId": "08dcaf56-d0bd-4992-8f10-75243793bbef",
      "coursePrerequisiteId": "20603fe9-7949-4a4b-844d-d479692491ca"
    }
    """
  Then la respuesta debe tener un código de estado 201
  And la respuesta debe estar en formato JSON
  And la respuesta debe contener los campos "courseId" y "coursePrerequisiteId"
  ```
</details>

<details open>
  <summary><b><i>Escenario 14: </i></b> Obtener todos los prerequisitos de curso válidos.</summary>

 ```gherkin
Given un ID de curso válido "08dcaf56-d0bd-4992-8f10-75243793bbef"
  When se envía una solicitud GET al endpoint "api/v1/course/prerequisite/08dcaf56-d0bd-4992-8f10-75243793bbef"
  Then la respuesta debe tener un código de estado 200
  And la respuesta debe estar en formato JSON
  And la respuesta debe contener una lista de prerequisitos de curso esperados
  ```
</details>

<details open>
  <summary><b><i>Escenario 15: </i></b>  Crear un horario de curso.</summary>

 ```gherkin
 Given un curso con ID "{{courseId}}"
  When se envía una solicitud POST al endpoint "api/v1/schedules" con los siguientes datos:
    {
      "courseId": "{{courseId}}",
      "year": 2024,
      "schoolId": "123",
      "details": {
        "group": "A",
        "professorId": "prof-123"
      },
      "entries": [
        {
          "dayOfWeek": 1,
          "startTime": "10:00:00",
          "endTime": "11:00:00"
        },
        {
          "dayOfWeek": 4,
          "startTime": "17:00:00",
          "endTime": "21:00:00"
        }
      ]
    }
  Then la respuesta debe tener un código de estado 201
  And la respuesta debe estar en formato JSON
  And la respuesta debe contener un ID de horario
  And la respuesta debe contener el ID del curso "{{$courseId}}"
  And el año en la respuesta debe ser 2024
  And el ID de la escuela en la respuesta debe ser "123"
  And el grupo en los detalles debe ser "A"
  And el ID del profesor en los detalles debe ser "prof-123"
  And la respuesta debe contener 2 entradas de horario
  ```
</details>

<details open>
  <summary><b><i>Escenario 16: </i></b> Crear un horario con horas no válidas.</summary>

 ```gherkin
  Given un curso con ID "{{$courseId}}"
  When se envía una solicitud POST al endpoint "api/v1/schedules" con los siguientes datos:
    """
    {
      "courseId": "{{courseId}}",
      "year": 2024,
      "schoolId": "123",
      "details": {
        "group": "A",
        "professorId": "prof-123"
      },
      "entries": [
        {
          "dayOfWeek": 1,
          "startTime": "10:00:00",
          "endTime": "11:00:00"  // 1 hour
        },
        {
          "dayOfWeek": 4,
          "startTime": "17:00:00",
          "endTime": "18:00:00"  // 1 hour (Total: 2 hours, should be 5)
        }
      ]
    }
    """
  Then la respuesta debe tener un código de estado 400
  And la respuesta debe estar en formato JSON
  And la respuesta debe contener un error de validación por horas no válidas

  ```
</details>

<details open>
  <summary><b><i>Escenario 17: </i></b>  Crear un horario con un curso inexistente.</summary>

 ```gherkin
  Given un curso con ID "00000000-0000-0000-0000-000000000000" (inexistente)
  When se envía una solicitud POST al endpoint "api/v1/schedules" con los siguientes datos:
    {
      "courseId": "00000000-0000-0000-0000-000000000000",
      "year": 2024,
      "schoolId": "123",
      "details": {
        "group": "A",
        "professorId": "prof-123"
      },
      "entries": [
        {
          "dayOfWeek": 1,
          "startTime": "10:00:00",
          "endTime": "11:00:00"
        }
      ]
    }
  Then la respuesta debe tener un código de estado 404
  And la respuesta debe estar en formato JSON
  And la respuesta debe contener el mensaje de error "El curso con el ID proporcionado no fue encontrado"

  ```
</details>

<details open>
  <summary><b><i>Escenario 18: </i></b> Obtener todos los horarios.</summary>

 ```gherkin
  Given existen horarios en la base de datos
 When se envía una solicitud GET al endpoint "api/v1/schedules"
  Then la respuesta debe tener un código de estado 200
  And la respuesta debe estar en formato JSON
  And el cuerpo de la respuesta debe ser un array
  And cada item del horario en el array debe tener las siguientes propiedades:
    | Property       |
    | ---------------|
    | scheduleId     |
    | courseId       |
    | year           |
    | schoolId       |
    | details        |
    | details.group  |
    | details.professorId |
    | entries        |
  And cada item de entradas en el horario debe ser un array
  ```
</details>

<details open>
  <summary><b><i>Escenario 19: </i></b> Obtener todos los horarios.</summary>

 ```gherkin
  Given existen horarios en la base de datos
 When se envía una solicitud GET al endpoint "api/v1/schedules"
  Then la respuesta debe tener un código de estado 200
  And la respuesta debe estar en formato JSON
  And el cuerpo de la respuesta debe ser un array
  And cada item del horario en el array debe tener las siguientes propiedades:
    | Property       |
    | ---------------|
    | scheduleId     |
    | courseId       |
    | year           |
    | schoolId       |
    | details        |
    | details.group  |
    | details.professorId |
    | entries        |
  And cada item de entradas en el horario debe ser un array
  ```
</details>

<details open>
  <summary><b><i>Escenario 20: </i></b> Obtener un horario por ID.</summary>

 ```gherkin
Given un horario creado con los siguientes datos:
    {
      "courseId": "20603fe9-7949-4a4b-844d-d479692491ca",
      "year": 2024,
      "schoolId": "123",
      "details": {
        "group": "A",
        "professorId": "prof-123"
      },
      "entries": [
        {
          "dayOfWeek": 1,
          "startTime": "10:00:00",
          "endTime": "11:00:00"
        },
        {
          "dayOfWeek": 4,
          "startTime": "17:00:00",
          "endTime": "19:00:00"
        }
      ]
    }
  When se envía una solicitud GET al endpoint "api/v1/schedules/{scheduleId}" donde {scheduleId} es el ID del horario creado
  Then la respuesta debe tener un código de estado 200
  And la respuesta debe estar en formato JSON
  And el cuerpo de la respuesta debe contener los datos del horario con las siguientes propiedades:
    {
      "scheduleId": "scheduleId",
      "courseId": "20603fe9-7949-4a4b-844d-d479692491ca",
      "year": 2024,
      "schoolId": "123",
      "details": {
        "group": "A",
        "professorId": "prof-123"
      },
      "entries": [
        {
          "dayOfWeek": 1,
          "startTime": "10:00:00",
          "endTime": "11:00:00"
        },
        {
          "dayOfWeek": 4,
          "startTime": "17:00:00",
          "endTime": "19:00:00"
        }
      ]
    }
  ```
</details>


<details open>
  <summary><b><i>Escenario 21: </i></b> Obtener un horario con un ID no existente.</summary>

 ```gherkin
  Given el endpoint   "api/v1/schedules/{{scheduleId}} esta activo
  When se envía una solicitud GET al endpoint "api/v1/schedules/00624d76-0203-4697-8c2a-d080fac214c9"
  Then la respuesta debe tener un código de estado 404
  And la respuesta debe estar en formato JSON
  And el cuerpo de la respuesta debe contener los detalles del error con el estado 404
  ```
</details>

<details open>
  <summary><b><i>Escenario 22: </i></b> Actualizar un horario existente.</summary>

 ```gherkin
  Given un horario creado con los siguientes datos:
    {
      "courseId": "20603fe9-7949-4a4b-844d-d479692491ca",
      "year": 2024,
      "schoolId": "123",
      "details": {
        "group": "A",
        "professorId": "prof-123"
      },
      "entries": [
        {
          "dayOfWeek": 1,
          "startTime": "10:00:00",
          "endTime": "11:00:00"
        },
        {
          "dayOfWeek": 4,
          "startTime": "17:00:00",
          "endTime": "19:00:00"
        }
      ]
    }
  When se envía una solicitud PUT al endpoint "api/v1/schedules/{scheduleId}" con el siguiente cuerpo:
    {
      "scheduleId": "{scheduleId}",
      "courseId": "20603fe9-7949-4a4b-844d-d479692491ca",
      "year": 2024,
      "schoolId": "123",
      "details": {
        "group": "B",
        "professorId": "prof-124"
      },
      "entries": [
        {
          "dayOfWeek": 2,
          "startTime": "09:00:00",
          "endTime": "10:00:00"
        },
        {
          "dayOfWeek": 5,
          "startTime": "14:00:00",
          "endTime": "16:00:00"
        }
      ]
    }
  Then la respuesta debe tener un código de estado 204
  ```
</details>

<details open>
  <summary><b><i>Escenario 23: </i></b> Eliminar un horario.</summary>

 ```gherkin
  Given un horario creado con los siguientes datos:
    """
    {
      "courseId": "20603fe9-7949-4a4b-844d-d479692491ca",
      "year": 2024,
      "schoolId": "123",
      "details": {
        "group": "A",
        "professorId": "prof-123"
      },
      "entries": [
        {
          "dayOfWeek": 1,
          "startTime": "10:00:00",
          "endTime": "11:00:00"
        },
        {
          "dayOfWeek": 4,
          "startTime": "17:00:00",
          "endTime": "19:00:00"
        }
      ]
    }
    """
  When se envía una solicitud DELETE al endpoint "api/v1/schedules/{scheduleId}"
  Then la respuesta debe tener un código de estado 204
  And el horario debe haber sido eliminado
    """
    Cuando se realiza una solicitud GET al endpoint "api/v1/schedules/{scheduleId}"
    Entonces la respuesta debe tener un código de estado 404
    """
  ```
</details>


<details open>
  <summary><b><i>Escenario 24: </i></b> Intentar eliminar un horario que no existe.</summary>

 ```gherkin
  Given un horario no existe en la base de datos
  When se envía una solicitud DELETE al endpoint "api/v1/schedules/{scheduleId}"
  Then la respuesta debe tener un código de estado 404 la respuesta debe tener un código de estado 404
  ```
</details>

<details open>
  <summary><b><i>Escenario 25: </i></b> Obtener horarios para un CourseId válido.</summary>

 ```gherkin
  Given existen horarios con el CourseId "07f3f5cf-09d2-4d57-bf3c-a50ca427a2f6"
  When se envía una solicitud GET al endpoint "api/v1/schedules/course/07f3f5cf-09d2-4d57-bf3c-a50ca427a2f6"
  Then la respuesta debe tener un código de estado 200
  And la respuesta debe estar en formato JSON
  And la respuesta debe contener datos de horarios
    La respuesta debe ser una lista no vacía
    Cada objeto en la lista debe tener las propiedades:
      - scheduleId
      - courseId
      - schoolId
      - year
      - details (objeto con propiedades: group, professorId)
      - entries (lista de objetos con propiedades: day, startTime, endTime)
      - course (objeto con propiedades: name, credits, hours, active, semester, schoolId)
    Cada entrada debe tener valores válidos para day (número), startTime (HH:mm:ss), y endTime (HH:mm:ss)
  ```
</details>

<details open>
  <summary><b><i>Escenario 26: </i></b> Intentar obtener horarios para un CourseId que no existe.</summary>

 ```gherkin
  Given el curso no existe en la base de datos o no hay ningun horario asociado al curso
  When se envía una solicitud GET al endpoint "api/v1/schedules/course/invalid-course-id"
  Then la respuesta debe tener un código de estado 404
  ```
</details>

</details>