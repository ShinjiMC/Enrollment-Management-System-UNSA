# Arquitectura DDD y Pruebas del Microservicio de Usuarios

## 1. Descripción
El microservicio de usarios se encarga de gestionar todas las transacciones financieras relacionadas con los pagos de los estudiantes, incluyendo la creación, actualización, obtención y eliminación de pagos.
- **Contexto Delimitado:** Gestión de Pagos

## 2. Arquitectura DDD

### 2.1. Capas de la Arquitectura

<details open>
  <summary><b><i>2.1.1. Capa de Presentación</b></i></summary>
  <ul>
    <li>Controladores</li>
    <ul>
      <li><b>PaymentController</b>: Gestiona las solicitudes HTTP relacionadas con los pagos. Incluye métodos para crear, actualizar, obtener y eliminar pagos, delegando la lógica a los servicios de aplicación.</li>
    </ul>
  </ul>
</details>
<details open>
  <summary><b><i>2.1.2. Capa de Aplicación</b></i></summary>
  <ul>
    <li>Servicios de aplicación</li>
    <ul>
      <li><b>PaymentService</b>: Contiene la lógica de negocio específica de la aplicación. Coordina las operaciones entre el controlador y el dominio, incluyendo la validación y transformación de datos.</li>
    </ul>
    <li>DTOs</li>
    <ul>
      <li><b>PaymentDto</b>: Objeto de transferencia de datos utilizado para encapsular los datos de un pago en una estructura simple que puede ser utilizada en la capa de presentación.</li>
    </ul>
    <li>Mapping</li>
    <ul>
      <li><b>PaymentMapper</b>: Clase responsable de mapear entre entidades de dominio y DTOs, asegurando que los datos se transfieran correctamente entre capas.</li>
    </ul>
  </ul>
</details>
<details open>
  <summary><b><i>2.1.3. Capa de Dominio</b></i></summary>
  <ul>
    <li>Entidades</li>
    <ul>
      <li><b>Payment</b>: Representa la entidad principal del sistema de pagos, incluyendo propiedades como <code>PaymentId</code>, <code>Amount</code>, <code>PaymentDate</code>, <code>StudentId</code>, entre otras.</li>
    </ul>
    <li>Value Objects</li>
    <ul>
      <li><b>Money</b>: Representa un objeto de valor para manejar cantidades monetarias con propiedades como <code>Amount</code> y <code>Currency</code>.</li>
      <li><b>PaymentDetails</b>: Representa detalles adicionales del pago que no cambian la identidad del pago.</li>
    </ul>
    <li>Agregados</li>
    <ul>
      <li><b>PaymentAggregate</b>: Agrupa entidades y objetos de valor relacionados al pago, asegurando la consistencia interna del agregado.</li>
    </ul>
    <li>Servicios de dominio</li>
    <ul>
      <li><b>PaymentDomainService</b>: Contiene lógica de negocio compleja que involucra múltiples entidades o agregados. Por ejemplo, cálculos de comisiones o validaciones específicas de dominio.</li>
    </ul>
    <li>Interfaces de repositorio</li>
    <ul>
      <li><b>IPaymentRepository</b>: Define los métodos que deben ser implementados para la gestión de los pagos en el repositorio. Incluye métodos como <code>AddPayment</code>, <code>UpdatePayment</code>, <code>GetPaymentById</code> y <code>DeletePayment</code>.</li>
    </ul>
    <!-- <li>Eventos de dominio</li>
    <ul>
      <li><b>PaymentCreatedEvent</b>: Evento que se dispara cuando se crea un nuevo pago.</li>
      <li><b>PaymentUpdatedEvent</b>: Evento que se dispara cuando se actualiza un pago.</li>
      <li><b>PaymentDeletedEvent</b>: Evento que se dispara cuando se elimina un pago.</li>
    </ul> -->
  </ul>
</details>
<details open>
  <summary><b><i>2.1.4. Capa de Repositorio</b></i></summary>
  <ul>
    <li>Implementaciones de repositorios</li>
    <ul>
      <li><b>PaymentRepository</b>: Implementación concreta de <code>IPaymentRepository</code>. Utiliza el contexto de la base de datos para realizar operaciones CRUD sobre los pagos.</li>
    </ul>
    <li>Contexto de la base de datos</li>
    <ul>
      <li><b>PaymentDbContext</b>: Clase que maneja la conexión a la base de datos y proporciona acceso a las entidades a través de DbSets. Configura mapeos y relaciones entre entidades.</li>
    </ul>
    <li>Integraciones externas</li>
    <ul>
      <li><b>PaymentGatewayIntegration</b>: Servicio que se integra con proveedores de pagos externos para procesar transacciones.</li>
      <li><b>NotificationService</b>: Servicio que se encarga de enviar notificaciones a los usuarios sobre el estado de sus pagos.</li>
    </ul>
  </ul>
</details>

### 2.2. Diagrama de la Arquitectura

![](resources/PaymentsArchitecture.png)

## 3. Pruebas

### 3.1. Pruebas de API

#### 3.1.1. Herramientas y Tecnologías
Descripción de las herramientas utilizadas para las pruebas de API (por ejemplo, Postman, Swagger, etc.).


#### 3.1.2. Escenarios de Prueba de API

<details open>
  <summary><b><i>Background:</i></b> El endpoint "/api/Admin/register/admin" es accesible y está disponible</summary>

  <details open>
    <summary><b><i>Escenario 1:</i></b> Registrar exitosamente un usuario administrador.</summary>

```gherkin
    Escenario: Registrar exitosamente un usuario administrador
      Given se proporciona una payload válida de datos
        {
            "fullName": "Aldo Benito Martinez",
            "userData": {
                "userName": "bananon",
                "password": "Aldechi@123",
                "confirmPassword": "Aldechi@123",
                "role": "ADMIN"
            },
            "email": "aldechi001@example.com",
            "phoneNumber": "123456789"
        }

      When se envía una solicitud POST a "/api/Admin/register/admin"
      Then se recibe una respuesta válida con código 201
      And se recibe un mensaje de "Admin user created successfully"

```

    
  </details>

  <details open>
    <summary><b><i>Escenario 2:</i></b> Fallar al registrar debido a datos inválidos.</summary>
    
```gherkin
    Escenario: Fallar al registrar debido a datos inválidos
      Given se proporciona un payload inválido para registrar un administrador:
      {
        "fullName": "as",
        "userData": {
            "userName": "",
            "password": "123",
            "confirmPassword": "456",
            "role": "STUDENT"
        },
        "email": "aldechi001example.com",
        "cui": "",
        "courseIds": [],
        "academicPerformance": -1,
        "credit": 1,
        "schoolId": ""
      }

      When se envía una solicitud POST a "/api/Admin/register/admin"
      Then se recibe una respuesta con código 400
      And se recibe un respuesta formato JSON
      And se recibe un mensaje de "One or more validation errors occurred."
```
  </details>

 

</details>

<details open>
  <summary><b><i>Background:</i></b> El endpoint "/api/Admin/{id}" es accesible y está disponible</summary>

  <details open>
    <summary><b><i>Escenario 4:</i></b> Actualizar exitosamente datos de un administrador.</summary>
    
```gherkin
    Escenario: Actualizar exitosamente datos de un administrador
      Given el ID del administrador existe y se proporciona un payload para actualizar el administrador
        { "id": 1  }
      When se envía una solicitud PUT a "/api/Admin/{id}"
      Then se recibe una respuesta válida con código 200
      And el cuerpo de la respuesta debe contener  “User updated successfully”
```
  </details>

  <details open>
    <summary><b><i>Escenario 5:</i></b> Actualización fallida debido a ID inexistente.</summary>
    
```gherkin
    Escenario: Actualización fallida debido a ID inexistente
      Given el ID del administrador no existe y se proporciona un payload válido para actualizar el administrador
        { 
          "id": 10000000
        }
      When se envía una solicitud PUT a "/api/Admin/{id}" con una carga útil válida
      Then se recibe una respuesta válida con código 404
      And se recibe respuesta formato json
      And se recibe un mensjae de "Not found"
```
  </details>

  <details open>
    <summary><b><i>Escenario 6:</i></b> Eliminar exitosamente un administrador.</summary>
    

```gherkin
    Escenario: Eliminar exitosamente un administrador
      Given el ID del administrador existe
        {
          "id" : 3
        } 
      When se envía una solicitud DELETE a "/api/Admin/{id}"
      Then se recibe una respuesta válida con código 204
      And se comprueba por petición Get que el id no exista
      And se obtiene codigo 404
```
  </details>

  <details open>
    <summary><b><i>Escenario 7:</i></b> Eliminar un administrador inexistente</summary>
    

```gherkin
    Escenario:  Eliminar un administrador inexistente
      Given el ID del administrador no existe
       {
        "id" : 10000000000
       }
      When se envía una solicitud DELETE a "/api/Admin/{id}"
      Then se recibe una respuesta válida con código 
      And se recibe respuesta formato json
      And se recibe mensaje "Not Found"
```
  </details>

  <details open>
    <summary><b><i>Escenario 8:</i></b> Obtener administrador existente.</summary>
    

```gherkin
    Escenario: Obtener administrador existente
      Given el ID del administrador existe
        {
          "id" : 1
        }
      When se envía una solicitud GET a "/api/Admin/{id}"
      Then se recibe una respuesta válida con código 200
      And se recibe respuesta en formato JSON
```
  </details>

  <details open>
    <summary><b><i>Escenario 9:</i></b> Error por administrador inexistente.</summary>
    
    
```gherkin
    Escenario: Error por administrador inexistente
      Given el ID del administrador no existe
        {
          "id" : 100000000
        }
      When se envía una solicitud GET a "/api/Admin/{id}"
      Then se recibe una respuesta válida con código 404
      And se recibe respuesta formato json
      And se recibe mensajae "Not found"
```
  </details>

</details>

<details open>
  <summary><b><i>Background:</i></b> El endpoint "/api/Admin/all" es accesible y está disponible</summary>

  <details open>
    <summary><b><i>Escenario 10:</i></b> Obtener todos los administradores</summary>
    
```gherkin
    Escenario: Obtener todos los administradores
      Given existan datos en el servidor
      When se envía una solicitud GET a "/api/Admin/all"
      Then se recibe una respuesta válida con código 200
      And se recibe una respuesta en formato JSON
      And se recibe un array en formato JSON
```
  </details>

</details>

<details open>
  <summary><b><i>Background:</i></b> El endpoint "/api/Admin/register/student" es accesible y está disponible</summary>

  <details open>
    <summary><b><i>Escenario 11:</i></b> Registrar exitosamente un estudiante.</summary>
    
```gherkin
    Escenario: Registrar exitosamente un estudiante
      Given se proporciona un payload válido para registrar un estudiante
        {
          "id": "",
          "fullName": "John Doe2",
          "userData": {
              "userName": "bananon",
              "password": "Aldechi@123",
              "confirmPassword": "Aldechi@123",
              "role": "STUDENT"
          },
          "cui": "1234562",
          "email": "john.doe@example.com",
          "courseIds": [
              "08b34482-e007-4ab5-a66d-c2765f31aa94",
              "207f058b-b9e7-4a7b-b516-346320d3d512",
              "37302ffc-5432-4431-9eb2-d23d52b9a5bd"
          ],
          "academicPerformance": 1,
          "credit": 30,
          "schoolId": "school123"
      }
      When se envía una solicitud POST a "/api/Admin/register/student" con un payload válido
      Then se recibe una respuesta válida con código 201
      And se recibe una respuesta formato JSON
      And recibe un mensaje de "Student created successfully"
```
  </details>

  <details open>
    <summary><b><i>Escenario 12:</i></b> Fallar debido a datos inválidos.</summary>
    
```gherkin
    Escenario: Fallar debido a datos inválidos
      Given se proporciona un payload inválido para registrar un estudiante
        {
          "fullName": "as",
          "userData": {
            "userName": "",
            "password": "123",
            "confirmPassword": "456",
            "role": "STUDENT"
          },
          "email": "aldechi001example.com",
          "cui": "",
          "courseIds": [],
          "academicPerformance": -1,
          "credit": 1,
          "schoolId": ""
        }
      When se envía una solicitud POST a "/api/Admin/register/student" con un payload inválido
      Then se recibe una respuesta válida con código 400
      And se recibe un respuesta formato JSON
      And se recibe un mensaje de "One or more validation errors occurred."
```
  </details>
   <details open>
    <summary><b><i>Escenario 13:</i></b> Eliminar exitosamente un estudiante.</summary>
    

```gherkin
    Escenario: Eliminar exitosamente un estudiante
      Given el ID del administrador existe
        {
          "id" : 3
        } 
      When se envía una solicitud DELETE a "/api/Admin/student/{id}"
      Then se recibe una respuesta válida con código 204
      And se comprueba por petición Get que el id no exista
      And se obtiene codigo 404
```
  </details>

  <details open>
    <summary><b><i>Escenario 14:</i></b> Eliminar un estudiante inexistente</summary>
    

```gherkin
    Escenario:  Eliminar un administrador inexistente
      Given el ID del administrador no existe
       {
        "id" : 10000000000
       }
      When se envía una solicitud DELETE a "/api/Admin/student/{id}"
      Then se recibe una respuesta válida con código 
      And se recibe respuesta formato json
      And se recibe mensaje "Not Found"
```
  </details>


</details>

<details open>
  <summary><b><i>Background:</i></b> El endpoint "/api/Student/{id}" es accesible y está disponible</summary>

  <details open>
    <summary><b><i>Escenario 15:</i></b> Obtener estudiante existente.</summary>
    
```gherkin
    Escenario: Obtener estudiante existente
      Given el ID del estudiante existe
        {
          "id": 1
        }
      When se envía una solicitud GET a "/api/Student/{id}"
      Then se recibe una respuesta válida con código 200
      And la respuesta es en formato JSON
```
  </details>

  <details open>
    <summary><b><i>Escenario 16:</i></b> Obtener estudiante inexistente.</summary>
    
```gherkin
    Escenario: Obtener estudiante inexistente
      Given el ID del estudiante no existe
        {
          "id": 10000000
        }
      When se envía una solicitud GET a "/api/Student/{id}"
      Then se recibe una respuesta válida con código 404
      And se recibe un mensaje de "Not found"
```
  </details>

</details>

<details open>
  <summary><b><i>Background:</i></b> El endpoint "/api/Student/all" es accesible y está disponible</summary>

  <details open>
    <summary><b><i>Escenario 17:</i></b> Obtener todos los estudiantes.</summary>
    
```gherkin
    Escenario: Obtener todos los estudiantes
      Given existen estudiantes registrados en la base de datos
      When se envía una solicitud GET a "/api/Student/all"
      Then se recibe una respuesta válida con código 200
      And se espera recibir un array de estudiantes
```
  </details>

</details>

<details open>
  <summary><b><i>Background:</i></b> El endpoint "/api/Student/{id}/courses" es accesible y está disponible</summary>

  <details open>
    <summary><b><i>Escenario 18:</i></b> Obtener cursos de un estudiante existente.</summary>
    
```gherkin
    Escenario: Obtener cursos de un estudiante existente
      Given el ID del estudiante existe
        {
          "id" : 3
        }
      When se envía una solicitud GET a "/api/Student/{id}/courses"
      Then se recibe una respuesta válida con código 200
      And se espera recibir un array de cursos
      And se espera recibir una respuesta en formato json
      And se espera recibir un array de string de courseid
```
  </details>

  <details open>
    <summary><b><i>Escenario 19:</i></b> Obtener cursos de un estudiante inexistente.</summary>
    
```gherkin
    Escenario: Obtener cursos de un estudiante inexistente
      Given el ID del estudiante no existe
        {
          "id" : 100000 
        }
      When se envía una solicitud GET a "/api/Student/{id}/courses"
      Then se recibe una respuesta válida con código 404
      And se recibe un mensaje "Not found"
      And recibe una respuesta en formato json
```
  </details>

</details>

<details open>
  <summary><b><i>Background:</i></b> El endpoint "/api/ExternalService/student/{id}" es accesible y está disponible</summary>

  <details open>
    <summary><b><i>Escenario 20:</i></b> Obtener exitosamente datos de cursos del estudiante.</summary>
    
```gherkin
    Escenario: Obtener exitosamente datos de cursos del estudiante
      Given el ID del estudiante existe
        {
          "id": 3
        }
      When se envía una solicitud GET a "/api/ExternalService/student/{id}"
      Then se recibe una respuesta válida con código 200
      And se espera recibir una respuesta en formato JSON
      And se espera recibir un array de string de cursos
```
  </details>

  <details open>
    <summary><b><i>Escenario 21:</i></b> Solicitar con ID de estudiante no válido.</summary>
    
```gherkin
    Escenario: Solicitar con ID de estudiante no válido
      Given se proporciona un ID de estudiante no válido
        {
          "id": 100000
        }
      When se envía una solicitud GET a "/api/ExternalService/student/{id}"
      Then se recibe una respuesta válida con código 404
      And se recibe respuesta formato json
      And se recibe un mensaje "Not found"
```
  </details>

</details>


---

### 3.2. Pruebas de Rendimiento

#### 3.2.1. Herramientas y Tecnologías
Descripción de las herramientas utilizadas para las pruebas de rendimiento (por ejemplo, Apache JMeter, k6, etc.).

#### 3.2.2. Escenarios de Prueba de Rendimiento
<details open>
  <summary><b><i>Escenario 1:</i></b> Prueba de carga bajo 100 solicitudes simultáneas GET.</summary>
  
  ```gherkin
  Given el endpoint "api/payments/" está disponible
    When se envían 100 solicitudes GET simultáneas al endpoint
    Then todas las respuestas deben ser 200 OK
    And el tiempo de respuesta promedio debe ser menor a 2 segundos
    And no debe haber errores o caídas del servicio
  ```
</details>

<details open>
  <summary><b><i>Escenario 2:</i></b> Prueba de estrés con picos de tráfico.</summary>
  <span>gherkin aqui</span>
</details>

<details open>
  <summary><b><i>Escenario 3:</i></b> Prueba de escalabilidad.</summary>
  <span>gherkin aqui</span>
</details>


### 3.3. Pruebas de Seguridad

#### 3.3.1. Herramientas y Tecnologías
Descripción de las herramientas utilizadas para las pruebas de seguridad (por ejemplo, OWASP ZAP, Burp Suite, etc.).

#### 3.3.2. Escenarios de Prueba de Seguridad
<details open>
  <summary><b><i>Escenario 1:</i></b> Verificación de la autenticación y autorización.</summary>

  ```gherkin
  Scenario: Verificación de la autenticación y autorización
    Given el endpoint "api/payments/" requiere autenticación
    When un usuario no autenticado intenta acceder al endpoint
    Then la respuesta debe ser 401 Unauthorized
  ```
</details>

<details open>
  <summary><b><i>Escenario 2:</i></b> Prueba de inyección SQL.</summary>

  <span>gherkin aqui</span>
</details>

<details open>
  <summary><b><i>Escenario 3:</i></b> Prueba de XSS (Cross-Site Scripting).</summary>

  <span>gherkin aqui</span>
</details>

<details open>
  <summary><b><i>Escenario 4:</i></b> Prueba de CSRF (Cross-Site Request Forgery).</summary>
  
  <span>gherkin aqui</span>
</details>

## 4. Referencias

[1] W. by Iamprovidence, **“Backend side architecture evolution (N-layered, DDD, Hexagon, Onion, Clean Architecture)”**, Medium, 27-jun-2023. [En línea]. Disponible en: [https://medium.com/@iamprovidence/backend-side-architecture-evolution-n-layered-ddd-hexagon-onion-clean-architecture-643d72444ce4](https://medium.com/@iamprovidence/backend-side-architecture-evolution-n-layered-ddd-hexagon-onion-clean-architecture-643d72444ce4).

[2] C. Ramalingam, **“Building domain driven microservices - Walmart global tech blog - medium”**, Walmart Global Tech Blog, 01-jul-2020. [En línea]. Disponible en: [https://medium.com/walmartglobaltech/building-domain-driven-microservices-af688aa1b1b8](https://medium.com/walmartglobaltech/building-domain-driven-microservices-af688aa1b1b8).

[3] J. Loscalzo, **“Domain Driven Design: principios, beneficios y elementos — Segunda Parte”**, Medium, 18-jun-2018. [En línea]. Disponible en: [https://medium.com/@jonathanloscalzo/domain-driven-design-principios-beneficios-y-elementos-segunda-parte-337d77dc8566](https://medium.com/@jonathanloscalzo/domain-driven-design-principios-beneficios-y-elementos-segunda-parte-337d77dc8566).

[4] P. Martinez, **“Domain-Driven Design: Everything you always wanted to know”**, SSENSE-TECH, 15-may-2020. [En línea]. Disponible en: [https://medium.com/ssense-tech/domain-driven-design-everything-you-always-wanted-to-know-about-it-but-were-afraid-to-ask-a85e7b74497a](https://medium.com/ssense-tech/domain-driven-design-everything-you-always-wanted-to-know-about-it-but-were-afraid-to-ask-a85e7b74497a).
