# Arquitectura DDD y Pruebas del Microservicio de Cursos

## 1. Descripcion
El microservicio de cursos es responsable de la gestión integral de los cursos ofrecidos por todas las escuelas, incluyendo la creación y administración de horarios. Facilita operaciones CRUD (Crear, Leer, Actualizar, Eliminar) para cursos, requisitos de cursos, y horarios.

## 2. Arquitectura DDD
### 2.1 Capas de la Arquitectura
#### 2.1.1. `Application`
Esta capa contiene la lógica de la aplicación y maneja los casos de uso del sistema. Se organiza en carpetas específicas por entidad, como `Courses`, `CoursePrerequisites`, `Customers`, y `Schedules`, cada una conteniendo comandos, manejadores de comandos y consultas.

- `CoursePrerequisites`: 
  - `Common`: Respuestas comunes.
  - `Create`, `Delete`, `GetAll`: Comandos y manejadores de comandos para operaciones CRUD.
  
- `Courses`:
  - `Common`: Respuestas comunes.
  - `Create`, `Delete`, `GetAll`, `GetById`, `Update`: Comandos y manejadores de comandos para operaciones CRUD.

- `Customers`:
  - `Create`, `Delete`, `GetAll`, `GetById`, `Update`: Comandos y manejadores de comandos para operaciones CRUD.

- `Schedules`:
  - `Common`: Respuestas comunes.
  - `Create`, `GetAll`, `GetById`, `Update`, `GetScheduleByYearSemesterSchool`, `GetSchedulesByCourseId`, `UpdateAvailableSeats`: Comandos y manejadores de comandos para operaciones CRUD.

#### 2.1.2. `Domain`
Esta capa contiene las entidades del dominio, objetos de valor, repositorios, servicios y eventos del dominio.

- `Entities`: Define las entidades principales como `Course`, `CoursePrerequisite`, `Customer`, y `Schedule`.
- `ValueObjects`: Define los objetos de valor como `Address`, `PhoneNumber`, `ScheduleDetails`, `ScheduleEntry`, y `Semester`.
- `Repositories`: Interfaces para repositorios.
- `Services`: Define servicios de dominio e interfaces.
- `Primitives`: Define elementos básicos como `AggregateRoot`, `DomainEvent`, e `IUnitOfWork`.
- `DomainErrors`: Define errores específicos del dominio.

#### 2.1.3. `Infrastructure`
Esta capa implementa las interfaces definidas en la capa de dominio y maneja la comunicación con la base de datos y otros servicios externos.

- `Persistence`: Configuraciones y contexto de base de datos.
  - `ApplicationDbContext.cs`: Contexto de base de datos.
  - `Configuration`: Configuraciones para entidades.
  - `Migrations`: Migraciones de base de datos.
  - `Repositories`: Implementaciones de los repositorios.

#### 2.1.4. `Web.API`
Esta capa expone la funcionalidad de la aplicación a través de controladores y configura la inyección de dependencias.

- `Controllers`: Define controladores API como `CourseController`, `CoursePrerequisiteController`, `CustomerController`, y `ScheduleController`.
- `Common`: Manejo de errores y configuración HTTP.
- `Program.cs`: Configuración principal de la aplicación.
- `Properties`: Configuraciones de entorno.
### 2.2 Diagrama de la Arquitectura

![](resources/CourseArquitecture.png)

## Pruebas

### 3.1 Pruebas de API

#### 3.1.1. Herramientas y Tecnologías
Descripción de las herramientas utilizadas para las pruebas de API (por ejemplo, Postman, Swagger, etc.).