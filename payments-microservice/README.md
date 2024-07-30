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

```gherkin
Background: 
  Dado que el endpoint "api/v1/electronicbill/{electronicBillId}" está disponible
```

<details open>
  <summary><b><i>Escenario 1:</i></b> Verificar la creación de un pago.</summary>
  
  ```gherkin
  Scenario: Verificación de la creación de un pago
    Given el endpoint "api/payments/" está disponible
    When se envía una solicitud POST con los datos del pago
    Then la respuesta debe ser 201 Created
    And el pago debe estar registrado en el sistema
  ```
</details>

<details open>
  <summary><b><i>Escenario 2:</i></b> Verificar la obtención de detalles de un pago.</summary>
  
  ```gherkin
  Scenario: Verificar la obtención de detalles de un pago
    Given el endpoint "api/payments/{id}" está disponible
    When se envía una solicitud GET con un ID de pago válido
    Then la respuesta debe ser 200 OK
    And los detalles del pago deben ser correctos
  ```
</details>

```gherkin
Background: 
  Dado que el endpoint "endpoint aqui" está disponible
```

<details open>
  <summary><b><i>Escenario 3:</i></b> Verificar la creación de un pago.</summary>
  
  ```gherkin
  Scenario: Verificación de la creación de un pago
    Given el endpoint "api/payments/" está disponible
    When se envía una solicitud POST con los datos del pago
    Then la respuesta debe ser 201 Created
    And el pago debe estar registrado en el sistema
  ```
</details>

---

### 3.2. Pruebas de Rendimiento

#### 3.2.1. Herramientas y Tecnologías

**Apache JMeter** es una herramienta de código abierto ampliamente utilizada para realizar pruebas de rendimiento y carga en aplicaciones. Diseñada para evaluar el rendimiento de servicios web y aplicaciones en una variedad de protocolos, JMeter permite simular múltiples usuarios concurrentes para medir el comportamiento del sistema bajo diferentes cargas. Con su interfaz gráfica intuitiva, JMeter facilita la creación de planes de prueba personalizados, la definición de escenarios de carga y la configuración de métricas detalladas. Esta herramienta también ofrece capacidades para generar reportes detallados y gráficos, proporcionando una visión integral del rendimiento del sistema y ayudando a identificar cuellos de botella y áreas de mejora. Su flexibilidad y extensibilidad la convierten en una opción ideal para evaluar la capacidad de respuesta y la estabilidad de aplicaciones en entornos de producción.


#### 3.2.2. Escenarios de Prueba de Rendimiento

```gherkin
Background: 
  Dado que el endpoint "api/v1/electronicbill/{electronicBillId}" está disponible
```

<details open>
  <summary><b><i>Escenario 1:</i></b> Obtener detalles de una factura electrónica con 10 peticiones simultáneas.</summary>
  
  ```gherkin
  Scenario: Obtener detalles de una factura electrónica con 10 peticiones simultáneas
    Given existe una factura electrónica con el ID "12345"
    When se envían 10 peticiones GET simultáneas al endpoint "api/v1/electronicbill/12345"
    Then todas las respuestas deben ser 200 OK
    And el tiempo de respuesta promedio debe ser menor a 300 ms
  ```
  
</details>

<details open>
  <summary><b><i>Escenario 2:</i></b> Obtener detalles de una factura electrónica con 25 peticiones simultáneas.</summary>
  
  ```gherkin
  Scenario: Obtener detalles de una factura electrónica con 25 peticiones simultáneas
    Given existe una factura electrónica con el ID "12345"
    When se envían 25 peticiones GET simultáneas al endpoint "api/v1/electronicbill/12345"
    Then todas las respuestas deben ser 200 OK
    And el tiempo de respuesta promedio debe ser menor a 600 ms
  ```
  
</details>

<details open>
  <summary><b><i>Escenario 3:</i></b> Obtener detalles de una factura electrónica con 50 peticiones simultáneas.</summary>
  
  ```gherkin
  Scenario: Obtener detalles de una factura electrónica con 50 peticiones simultáneas
    Given existe una factura electrónica con el ID "12345"
    When se envían 50 peticiones GET simultáneas al endpoint "api/v1/electronicbill/12345"
    Then todas las respuestas deben ser 200 OK
    And el tiempo de respuesta promedio debe ser menor a 1000 ms
  ```
  
</details>


```gherkin
Background: 
  Dado que el endpoint "api/v1/electronicbill" está disponible
```

<details open>
  <summary><b><i>Escenario 4:</i></b> Verificar la obtención de todas las facturas electrónicas con 10 peticiones simultáneas.</summary>
  
  ```gherkin
  Scenario: Verificar la obtención de todas las facturas electrónicas con 10 peticiones simultáneas
    Given existen varias facturas electrónicas en el sistema
    When se envían 10 peticiones GET simultáneas al endpoint "api/v1/electronicbill"
    Then todas las respuestas deben ser 200 OK
    And el tiempo de respuesta promedio debe ser menor a 300 ms
  ```
  
</details>

<details open>
  <summary><b><i>Escenario 5:</i></b> Verificar la obtención de todas las facturas electrónicas con 25 peticiones simultáneas.</summary>
  
  ```gherkin
  Scenario: Verificar la obtención de todas las facturas electrónicas con 25 peticiones simultáneas
    Given existen varias facturas electrónicas en el sistema
    When se envían 25 peticiones GET simultáneas al endpoint "api/v1/electronicbill"
    Then todas las respuestas deben ser 200 OK
    And el tiempo de respuesta promedio debe ser menor a 600 ms
  ```
  
</details>

<details open>
  <summary><b><i>Escenario 6:</i></b> Verificar la obtención de todas las facturas electrónicas con 50 peticiones simultáneas.</summary>
  
  ```gherkin
  Scenario: Verificar la obtención de todas las facturas electrónicas con 50 peticiones simultáneas
    Given existen varias facturas electrónicas en el sistema
    When se envían 50 peticiones GET simultáneas al endpoint "api/v1/electronicbill"
    Then todas las respuestas deben ser 200 OK
    And el tiempo de respuesta promedio debe ser menor a 1000 ms
  ```
  
</details>

```gherkin
Background: 
  Dado que el endpoint "api/v1/payer" está disponible
```

<details open>
  <summary><b><i>Escenario 7:</i></b> Verificación de la obtención de todos los pagantes con 10 peticiones simultáneas.</summary>
  
  ```gherkin
  Scenario: Verificación de la obtención de todos los pagantes con 10 peticiones simultáneas
    Given existen varios pagantes en el sistema
    When se envían 10 peticiones GET simultáneas al endpoint "api/v1/payer"
    Then todas las respuestas deben ser 200 OK
    And el tiempo de respuesta promedio debe ser menor a 200 ms
  ```
  
</details>

<details open>
  <summary><b><i>Escenario 8:</i></b> Verificación de la obtención de todos los pagantes con 25 peticiones simultáneas.</summary>
  
  ```gherkin
  Scenario: Verificación de la obtención de todos los pagantes con 25 peticiones simultáneas
    Given existen varios pagantes en el sistema
    When se envían 25 peticiones GET simultáneas al endpoint "api/v1/payer"
    Then todas las respuestas deben ser 200 OK
    And el tiempo de respuesta promedio debe ser menor a 200 ms
  ```
  
</details>

<details open>
  <summary><b><i>Escenario 9:</i></b> Verificación de la obtención de todos los pagantes con 50 peticiones simultáneas.</summary>
  
  ```gherkin
  Scenario: Verificación de la obtención de todos los pagantes con 50 peticiones simultáneas
    Given existen varios pagantes en el sistema
    When se envían 50 peticiones GET simultáneas al endpoint "api/v1/payer"
    Then todas las respuestas deben ser 200 OK
    And el tiempo de respuesta promedio debe ser menor a 200 ms
  ```
  
</details>

```gherkin
Background: 
  Dado que el endpoint "api/v1/payer/12345" está disponible
```

<details open>
  <summary><b><i>Escenario 10:</i></b> Verificación de la obtención de un pagante por ID con 10 peticiones simultáneas.</summary>
  
  ```gherkin
  Scenario: Verificación de la obtención de un pagante por ID con 10 peticiones simultáneas
    Given existe un pagante con el ID "12345"
    When se envían 10 peticiones GET simultáneas al endpoint "api/v1/payer/12345"
    Then todas las respuestas deben ser 200 OK
    And el tiempo de respuesta promedio debe ser menor a 200 ms
  ```
  
</details>

<details open>
  <summary><b><i>Escenario 11:</i></b> Verificación de la obtención de un pagante por ID con 25 peticiones simultáneas.</summary>
  
  ```gherkin
  Scenario: Verificación de la obtención de un pagante por ID con 25 peticiones simultáneas
    Given existe un pagante con el ID "12345"
    When se envían 25 peticiones GET simultáneas al endpoint "api/v1/payer/12345"
    Then todas las respuestas deben ser 200 OK
    And el tiempo de respuesta promedio debe ser menor a 200 ms
  ```
  
</details>

<details open>
  <summary><b><i>Escenario 12:</i></b> Verificación de la obtención de un pagante por ID con 50 peticiones simultáneas.</summary>
  
  ```gherkin
  Scenario: Verificación de la obtención de un pagante por ID con 50 peticiones simultáneas
    Given existe un pagante con el ID "12345"
    When se envían 50 peticiones GET simultáneas al endpoint "api/v1/payer/12345"
    Then todas las respuestas deben ser 200 OK
    And el tiempo de respuesta promedio debe ser menor a 200 ms
  ```
  
</details>


```gherkin
Background: 
  Dado que el endpoint "api/v1/payment" está disponible
```

<details open>
  <summary><b><i>Escenario 13:</i></b> Verificación de la obtención de un pago por ID con 10 peticiones simultáneas.</summary>
  
  ```gherkin
  Scenario: Verificación de la obtención de un pago por ID con 10 peticiones simultáneas
    Given existe un pago con el ID "12345"
    When se envían 10 peticiones GET simultáneas al endpoint "api/v1/payment/12345"
    Then todas las respuestas deben ser 200 OK
    And el tiempo de respuesta promedio debe ser menor a 200 ms
  ```
  
</details>

<details open>
  <summary><b><i>Escenario 14:</i></b> Verificación de la obtención de un pago por ID con 25 peticiones simultáneas.</summary>
  
  ```gherkin
  Scenario: Verificación de la obtención de un pago por ID con 25 peticiones simultáneas
    Given existe un pago con el ID "12345"
    When se envían 25 peticiones GET simultáneas al endpoint "api/v1/payment/12345"
    Then todas las respuestas deben ser 200 OK
    And el tiempo de respuesta promedio debe ser menor a 200 ms
  ```
  
</details>

<details open>
  <summary><b><i>Escenario 15:</i></b> Verificación de la obtención de un pago por ID con 50 peticiones simultáneas.</summary>
  
  ```gherkin
  Scenario: Verificación de la obtención de un pago por ID con 50 peticiones simultáneas
    Given existe un pago con el ID "12345"
    When se envían 50 peticiones GET simultáneas al endpoint "api/v1/payment/12345"
    Then todas las respuestas deben ser 200 OK
    And el tiempo de respuesta promedio debe ser menor a 200 ms
  ```

</details>





### 3.3. Pruebas de Seguridad

#### 3.3.1. Herramientas y Tecnologías

**OWASP ZAP (Zed Attack Proxy)** es una herramienta de código abierto diseñada para realizar pruebas de seguridad en aplicaciones web. Desarrollada por el Open Web Application Security Project (OWASP), ZAP proporciona una amplia gama de funciones para identificar vulnerabilidades y evaluar la seguridad de aplicaciones durante el ciclo de desarrollo. Su interfaz intuitiva permite a los usuarios realizar escaneos automatizados, así como llevar a cabo pruebas manuales de seguridad, como la exploración de aplicaciones y la identificación de puntos débiles. ZAP es particularmente útil para detectar problemas de seguridad comunes, como inyecciones SQL, ataques de cross-site scripting (XSS) y configuraciones incorrectas. Además, ZAP ofrece soporte para integraciones con otras herramientas de desarrollo y pruebas, lo que facilita la inclusión de prácticas de seguridad en el proceso de desarrollo ágil. Su capacidad para generar reportes detallados ayuda a los equipos de desarrollo a abordar las vulnerabilidades de manera efectiva y mejorar la seguridad general de sus aplicaciones.

#### 3.3.2. Escenarios de Prueba de Seguridad

```gherkin
Background:
    Given que el endpoint "http://localhost:8007" está accesible
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
  <img src="resources/securitytest_scanning_report.png" alt="Performance Test" />
</p>


## 4. Referencias

[1] W. by Iamprovidence, **“Backend side architecture evolution (N-layered, DDD, Hexagon, Onion, Clean Architecture)”**, Medium, 27-jun-2023. [En línea]. Disponible en: [https://medium.com/@iamprovidence/backend-side-architecture-evolution-n-layered-ddd-hexagon-onion-clean-architecture-643d72444ce4](https://medium.com/@iamprovidence/backend-side-architecture-evolution-n-layered-ddd-hexagon-onion-clean-architecture-643d72444ce4).

[2] C. Ramalingam, **“Building domain driven microservices - Walmart global tech blog - medium”**, Walmart Global Tech Blog, 01-jul-2020. [En línea]. Disponible en: [https://medium.com/walmartglobaltech/building-domain-driven-microservices-af688aa1b1b8](https://medium.com/walmartglobaltech/building-domain-driven-microservices-af688aa1b1b8).

[3] J. Loscalzo, **“Domain Driven Design: principios, beneficios y elementos — Segunda Parte”**, Medium, 18-jun-2018. [En línea]. Disponible en: [https://medium.com/@jonathanloscalzo/domain-driven-design-principios-beneficios-y-elementos-segunda-parte-337d77dc8566](https://medium.com/@jonathanloscalzo/domain-driven-design-principios-beneficios-y-elementos-segunda-parte-337d77dc8566).

[4] P. Martinez, **“Domain-Driven Design: Everything you always wanted to know”**, SSENSE-TECH, 15-may-2020. [En línea]. Disponible en: [https://medium.com/ssense-tech/domain-driven-design-everything-you-always-wanted-to-know-about-it-but-were-afraid-to-ask-a85e7b74497a](https://medium.com/ssense-tech/domain-driven-design-everything-you-always-wanted-to-know-about-it-but-were-afraid-to-ask-a85e7b74497a).