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

## 4. Conclusiones
Resumen de los hallazgos y mejoras implementadas basadas en las pruebas.
