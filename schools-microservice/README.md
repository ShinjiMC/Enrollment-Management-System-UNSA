# Arquitectura DDD y Pruebas del Microservicio de Gestión de Escuelas

## 1. Descripción

El microservicio de Gestión de Escuelas se encarga de gestionar todos los aspectos relacionados con las cursos, departamentos, plan de estudios, docentes, todo ello relacionado a las escuelas, incluyendo la creación, actualización, consulta y eliminación de todo lo mencionado.

- **Contexto Delimitado:** Gestión de Escuelas

## 2. Arquitectura DDD

### 2.1. Capas de la Arquitectura

<details open>
  <summary><b><i>2.1.1. Capa de Presentación</b></i></summary>
  <ul>
    <li>Controladores</li>
        <ul>
        <li><b>CourseController</b>: Gestiona las operaciones CRUD (Crear, Leer, Actualizar, Eliminar) para los cursos del microservicio. Utiliza la interfaz `ICourseService` para delegar las operaciones de negocio y expone los endpoints HTTP para obtener todos los cursos, obtener un curso por su ID, agregar un nuevo curso, actualizar un curso existente y eliminar un curso. Cada método HTTP está decorado con los atributos correspondientes (`[HttpGet]`, `[HttpPost]`, `[HttpPut]`, `[HttpDelete]`) y maneja las respuestas HTTP apropiadas, incluyendo códigos de estado y mensajes de error cuando es necesario.</li>
        </ul>
        <ul>
        <li><b>DepartmentController</b>: Maneja operaciones CRUD (Crear, Leer, Actualizar, Eliminar) para departamentos, utilizando `IDepartmentService`. Proporciona endpoints para obtener todos los departamentos, obtener un departamento por ID, agregar, actualizar y eliminar departamentos, con respuestas HTTP adecuadas para cada operación.</li>
        </ul>
        <ul>
        <li><b>SchoolController</b>: Gestiona operaciones CRUD (Crear, Leer, Actualizar, Eliminar) para escuelas, utilizando `ISchoolService`. Expone endpoints para obtener todas las escuelas, obtener una escuela por ID, agregar, actualizar y eliminar escuelas, y obtener el nombre de una escuela por ID. Los métodos responden con los códigos de estado HTTP adecuados.</li>
        </ul>
        <ul>
        <li><b>StudyPlanSchoolController</b>: Maneja operaciones relacionadas con los planes de estudio de las escuelas, utilizando `IStudyPlanSchoolService`. Proporciona endpoints para agregar un nuevo plan de estudio (`POST`), obtener un plan de estudio por nombre de la escuela (`GET`) y obtener un plan de estudio por ID de la escuela (`GET`), devolviendo las respuestas HTTP adecuadas para cada operación.</li>
        </ul>
        <ul>
        <li><b>TeacherController</b>: Gestiona los datos de los profesores, proporcionando endpoints para obtener todos los profesores, obtener un profesor por ID, agregar un nuevo profesor, actualizar un profesor existente y eliminar un profesor. Utiliza un servicio `ITeacherService` para realizar las operaciones de negocio y responde con el estado adecuado y los datos necesarios para cada acción.</li>
        </ul>
  </ul>
</details>
<details open>
  <summary><b><i>2.1.2. Capa de Aplicación</b></i></summary>
  <ul>
    <li>Servicios de aplicación</li>
        <ul>
        <li><b>CourseService</b>: Implementa la interfaz `ICourseService` y proporciona métodos para gestionar los cursos en un microservicio escolar. Utiliza un repositorio `ICourseRepository` para realizar las operaciones de acceso a datos. Los métodos incluidos permiten obtener todos los cursos, obtener un curso por ID, agregar un nuevo curso, actualizar un curso existente y eliminar un curso, delegando cada operación al repositorio correspondiente.</li>
        </ul>
        <ul>
        <li><b>DepartmentService</b>: Implementa la interfaz `IDepartmentService` y maneja la lógica de negocio relacionada con los departamentos en un microservicio escolar. Utiliza un repositorio `IDepartmentRepository` para acceder y manipular los datos de los departamentos. Ofrece métodos para obtener todos los departamentos, obtener un departamento por ID, agregar un nuevo departamento, actualizar un departamento existente y eliminar un departamento, delegando cada tarea al repositorio correspondiente.</li>
        </ul>
        <ul>
        <li><b>SchoolService</b>: Implementa `ISchoolService` y gestiona la lógica de negocio para los datos de las escuelas. Utiliza `ISchoolRepository` para acceder a los datos, ofreciendo métodos para obtener todas las escuelas, obtener una escuela por ID, obtener el nombre de una escuela por ID, agregar, actualizar y eliminar una escuela.</li>
        </ul>
        <ul>
        <li><b>StudyPlanSchoolService</b>: Implementa `IStudyPlanSchoolService` y maneja la lógica de negocio para los planes de estudio asociados a escuelas. Utiliza `IStudyPlanSchoolRepository` para acceder a los datos, ofreciendo métodos para obtener un plan de estudio por nombre de escuela o ID, y para agregar un nuevo plan de estudio.</li>
        </ul>
        <ul>
        <li><b>TeacherService</b>: Implementa `ITeacherService` y gestiona la lógica de negocio para los datos de los profesores. Utiliza `ITeacherRepository` para acceder a los datos, proporcionando métodos para obtener todos los profesores, obtener un profesor por ID, agregar, actualizar y eliminar profesores.</li>
        </ul>
  </ul>
</details>
<details open>
  <summary><b><i>2.1.3. Capa de Dominio</b></i></summary>
  <ul>
    <li>Modelos</li>
    <ul>
        <li><b>Course</b>: Representa un curso en el sistema, incluyendo propiedades como <code>Id</code>, <code>Name</code>, y <code>Credits</code>.</li>
        <li><b>Department</b>: Representa un departamento en el sistema, incluyendo propiedades como <code>Id</code> y <code>Name</code>.</li>
        <li><b>School</b>: Representa una escuela en el sistema, incluyendo propiedades como <code>Id</code>, <code>Name</code>, y <code>Location</code>.</li>
        <li><b>StudyPlanSchool</b>: Representa un plan de estudio asociado a una escuela, incluyendo propiedades como <code>Id</code>, <code>Name</code>, y <code>CoursesOfStudyPlan</code>, que es una lista de <code>CourseOfStudyPlan</code>.</li>
        <li><b>CourseOfStudyPlan</b>: Representa un curso dentro de un plan de estudio, incluyendo propiedades como <code>IdCourse</code> y <code>PrerequisitesCourse</code>, que es una lista de identificadores de cursos previos.</li>
        <li><b>Teacher</b>: Representa un profesor en el sistema, incluyendo propiedades como <code>Id</code>, <code>Name</code>, y <code>Department</code>.</li>
    </ul>
    <li>Interfaces de repositorio</li>
    <ul>
        <li><b>ICourseRepository</b>: Define las operaciones de acceso a datos para los cursos, incluyendo métodos para obtener todos los cursos (<code>GetAllCourses</code>), obtener un curso por ID (<code>GetCourseById</code>), agregar un curso (<code>AddCourse</code>), actualizar un curso (<code>UpdateCourse</code>), y eliminar un curso (<code>DeleteCourse</code>).</li>
        <li><b>IDepartmentRepository</b>: Define las operaciones de acceso a datos para los departamentos, incluyendo métodos para obtener todos los departamentos (<code>GetAllDepartments</code>), obtener un departamento por ID (<code>GetDepartmentById</code>), agregar un departamento (<code>AddDepartment</code>), actualizar un departamento (<code>UpdateDepartment</code>), y eliminar un departamento (<code>DeleteDepartment</code>).</li>
        <li><b>ISchoolRepository</b>: Define las operaciones de acceso a datos para las escuelas, incluyendo métodos para obtener todas las escuelas (<code>GetAllSchools</code>), obtener una escuela por ID (<code>GetSchoolById</code>), obtener el nombre de una escuela por ID (<code>GetSchoolNameById</code>), agregar una escuela (<code>AddSchool</code>), actualizar una escuela (<code>UpdateSchool</code>), y eliminar una escuela (<code>DeleteSchool</code>).</li>
        <li><b>IStudyPlanSchoolRepository</b>: Define las operaciones de acceso a datos para los planes de estudio de las escuelas, incluyendo métodos para obtener un plan de estudio por nombre de escuela (<code>GetStudyPlanBySchoolName</code>), obtener un plan de estudio por ID de escuela (<code>GetStudyPlanBySchoolId</code>), y agregar un plan de estudio (<code>AddStudyPlanSchool</code>).</li>
        <li><b>ITeacherRepository</b>: Define las operaciones de acceso a datos para los profesores, incluyendo métodos para obtener todos los profesores (<code>GetAllTeachers</code>), obtener un profesor por ID (<code>GetTeacherById</code>), agregar un profesor (<code>AddTeacher</code>), actualizar un profesor (<code>UpdateTeacher</code>), y eliminar un profesor (<code>DeleteTeacher</code>).</li>
    </ul>
  </ul>
</details>
<details open>
  <summary><b><i>2.1.4. Capa de Repositorio</b></i></summary>
  <ul>
    <li>Implementaciones de repositorios</li>
    <ul>
        <li><b>CourseRepository</b>: Implementa <code>ICourseRepository</code> y gestiona el acceso a datos para los cursos utilizando MongoDB. Proporciona métodos para obtener todos los cursos (<code>GetAllCourses</code>), obtener un curso por ID (<code>GetCourseById</code>), agregar un curso (<code>AddCourse</code>), actualizar un curso (<code>UpdateCourse</code>), y eliminar un curso (<code>DeleteCourse</code>).</li>
        <li><b>DepartmentRepository</b>: Implementa <code>IDepartmentRepository</code> y gestiona el acceso a datos para los departamentos utilizando MongoDB. Ofrece métodos para obtener todos los departamentos (<code>GetAllDepartments</code>), obtener un departamento por ID (<code>GetDepartmentById</code>), agregar un departamento (<code>AddDepartment</code>), actualizar un departamento (<code>UpdateDepartment</code>), y eliminar un departamento (<code>DeleteDepartment</code>).</li>
        <li><b>SchoolRepository</b>: Implementa <code>ISchoolRepository</code> y gestiona el acceso a datos para las escuelas utilizando MongoDB. Proporciona métodos para obtener todas las escuelas (<code>GetAllSchools</code>), obtener una escuela por ID (<code>GetSchoolById</code>), obtener el nombre de una escuela por ID (<code>GetSchoolNameById</code>), agregar una escuela (<code>AddSchool</code>), actualizar una escuela (<code>UpdateSchool</code>), y eliminar una escuela (<code>DeleteSchool</code>).</li>
        <li><b>StudyPlanSchoolRepository</b>: Implementa <code>IStudyPlanSchoolRepository</code> y gestiona el acceso a datos para los planes de estudio de las escuelas utilizando MongoDB. Ofrece métodos para obtener un plan de estudio por nombre de escuela (<code>GetStudyPlanBySchoolName</code>), obtener un plan de estudio por ID de escuela (<code>GetStudyPlanBySchoolId</code>), y agregar un plan de estudio (<code>AddStudyPlanSchool</code>).</li>
        <li><b>TeacherRepository</b>: Implementa <code>ITeacherRepository</code> y gestiona el acceso a datos para los profesores utilizando MongoDB. Proporciona métodos para obtener todos los profesores (<code>GetAllTeachers</code>), obtener un profesor por ID (<code>GetTeacherById</code>), agregar un profesor (<code>AddTeacher</code>), actualizar un profesor (<code>UpdateTeacher</code>), y eliminar un profesor (<code>DeleteTeacher</code>).</li>
    </ul>
    <li>Contexto de la base de datos</li>
    <ul>
      <li><b>MongoDbContext</b>: Configura la conexión a MongoDB y proporciona colecciones para diferentes entidades del sistema, incluyendo <code>Courses</code>, <code>Departments</code>, <code>Schools</code>, <code>Teachers</code>, y <code>StudyPlanSchools</code>. Utiliza una cadena de conexión y un nombre de base de datos definidos en la configuración.</li>
    </ul>
  </ul>
</details>

### 2.2. Diagrama de la Arquitectura

<p align="center">
  <img src="./src/Resources/School_Microservice.png" alt="Performance Test" />
</p>

## 3. Pruebas

### 3.1. Pruebas de API

#### 3.1.1. Herramientas y Tecnologías

Para llevar a cabo las pruebas de nuestra API, hemos utilizado una serie de herramientas y tecnologías clave que facilitan la evaluación exhaustiva y efectiva de las funcionalidades expuestas por la API. A continuación se describen las herramientas principales empleadas:

- **Postman:** Esta herramienta es ampliamente utilizada para el diseño, prueba y documentación de APIs. Permite realizar solicitudes HTTP a los endpoints de la API, definir y gestionar colecciones de pruebas, y verificar respuestas con gran detalle. Postman también proporciona funcionalidades avanzadas como la ejecución de scripts pre y post solicitud, así como la integración con sistemas de CI/CD para automatizar las pruebas.

- **Swagger:** Swagger, ahora conocido como OpenAPI, es una herramienta poderosa para documentar y probar APIs. Permite generar documentación interactiva que facilita la comprensión y el uso de los endpoints de la API. Con Swagger, es posible visualizar y probar los endpoints directamente desde la documentación, lo que ayuda a identificar problemas y validar el comportamiento de la API de manera eficiente.

Estas herramientas y tecnologías no solo facilitan la creación de pruebas detalladas y la generación de documentación precisa, sino que también aseguran una integración continua y una experiencia de usuario consistente durante el ciclo de vida de desarrollo de la API.

#### 3.1.2. Escenarios de Prueba de API

En esta sección se detallan los escenarios de prueba para el servicio de gestión de escuelas, los cuales han sido diseñados para validar el correcto funcionamiento de las diferentes operaciones del API.


### 3.3. Pruebas de Seguridad

#### 3.3.1. Herramientas y Tecnologías

**OWASP ZAP (Zed Attack Proxy)** es una herramienta de código abierto diseñada para realizar pruebas de seguridad en aplicaciones web. Desarrollada por el Open Web Application Security Project (OWASP), ZAP proporciona una amplia gama de funciones para identificar vulnerabilidades y evaluar la seguridad de aplicaciones durante el ciclo de desarrollo. Su interfaz intuitiva permite a los usuarios realizar escaneos automatizados, así como llevar a cabo pruebas manuales de seguridad, como la exploración de aplicaciones y la identificación de puntos débiles. ZAP es particularmente útil para detectar problemas de seguridad comunes, como inyecciones SQL, ataques de cross-site scripting (XSS) y configuraciones incorrectas. Además, ZAP ofrece soporte para integraciones con otras herramientas de desarrollo y pruebas, lo que facilita la inclusión de prácticas de seguridad en el proceso de desarrollo ágil. Su capacidad para generar reportes detallados ayuda a los equipos de desarrollo a abordar las vulnerabilidades de manera efectiva y mejorar la seguridad general de sus aplicaciones.

#### 3.3.2. Escenarios de Prueba de Seguridad

```gherkin
Background:
    Given que el endpoint "http://localhost:8004" está accesible
```

<details open>
  <summary><b><i>Escenario 1:</i></b> Verificación de encabezados de seguridad HTTP.</summary>

```gherkin
Scenario: Verificación de encabezados de seguridad HTTP
  Given el sitio web "http://localhost:8004" debe incluir varios encabezados de seguridad HTTP
  When se realiza un análisis de encabezados HTTP
  Then los encabezados esperados deben estar presentes, incluyendo "Strict-Transport-Security", "Content-Security-Policy", y "X-Content-Type-Options"
```

</details>

<details open>
  <summary><b><i>Escenario 2:</i></b> Verificación de vulnerabilidades en bibliotecas JavaScript.</summary>

```gherkin
Scenario: Verificación de vulnerabilidades en bibliotecas JavaScript
  Given el sitio web "http://localhost:8004" utiliza bibliotecas JavaScript de terceros
  When se realiza un análisis de bibliotecas JavaScript
  Then no se deben encontrar bibliotecas vulnerables, como aquellas indicadas por Retire.js
```

</details>

<details open>
  <summary><b><i>Escenario 3:</i></b> Verificación de la configuración de cookies.</summary>

```gherkin
Scenario: Verificación de la configuración de cookies
  Given el sitio web "http://localhost:8004" utiliza cookies para el manejo de sesiones
  When se realiza un análisis de cookies
  Then todas las cookies deben estar configuradas con las banderas adecuadas, incluyendo "HttpOnly" y "Secure"
```

</details>

<details open>
  <summary><b><i>Escenario 4:</i></b> Verificación de la protección contra ataques XSS.</summary>

```gherkin
Scenario: Verificación de la protección contra ataques XSS
  Given el sitio web "http://localhost:8004" permite la entrada de usuarios
  When se realiza un análisis de entrada para detectar vulnerabilidades XSS
  Then no se deben encontrar puntos de entrada vulnerables a ataques de cross-site scripting (XSS)
```

</details>

<details open>
  <summary><b><i>Escenario 5:</i></b> Verificación de la política de seguridad de contenido (CSP).</summary>

```gherkin
Scenario: Verificación de la política de seguridad de contenido (CSP)
  Given el sitio web "http://localhost:8004" debe tener una política de seguridad de contenido configurada
  When se realiza un análisis de la política de seguridad de contenido
  Then la política debe estar correctamente configurada y debe incluir directivas como "default-src" y "script-src"
```

</details>

<details open>
  <summary><b><i>Escenario 6:</i></b> Verificación de la protección contra el clickjacking.</summary>

```gherkin
Scenario: Verificación de la protección contra el clickjacking
  Given el sitio web "http://localhost:8004" debe protegerse contra ataques de clickjacking
  When se realiza un análisis de encabezados HTTP
  Then debe estar presente el encabezado "X-Frame-Options" con una configuración segura
```

</details>

<details open>
  <summary><b><i>Escenario 7:</i></b> Verificación de redirecciones abiertas.</summary>

```gherkin
Scenario: Verificación de redirecciones abiertas
  Given el sitio web "http://localhost:8004" debe manejar las redirecciones de forma segura
  When se realiza un análisis para detectar redirecciones abiertas
  Then no deben encontrarse vulnerabilidades de redirección abierta
```

</details>

<details open>
  <summary><b><i>Escenario 8:</i></b> Verificación de exposición de información sensible.</summary>

```gherkin
Scenario: Verificación de exposición de información sensible
  Given el sitio web "http://localhost:8004" debe proteger la información sensible
  When se realiza un análisis para detectar la exposición de información sensible
  Then no se debe encontrar información sensible expuesta en encabezados HTTP o mensajes de error
```

</details>

<details open>
  <summary><b><i>Escenario 9:</i></b> Verificación de configuraciones de seguridad en la administración de sesiones.</summary>

```gherkin
Scenario: Verificación de configuraciones de seguridad en la administración de sesiones
  Given el sitio web "http://localhost:8004" gestiona sesiones de usuario
  When se realiza un análisis de la gestión de sesiones
  Then la sesión debe estar protegida contra ataques como el secuestro de sesión y el manejo inseguro de IDs de sesión
```

</details>

<details open>
  <summary><b><i>Escenario 10:</i></b> Verificación de la protección contra ataques de CSRF.</summary>

```gherkin
Scenario: Verificación de la protección contra ataques de CSRF
  Given el sitio web "http://localhost:8004" debe protegerse contra ataques de falsificación de solicitudes entre sitios (CSRF)
  When se realiza un análisis para detectar la protección contra CSRF
  Then deben encontrarse y validarse tokens anti-CSRF en formularios y solicitudes
```

</details>

<details open>
  <summary><b><i>Escenario 11:</i></b> Verificación de vulnerabilidades de exposición de información.</summary>

```gherkin
Scenario: Verificación de vulnerabilidades de exposición de información
  Given el sitio web "http://localhost:8004" debe manejar la exposición de información con cuidado
  When se realiza un análisis para detectar la divulgación de información sensible
  Then no deben encontrarse vulnerabilidades de divulgación de información, como mensajes de error o encabezados de respuesta que revelen detalles internos
```

</details>

<p align="center">
  <img src="resources/security_test.png" alt="Performance Test" />
</p>

## 4. Referencias

[1] W. by Iamprovidence, **“Backend side architecture evolution (N-layered, DDD, Hexagon, Onion, Clean Architecture)”**, Medium, 27-jun-2023. [En línea]. Disponible en: [https://medium.com/@iamprovidence/backend-side-architecture-evolution-n-layered-ddd-hexagon-onion-clean-architecture-643d72444ce4](https://medium.com/@iamprovidence/backend-side-architecture-evolution-n-layered-ddd-hexagon-onion-clean-architecture-643d72444ce4).

[2] C. Ramalingam, **“Building domain driven microservices - Walmart global tech blog - medium”**, Walmart Global Tech Blog, 01-jul-2020. [En línea]. Disponible en: [https://medium.com/walmartglobaltech/building-domain-driven-microservices-af688aa1b1b8](https://medium.com/walmartglobaltech/building-domain-driven-microservices-af688aa1b1b8).

[3] J. Loscalzo, **“Domain Driven Design: principios, beneficios y elementos — Segunda Parte”**, Medium, 18-jun-2018. [En línea]. Disponible en: [https://medium.com/@jonathanloscalzo/domain-driven-design-principios-beneficios-y-elementos-segunda-parte-337d77dc8566](https://medium.com/@jonathanloscalzo/domain-driven-design-principios-beneficios-y-elementos-segunda-parte-337d77dc8566).

[4] P. Martinez, **“Domain-Driven Design: Everything you always wanted to know”**, SSENSE-TECH, 15-may-2020. [En línea]. Disponible en: [https://medium.com/ssense-tech/domain-driven-design-everything-you-always-wanted-to-know-about-it-but-were-afraid-to-ask-a85e7b74497a](https://medium.com/ssense-tech/domain-driven-design-everything-you-always-wanted-to-know-about-it-but-were-afraid-to-ask-a85e7b74497a).
