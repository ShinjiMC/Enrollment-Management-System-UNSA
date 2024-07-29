# Arquitectura DDD y Pruebas del Microservicio de Pagos

## 1. Descripcion
El microservicio de pagos se encarga de gestionar todas las transacciones financieras relacionadas con los pagos de los estudiantes, incluyendo la creación, actualización, obtención y eliminación de pagos.
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
  <summary><b><i>Escenario 1:</i></b> Verificación de la creación de un pago.</summary>
  
  ```gherkin
  Escenario: Verificación de la creación de un pago
    Given el endpoint "api/payments/" está disponible
    When se envía una solicitud POST con los datos del pago
    Then la respuesta debe ser 201 Created
    And el pago debe estar registrado en el sistema
  ```
</details>

<details open>
  <summary><b><i>Escenario 2:</i></b> Verificación de la obtención de detalles de un pago.</summary>
  
  ```gherkin
  Given el endpoint "api/payments/{id}" está disponible
    When se envía una solicitud GET con un ID de pago válido
    Then la respuesta debe ser 200 OK
    And los detalles del pago deben ser correctos
  ```
</details>

<details open>
  <summary><b><i>Escenario 3:</i></b> Verificación de la actualización de un pago.</summary>
  
  <span>gherkin aqui</span>
</details>

---

### 3.2. Pruebas de Rendimiento

#### 3.2.1. Herramientas y Tecnologías
Descripción de las herramientas utilizadas para las pruebas de rendimiento (por ejemplo, Apache JMeter, k6, etc.).

#### 3.2.2. Escenarios de Prueba de Rendimiento

```gherkin
Background: 
  Dado que el endpoint "api/v1/electronicbill/{electronicBillId}" está disponible
```

<details open>
  <summary><b><i>Escenario 1:</i></b> Obtener detalles de una factura electrónica con 10 peticiones concurrentes.</summary>
  
  ```gherkin
  Scenario: Obtener detalles de una factura electrónica con 10 peticiones concurrentes
    Given existe una factura electrónica con el ID "12345"
    When se envían 10 peticiones GET concurrentes al endpoint "api/v1/electronicbill/12345"
    Then todas las respuestas deben ser 200 OK
    And el tiempo de respuesta promedio debe ser menor a 300 ms
  ```
  
</details>

<details open>
  <summary><b><i>Escenario 2:</i></b> Obtener detalles de una factura electrónica con 25 peticiones concurrentes.</summary>
  
  ```gherkin
  Escenario: Obtener detalles de una factura electrónica con 25 peticiones concurrentes
    Given existe una factura electrónica con el ID "12345"
    When se envían 25 peticiones GET concurrentes al endpoint "api/v1/electronicbill/12345"
    Then todas las respuestas deben ser 200 OK
    And el tiempo de respuesta promedio debe ser menor a 600 ms
  ```
  
</details>

<details open>
  <summary><b><i>Escenario 3:</i></b> Obtener detalles de una factura electrónica con 50 peticiones concurrentes.</summary>
  
  ```gherkin
  Escenario: Obtener detalles de una factura electrónica con 50 peticiones concurrentes
    Given existe una factura electrónica con el ID "12345"
    When se envían 50 peticiones GET concurrentes al endpoint "api/v1/electronicbill/12345"
    Then todas las respuestas deben ser 200 OK
    And el tiempo de respuesta promedio debe ser menor a 1000 ms
  ```
  
</details>


```gherkin
Background: 
  Dado que el endpoint "api/v1/electronicbill" está disponible
```

<details open>
  <summary><b><i>Escenario 4:</i></b> Verificar la obtención de todas las facturas electrónicas con 10 peticiones concurrentes.</summary>
  
  ```gherkin
  Escenario: Verificar la obtención de todas las facturas electrónicas con 10 peticiones concurrentes
    Given existen varias facturas electrónicas en el sistema
    When se envían 10 peticiones GET concurrentes al endpoint "api/v1/electronicbill"
    Then todas las respuestas deben ser 200 OK
    And el tiempo de respuesta promedio debe ser menor a 300 ms
  ```
  
</details>

<details open>
  <summary><b><i>Escenario 5:</i></b> Verificar la obtención de todas las facturas electrónicas con 25 peticiones concurrentes.</summary>
  
  ```gherkin
  Escenario: Verificar la obtención de todas las facturas electrónicas con 25 peticiones concurrentes
    Given existen varias facturas electrónicas en el sistema
    When se envían 25 peticiones GET concurrentes al endpoint "api/v1/electronicbill"
    Then todas las respuestas deben ser 200 OK
    And el tiempo de respuesta promedio debe ser menor a 600 ms
  ```
  
</details>

<details open>
  <summary><b><i>Escenario 6:</i></b> Verificar la obtención de todas las facturas electrónicas con 50 peticiones concurrentes.</summary>
  
  ```gherkin
  Escenario: Verificar la obtención de todas las facturas electrónicas con 50 peticiones concurrentes
    Given existen varias facturas electrónicas en el sistema
    When se envían 50 peticiones GET concurrentes al endpoint "api/v1/electronicbill"
    Then todas las respuestas deben ser 200 OK
    And el tiempo de respuesta promedio debe ser menor a 1000 ms
  ```
  
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
