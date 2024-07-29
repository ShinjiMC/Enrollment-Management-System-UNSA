# Arquitectura DDD y Pruebas del Microservicio de Notificaciones

## 1. Descripción
El microservicio de notificaciones se encarga de gestionar todas las notificaciones enviadas a los usuarios, incluyendo la creación, actualización, obtención y eliminación de notificaciones.
- **Contexto Delimitado:** Gestión de Notificaciones

## 2. Arquitectura DDD

### 2.1. Capas de la Arquitectura

<details open>
  <summary><b><i>2.1.1. Capa de Presentación</b></i></summary>
  <ul>
    <li>Controladores</li>
    <ul>
      <li><b>NotificationController</b>: Gestiona las solicitudes HTTP relacionadas con las notificaciones. Incluye métodos para crear, actualizar, obtener y eliminar notificaciones, delegando la lógica a los servicios de aplicación.</li>
      <li><b>UserController</b>: Gestiona las solicitudes HTTP relacionadas con los usuarios. Incluye métodos para gestionar la información de los usuarios.</li>
    </ul>
  </ul>
</details>
<details open>
  <summary><b><i>2.1.2. Capa de Aplicación</b></i></summary>
  <ul>
    <li>Servicios de aplicación</li>
    <ul>
      <li><b>NotificationService</b>: Contiene la lógica de negocio específica de las notificaciones. Coordina las operaciones entre el controlador y el dominio, incluyendo la validación y transformación de datos.</li>
      <li><b>UserService</b>: Contiene la lógica de negocio específica de los usuarios. Coordina las operaciones entre el controlador y el dominio.</li>
    </ul>
    <li>DTOs</li>
    <ul>
      <li><b>NotificationDto</b>: Objeto de transferencia de datos utilizado para encapsular los datos de una notificación en una estructura simple que puede ser utilizada en la capa de presentación.</li>
      <li><b>UserDto</b>: Objeto de transferencia de datos utilizado para encapsular los datos de un usuario en una estructura simple que puede ser utilizada en la capa de presentación.</li>
    </ul>
  </ul>
</details>
<details open>
  <summary><b><i>2.1.3. Capa de Dominio</b></i></summary>
  <ul>
    <li>Entidades</li>
    <ul>
      <li><b>Notification</b>: Representa la entidad principal del sistema de notificaciones, incluyendo propiedades como <code>NotificationId</code>, <code>Message</code>, <code>SentDate</code>, <code>UserId</code>, entre otras.</li>
      <li><b>User</b>: Representa la entidad del usuario, incluyendo propiedades como <code>UserId</code>, <code>Username</code>, <code>Email</code>, entre otras.</li>
    </ul>
    <li>Value Objects</li>
    <ul>
      <li><b>Message</b>: Representa un objeto de valor para manejar los mensajes de las notificaciones.</li>
    </ul>
    <li>Agregados</li>
    <ul>
      <li><b>NotificationAggregate</b>: Agrupa entidades y objetos de valor relacionados a las notificaciones, asegurando la consistencia interna del agregado.</li>
    </ul>
    <li>Servicios de dominio</li>
    <ul>
      <li><b>NotificationDomainService</b>: Contiene lógica de negocio compleja que involucra múltiples entidades o agregados. Por ejemplo, la lógica para enviar notificaciones.</li>
      <li><b>UserDomainService</b>: Contiene lógica de negocio compleja relacionada con los usuarios.</li>
    </ul>
    <li>Interfaces de repositorio</li>
    <ul>
      <li><b>INotificationRepository</b>: Define los métodos que deben ser implementados para la gestión de las notificaciones en el repositorio. Incluye métodos como <code>AddNotification</code>, <code>UpdateNotification</code>, <code>GetNotificationById</code> y <code>DeleteNotification</code>.</li>
      <li><b>IUserRepository</b>: Define los métodos que deben ser implementados para la gestión de los usuarios en el repositorio.</li>
    </ul>
  </ul>
</details>
<details open>
  <summary><b><i>2.1.4. Capa de Repositorio</b></i></summary>
  <ul>
    <li>Implementaciones de repositorios</li>
    <ul>
      <li><b>NotificationRepository</b>: Implementación concreta de <code>INotificationRepository</code>. Utiliza el contexto de la base de datos para realizar operaciones CRUD sobre las notificaciones.</li>
      <li><b>UserRepository</b>: Implementación concreta de <code>IUserRepository</code>. Utiliza el contexto de la base de datos para realizar operaciones CRUD sobre los usuarios.</li>
    </ul>
    <li>Contexto de la base de datos</li>
    <ul>
      <li><b>MongoDbContext</b>: Clase que maneja la conexión a la base de datos y proporciona acceso a las entidades a través de DbSets. Configura mapeos y relaciones entre entidades.</li>
    </ul>
  </ul>
</details>

### 2.2. Diagrama de la Arquitectura

![Diagrama de la Arquitectura](resources/NotificationsArchitecture.png)

## 3. Pruebas

### 3.1. Pruebas de API

#### 3.1.1. Herramientas y Tecnologías
Descripción de las herramientas utilizadas para las pruebas de API (por ejemplo, Postman, Swagger, etc.).

#### 3.1.2. Escenarios de Prueba de API

<details open>
<summary><b><i>UsersController</i></b></summary>

<details open>
<summary><b><i>GET api/v1/users</i></b></summary>
<details open>
<summary><b><i>Escenario 1:</i></b> Verificación de la obtención de todos los usuarios.</summary>

```gherkin
    Escenario: Verificación de la obtención de todos los usuarios
       Given el endpoint "api/v1/users" está disponible
       When se envía una solicitud GET
       Then la respuesta debe ser 200 OK
       And la respuesta debe ser un array de usuarios
```
</details>

</details>

<details open>
<summary><b><i>POST api/v1/users</i></b></summary>

<details open>
<summary><b><i>Escenario 1:</i></b> Verificación de la creación de un usuario con datos válidos.</summary>

```gherkin
Escenario: Verificación de la creación de un usuario con datos válidos
  Given el endpoint "api/v1/users" está disponible
  And el cuerpo de la solicitud es:
"""
{
  "id": 501,
  "name": "Lyon",
  "preference": "Correo",
  "contactInfo": "lyonelmessi@gmail.com",
  "isActive": true
}
"""
  When se envía una solicitud POST con los datos del usuario
  Then la respuesta debe ser 201 Created
  And la respuesta debe contener el usuario creado
```
 </details>

<details open>
  <summary><b><i>Escenario 2:</i></b> Verificación de la creación de un usuario con ID duplicado.</summary>

```gherkin
Escenario: Verificación de la creación de un usuario con ID duplicado
  Given el endpoint "api/v1/users" está disponible
  And el cuerpo de la solicitud es:
  """
  {
    "id": 1,
    "name": "Lyon",
    "preference": "Correo",
    "contactInfo": "lyonelmessi@gmail.com",
    "isActive": true
  }
  """
  When se envía una solicitud POST con un ID de usuario que ya existe
  Then la respuesta debe ser 409 Conflict
  And la respuesta debe contener un mensaje de error
```
</details>

<details open>
  <summary><b><i>Escenario 3:</i></b> Verificación de la creación de un usuario con datos incompletos.</summary>

```gherkin
Escenario: Verificación de la creación de un usuario con datos incompletos
  Given el endpoint "api/v1/users" está disponible
  And el cuerpo de la solicitud es:
  """
  {
    "id": 502,
    "name": "Lyon",
    "preference": "Correo",
    "isActive": true
  }
  """
  When se envía una solicitud POST con datos incompletos del usuario
  Then la respuesta debe ser 400 Bad Request
  And la respuesta debe contener un mensaje de error
```
</details>

</details>

<details open>
  <summary><b><i>GET api/v1/users/{id}</i></b></summary>

  <details open>
    <summary><b><i>Escenario 1:</i></b> Verificación de la obtención de un usuario existente.</summary>

  ```gherkin
  Escenario: Verificación de la obtención de un usuario existente
    Given el endpoint "api/v1/users/{id}" está disponible
    When se envía una solicitud GET con un ID de usuario válido
    Then la respuesta debe ser 200 OK
    And la respuesta debe contener el usuario solicitado
  ```
  </details>

  <details open>
    <summary><b><i>Escenario 2:</i></b> Verificación de la obtención de un usuario inexistente.</summary>

  ```gherkin
  Escenario: Verificación de la obtención de un usuario inexistente
    Given el endpoint "api/v1/users/{id}" está disponible
    When se envía una solicitud GET con un ID de usuario no válido
    Then la respuesta debe ser 404 Not Found
    And la respuesta debe contener un mensaje de error
  ```
  </details>

</details>

<details open>
  <summary><b><i>PUT api/v1/users/{id}</i></b></summary>

  <details open>
    <summary><b><i>Escenario 1:</i></b> Verificación de la actualización de un usuario existente.</summary>

  ```gherkin
  Escenario: Verificación de la actualización de un usuario existente
    Given el endpoint "api/v1/users/{id}" está disponible
    And el cuerpo de la solicitud es:
    """
    {
      "id": 501,
      "name": "Lyon",
      "preference": "SMS",
      "contactInfo": "987654321",
      "isActive": true
    }
    """
    When se envía una solicitud PUT con datos válidos del usuario
    Then la respuesta debe ser 204 No Content
  ```
  </details>

  <details open>
    <summary><b><i>Escenario 2:</i></b> Verificación de la actualización de un usuario con datos inválidos.</summary>

  ```gherkin
  Escenario: Verificación de la actualización de un usuario con datos inválidos
    Given el endpoint "api/v1/users/{id}" está disponible
    And el cuerpo de la solicitud es:
    """
    {
      "id": 501,
      "name": "Lyon",
      "preference": "SMS",
      "contactInfo": "",
      "isActive": true
    }
    """
    When se envía una solicitud PUT con datos inválidos del usuario
    Then la respuesta debe ser 400 Bad Request
    And la respuesta debe contener un mensaje de error
  ```
  </details>

  <details open>
    <summary><b><i>Escenario 3:</i></b> Verificación de la actualización de un usuario inexistente.</summary>

  ```gherkin
  Escenario: Verificación de la actualización de un usuario inexistente
    Given el endpoint "api/v1/users/{id}" está disponible
    And el cuerpo de la solicitud es:
    """
    {
      "id": 987,
      "name": "Lyon",
      "preference": "SMS",
      "contactInfo": "987654321",
      "isActive": true
    }
    """
    When se envía una solicitud PUT con un ID de usuario no válido
    Then la respuesta debe ser 404 Not Found
    And la respuesta debe contener un mensaje de error
  ```
  </details>

</details>

<details open>
  <summary><b><i>DELETE api/v1/users/{id}</i></b></summary>

  <details open>
    <summary><b><i>Escenario 1:</i></b> Verificación de la eliminación de un usuario existente.</summary>

  ```gherkin
  Escenario: Verificación de la eliminación de un usuario existente
    Given el endpoint "api/v1/users/{id}" está disponible
    When se envía una solicitud DELETE con un ID de usuario válido
    Then la respuesta debe ser 204 No Content
  ```
  </details>

  <details open>
    <summary><b><i>Escenario 2:</i></b> Verificación de la eliminación de un usuario inexistente.</summary>

  ```gherkin
  Escenario: Verificación de la eliminación de un usuario inexistente
    Given el endpoint "api/v1/users/{id}" está disponible
    When se envía una solicitud DELETE con un ID de usuario no válido
    Then la respuesta debe ser 404 Not Found
    And la respuesta debe contener un mensaje de error
  ```
  </details>

</details>

</details>

<details open>
  <summary><b><i>NotificationsController</i></b></summary>

<details open>
  <summary><b><i>POST api/v1/notifications</i></b></summary>

  <details open>
    <summary><b><i>Escenario 1:</i></b> Verificación de la creación de una notificación con datos válidos.</summary>

  ```gherkin
  Escenario: Verificación de la creación de una notificación con datos válidos
    Given el endpoint "api/v1/notifications" está disponible
    And el cuerpo de la solicitud es:
    """
    {
      "type": "payment",
      "message": "Su pago se realizó con éxito",
      "recipientId": 501
    }
    """
    When se envía una solicitud POST con los datos de la notificación
    Then la respuesta debe ser 201 Created
    And la respuesta debe contener la notificación creada
  ```
  </details>

<details open>
    <summary><b><i>Escenario 2:</i></b> Verificación de la creación de una notificación con recipientId inválido.</summary>

  ```gherkin
  Escenario: Verificación de la creación de una notificación con recipientId inválido
    Given el endpoint "api/v1/notifications" está disponible
    And el cuerpo de la solicitud es:
    """
    {
      "type": "payment",
      "message": "Su pago se realizó con éxito",
      "recipientId": 987
    }
    """
    When se envía una solicitud POST con un recipientId no válido
    Then la respuesta debe ser 404 Not Found
    And la respuesta debe contener un mensaje de error
  ```
  </details>

  <details open>
    <summary><b><i>Escenario 3:</i></b> Verificación de la creación de una notificación con datos incompletos.</summary>

  ```gherkin
  Escenario: Verificación de la creación de una notificación con datos incompletos
    Given el endpoint "api/v1/notifications" está disponible
    And el cuerpo de la solicitud es:
    """
    {
      "type": "payment",
      "recipientId": 0
    }
    """
    When se envía una solicitud POST con datos incompletos de la notificación
    Then la respuesta debe ser 400 Bad Request
    And la respuesta debe contener un mensaje de error
  ```
  </details>

  <details open>
    <summary><b><i>Escenario 4:</i></b> Verificación de la creación de una notificación con datos inválidos.</summary>

  ```gherkin
  Escenario: Verificación de la creación de una notificación con datos inválidos
    Given el endpoint "api/v1/notifications" está disponible
    And el cuerpo de la solicitud es:
    """
    {
      "type": "payment",
      "message": "",
      "recipientId": 0
    }
    """
    When se envía una solicitud POST con datos inválidos de la notificación
    Then la respuesta debe ser 400 Bad Request
    And la respuesta debe contener un mensaje de error
  ```
  </details>

</details>

<details open>
  <summary><b><i>GET api/v1/notifications</i></b></summary>

  <details open>
    <summary><b><i>Escenario 1:</i></b> Verificación de la obtención de todas las notificaciones.</summary>

  ```gherkin
  Escenario: Verificación de la obtención de todas las notificaciones
    Given el endpoint "api/v1/notifications" está disponible
    When se envía una solicitud GET
    Then la respuesta debe ser 200 OK
    And la respuesta debe ser un array de notificaciones
  ```
  </details>

</details>

<details open>
  <summary><b><i>GET api/v1/notifications/recipient/{id}</i></b></summary>

  <details open>
    <summary><b><i>Escenario 1:</i></b> Verificación de la obtención de notificaciones por recipientId existente.</summary>

  ```gherkin
  Escenario: Verificación de la obtención de notificaciones por recipientId existente
    Given el endpoint "api/v1/notifications/recipient/{id}" está disponible
    When se envía una solicitud GET con un ID de recipient válido
    Then la respuesta debe ser 200 OK
    And la respuesta debe ser un array de notificaciones
  ```
  </details>

  <details open>
    <summary><b><i>Escenario 2:</i></b> Verificación de la obtención de notificaciones por recipientId inexistente.</summary>

```gherkin
  Escenario: Verificación de la obtención de notificaciones por recipientId inexistente
    Given el endpoint "api/v1/notifications/recipient/{id}" está disponible
    When se envía una solicitud GET con un ID de recipient no válido
    Then la respuesta debe ser 404 Not Found
    And la respuesta debe contener un mensaje de error
  ```
  </details>

</details>

</details>

### 3.2. Pruebas de Rendimiento

#### 3.2.1. Herramientas y Tecnologías
Descripción de las herramientas utilizadas para las pruebas de rendimiento (por ejemplo, Apache JMeter, k6, etc.).

#### 3.2.2. Escenarios de Prueba de Rendimiento

<details open>
  <summary><b><i>Escenario 1:</i></b> Prueba de carga bajo 100 solicitudes simultáneas GET.</summary>

```gherkin
Escenario: Prueba de carga bajo 100 solicitudes simultáneas GET
  Given el endpoint "api/v1/notifications" está disponible
  When se envían 100 solicitudes GET simultáneas al endpoint
  Then todas las respuestas deben ser 200 OK
  And el tiempo de respuesta promedio debe ser menor a 2 segundos
  And no debe haber errores o caídas del servicio
```
</details>

### 3.3. Pruebas de Seguridad

#### 3.3.1. Herramientas y Tecnologías
Descripción de las herramientas utilizadas para las pruebas de seguridad (por ejemplo, OWASP ZAP, Burp Suite, etc.).

#### 3.3.2. Escenarios de Prueba de Seguridad

<details open>
  <summary><b><i>Escenario 1:</i></b> Verificación de la autenticación y autorización.</summary>

```gherkin
Escenario: Verificación de la autenticación y autorización
  Given el endpoint "api/v1/notifications" requiere autenticación
  When un usuario no autenticado intenta acceder al endpoint
  Then la respuesta debe ser 401 Unauthorized
```
</details>

## 4. Referencias

[1] W. by Iamprovidence, **“Backend side architecture evolution (N-layered, DDD, Hexagon, Onion, Clean Architecture)”**, Medium, 27-jun-2023. [En línea]. Disponible en: [https://medium.com/@iamprovidence/backend-side-architecture-evolution-n-layered-ddd-hexagon-onion-clean-architecture-643d72444ce4](https://medium.com/@iamprovidence/backend-side-architecture-evolution-n-layered-ddd-hexagon-onion-clean-architecture-643d72444ce4).

[2] C. Ramalingam, **“Building domain driven microservices - Walmart global tech blog - medium”**, Walmart Global Tech Blog, 01-jul-2020. [En línea]. Disponible en: [https://medium.com/walmartglobaltech/building-domain-driven-microservices-af688aa1b1b8](https://medium.com/walmartglobaltech/building-domain-driven-microservices-af688aa1b1b8).

[3] J. Loscalzo, **“Domain Driven Design: principios, beneficios y elementos — Segunda Parte”**, Medium, 18-jun-2018. [En línea]. Disponible en: [https://medium.com/@jonathanloscalzo/domain-driven-design-principios-beneficios-y-elementos-segunda-parte-337d77dc8566](https://medium.com/@jonathanloscalzo/domain-driven-design-principios-beneficios-y-elementos-segunda-parte-337d77dc8566).

[4] P. Martinez, **“Domain-Driven Design: Everything you always wanted to know”**, SSENSE-TECH, 15-may-2020. [En línea]. Disponible en: [https://medium.com/ssense-tech/domain-driven-design-everything-you-always-wanted-to-know-about-it-but-were-afraid-to-ask-a85e7b74497a](https://medium.com/ssense-tech/domain-driven-design-everything-you-always-wanted-to-know-about-it-but-were-afraid-to-ask-a85e7b74497a).
