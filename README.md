# .NET-Aspire

## 🚀 ¿Qué es .NET Aspire?

.NET Aspire es una herramienta moderna desarrollada por Microsoft para facilitar la **creación, orquestación y monitoreo** de aplicaciones distribuidas. Permite levantar múltiples microservicios con sus dependencias desde un solo proyecto controlador (**AppHost**) y proporciona un **dashboard visual interactivo** para gestionar y observar cada componente.

Aspire elimina la necesidad de configuraciones complicadas con contenedores o scripts de inicio, centralizando toda la experiencia en una interfaz sencilla y automatizada.

---

## 📅 Estructura del Proyecto Implementado

En esta investigación, se desarrolló una solución distribuida con los siguientes microservicios:

| Servicio          | Responsabilidad principal                          |
|------------------|-----------------------------------------------------|
| `apiservice`      | Servicio central de lógica de negocio              |
| `authservice`     | Autenticación y autorización                       |
| `ordersservice`   | Registro y validación de órdenes de productos       |
| `inventoryservice`| Simulación de stock e inventario                  |

Todos los servicios están orquestados mediante `Orchestralia.AppHost`, el cual define la topología del sistema, los puertos, las dependencias entre servicios y sus endpoints.

---

## 💻 Funcionamiento del programa

1. El usuario hace una petición al **`ordersservice`** para crear una orden.
2. Este servicio consulta a **`inventoryservice`** si hay stock disponible.
3. Si hay suficiente stock:
   - Se registra la orden localmente.
   - Se retorna un ID de confirmación.
4. Si **no hay stock**, se responde con un mensaje de error.

Ejemplo:
```json
POST /orders
{
  "product": "laptop",
  "quantity": 2
}
```

Respuestas esperadas:
- Si hay stock: `201 Created`
- Si no hay stock: `400 BadRequest - "Stock insuficiente"`

---

## 🖹 Visualización del programa en Aspire

Al ejecutar `dotnet run` desde el proyecto `AppHost`, se despliega un dashboard interactivo con los siguientes elementos:

### 📚 Panel lateral izquierdo

| Elemento       | Funcionalidad                                                            |
|----------------|--------------------------------------------------------------------------|
| Recursos       | Lista de todos los servicios levantados con sus puertos y estados        |
| Consola        | Visualización de logs individuales por servicio                          |
| Estructurado   | Vista agrupada jerárquicamente                                           |
| Seguimientos   | Para integrar con OpenTelemetry o trazas de peticiones                   |
| Métricas       | Tiempos de inicio, puertos ocupados, estado de cada recurso              |

### 🔗 Tabla principal de recursos

| Columna             | Descripción                                                                 |
|---------------------|------------------------------------------------------------------------------|
| Tipo                | Tipo de recurso: proyecto, contenedor, servicio                              |
| Nombre              | Nombre definido en `AddProject<>()`                                          |
| Estado              | Estado de ejecución (Running, Error)                                         |
| Hora de inicio      | Cuándo se inició el recurso                                                 |
| Origen              | Ruta del archivo `.csproj` del servicio                                      |
| Puntos de conexión  | Puertos accesibles (ej: https://localhost:7096)                              |
| Actions             | Acciones rápidas: ver consola, logs, reiniciar, abrir en navegador           |

---

## 📈 GitHub Actions para pruebas y despliegue

Se configuraron dos workflows en GitHub para automatizar procesos:

1. **Pruebas automatizadas de órdenes**: verifica que el sistema rechace correctamente las órdenes no válidas y acepte las válidas.
2. **Simulación de deploy**: realiza la compilación y muestra un mensaje si el sistema está listo para ser desplegado.

Para conocer más sobre cómo crear y usar GitHub Actions en general, consulta el siguiente repositorio de aprendizaje:
**[Repositorio Actions_Repo](https://github.com/Danielammmm/Actions_Repo)**

> Nota: Este repositorio es para aprender a hacer GitHub Actions, no contiene los workflows específicos usados en este proyecto.

---

## 🚀 Conclusión 

.NET Aspire facilita el desarrollo de arquitecturas distribuidas, ofreciendo visibilidad completa, configuraciones mínimas y una experiencia de desarrollo local fluida. Su integración con herramientas modernas como GitHub Actions y su soporte para servicios modulares lo convierten en una opción ideal para equipos que buscan profesionalizar su ciclo de desarrollo.

---

