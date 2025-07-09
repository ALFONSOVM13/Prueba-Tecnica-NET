# 📋 Explicación del Proyecto: TravelRequests.Api

## 🎯 Resumen del Proyecto

**TravelRequests.Api** es una API REST desarrollada en **.NET 9.0** para la gestión de solicitudes de viajes corporativos. El proyecto implementa un sistema completo de autenticación, autorización por roles y CRUD de solicitudes de viaje.

---

## 🏗️ Arquitectura del Proyecto

El proyecto sigue los principios de **Clean Architecture**, separando las responsabilidades en 4 capas bien definidas:

### 1. **TravelRequests.Api** (Capa de Presentación)
- **Controllers**: Manejan las peticiones HTTP
- **Middleware**: Manejo global de errores
- **Program.cs**: Configuración de la aplicación (JWT, Swagger, DI)

### 2. **TravelRequests.Application** (Capa de Aplicación)
- **Services**: Lógica de negocio
- **DTOs**: Objetos de transferencia de datos
- **Validators**: Validaciones de entrada
- **Interfaces**: Contratos de servicios

### 3. **TravelRequests.Domain** (Capa de Dominio)
- **Entities**: Modelos del negocio (User, TravelRequest)
- **Enums**: Estados y roles del sistema
- **Interfaces**: Contratos del dominio

### 4. **TravelRequests.Infrastructure** (Capa de Infraestructura)
- **Persistencia**: Entity Framework y configuraciones
- **Authentication**: Implementación de JWT
- **Servicios externos**: Implementaciones concretas

---

## 📊 Modelo de Datos

### **Entidad User**
```csharp
public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public UserRole Role { get; set; }  // Solicitante o Aprobador
    public string? PasswordResetCode { get; set; }
    public DateTime? PasswordResetCodeExpiration { get; set; }
    public virtual ICollection<TravelRequest> TravelRequests { get; set; }
}
```

### **Entidad TravelRequest**
```csharp
public class TravelRequest
{
    public int Id { get; set; }
    public string OriginCity { get; set; }
    public string DestinationCity { get; set; }
    public DateTime DepartureDate { get; set; }
    public DateTime ReturnDate { get; set; }
    public string Justification { get; set; }
    public TravelRequestStatus Status { get; set; }  // Pendiente, Aprobada, Rechazada
    public int UserId { get; set; }
    public virtual User User { get; set; }
}
```

### **Enums del Sistema**
- **UserRole**: `Solicitante`, `Aprobador`
- **TravelRequestStatus**: `Pendiente`, `Aprobada`, `Rechazada`

---

## 🔐 Sistema de Autenticación y Autorización

### **Autenticación JWT**
- Implementación completa con tokens JWT
- Configuración en `Program.cs` con validación de issuer, audience y firma
- Login endpoint que retorna token de acceso

### **Sistema de Roles**
1. **Solicitante**: Puede crear y ver sus propias solicitudes
2. **Aprobador**: Puede ver solicitudes pendientes y cambiar estados

### **Endpoints de Autenticación**
- `POST /api/users/register` - Registro de usuario
- `POST /api/users/login` - Login (retorna JWT token)
- `POST /api/users/password-reset-request` - Solicitud de reset de contraseña
- `POST /api/users/password-reset-confirm` - Confirmación de reset

---

## 🛠️ Endpoints Principales

### **TravelRequestsController** (Requiere autenticación)

| Método | Endpoint | Descripción | Autorización |
|--------|----------|-------------|--------------|
| `POST` | `/api/travelrequests` | Crear solicitud | Autenticado |
| `GET` | `/api/travelrequests/{id}` | Obtener solicitud por ID | Propietario o Aprobador |
| `GET` | `/api/travelrequests/my` | Obtener mis solicitudes | Autenticado |
| `GET` | `/api/travelrequests/pending` | Obtener pendientes | Solo Aprobadores |
| `PUT` | `/api/travelrequests/{id}/status` | Cambiar estado | Solo Admins |

### **UsersController**

| Método | Endpoint | Descripción | Autorización |
|--------|----------|-------------|--------------|
| `POST` | `/api/users/register` | Registro | Público |
| `POST` | `/api/users/login` | Login | Público |
| `GET` | `/api/users` | Listar usuarios | Solo Aprobadores |

---

## 💡 Características Técnicas Destacadas

### **1. Clean Architecture**
- Separación clara de responsabilidades
- Inversión de dependencias
- Testabilidad mejorada

### **2. Seguridad**
- Autenticación JWT robusta
- Autorización basada en roles
- Hash de contraseñas
- Sistema de reset de contraseñas

### **3. Validaciones y Manejo de Errores**
- Middleware global para manejo de errores
- Validaciones en DTOs
- Responses HTTP apropiados

### **4. Documentación**
- Swagger UI integrado
- Documentación automática de endpoints
- Esquemas de autenticación documentados

### **5. Entity Framework**
- Code First approach
- Migraciones para base de datos
- Relaciones entre entidades

---

## 🚀 Tecnologías Utilizadas

- **.NET 9.0**
- **ASP.NET Core Web API**
- **Entity Framework Core**
- **JWT Authentication**
- **Swagger/OpenAPI**
- **SQL Server**

---

## 📝 Preguntas Frecuentes de Entrevista

### **1. ¿Por qué elegiste Clean Architecture?**
"Elegí Clean Architecture porque permite separar las responsabilidades del negocio de los detalles de implementación. Esto hace el código más mantenible, testeable y permite cambiar tecnologías sin afectar la lógica de negocio."

### **2. ¿Cómo manejas la seguridad?**
"Implementé autenticación JWT con validación completa de tokens, autorización basada en roles, hash de contraseñas y un sistema seguro de reset de contraseñas con códigos temporales."

### **3. ¿Cómo estructuraste la base de datos?**
"Utilicé Entity Framework Code First con dos entidades principales: User y TravelRequest, con una relación uno-a-muchos. Los enums manejan estados y roles del sistema."

### **4. ¿Qué patrones de diseño implementaste?**
- **Repository Pattern** (a través de los servicios)
- **Dependency Injection**
- **DTO Pattern** para transferencia de datos
- **Middleware Pattern** para manejo de errores

### **5. ¿Cómo manejas los errores?**
"Implementé un middleware global que captura todas las excepciones, las registra y devuelve respuestas HTTP apropiadas sin exponer detalles internos."

---

## 🎯 Puntos Fuertes del Proyecto

1. **Arquitectura sólida** y escalable
2. **Seguridad robusta** con JWT y roles
3. **Código limpio** y bien organizado
4. **Documentación automática** con Swagger
5. **Manejo profesional de errores**
6. **Separación clara de responsabilidades**
7. **Facilidad de testing** por la arquitectura

---

## 🔧 Posibles Mejoras que Podrían Preguntar

1. **Logging**: Implementar logging estructurado
2. **Caching**: Agregar cache para consultas frecuentes
3. **Paginación**: Para endpoints que retornan listas
4. **Rate Limiting**: Control de frecuencia de requests
5. **Unit Tests**: Implementar testing automatizado
6. **Health Checks**: Monitoreo de salud de la API

---

## 🧪 Cómo Probar la API

### **1. Ejecutar el Proyecto**
```bash
cd src/TravelRequests.Api
dotnet run
```
La API estará disponible en: `http://localhost:5000`

### **2. Documentación Swagger**
Acceder a: `http://localhost:5000/swagger`
- Interfaz gráfica para probar endpoints
- Documentación automática de esquemas
- Autenticación integrada con JWT

### **3. Flujo de Pruebas Típico**
1. **Registrar usuario**: `POST /api/users/register`
2. **Hacer login**: `POST /api/users/login` (obtener token)
3. **Crear solicitud**: `POST /api/travelrequests` (con token)
4. **Ver mis solicitudes**: `GET /api/travelrequests/my`
5. **Aprobar/Rechazar**: `PUT /api/travelrequests/{id}/status` (rol Aprobador)

---

## 💼 Consejos para la Entrevista

### **Demuestra tu Conocimiento Técnico**
- Explica la separación de capas y por qué es importante
- Menciona cómo Entity Framework maneja las relaciones
- Habla sobre la configuración del middleware de JWT

### **Destaca las Mejores Prácticas**
- Uso de DTOs para separar el modelo interno de la API
- Validaciones en múltiples capas
- Manejo apropiado de códigos de estado HTTP
- Documentación automática con Swagger

### **Prepárate para Preguntas de Escalabilidad**
- Cómo agregarías cache (Redis)
- Implementación de logging (Serilog)
- Estrategias de testing (Unit, Integration)
- Deployment en contenedores (Docker)

### **Conoce las Limitaciones Actuales**
- Falta de paginación en listados
- No hay rate limiting
- Podrían faltar más validaciones de negocio
- No hay auditoría de cambios

---

## 🎭 Posibles Preguntas Técnicas

### **Arquitectura y Patrones**
- "¿Por qué separaste en 4 proyectos?"
- "¿Qué patrón usaste para la inyección de dependencias?"
- "¿Cómo implementarías el patrón Repository?"

### **Seguridad**
- "¿Cómo validas los tokens JWT?"
- "¿Qué información incluyes en el payload del token?"
- "¿Cómo manejas la expiración de tokens?"

### **Base de Datos**
- "¿Cómo configuraste las relaciones en Entity Framework?"
- "¿Qué son las migraciones y cómo las usaste?"
- "¿Cómo optimizarías las consultas?"

### **Testing y Calidad**
- "¿Cómo testearías los controllers?"
- "¿Qué herramientas usarías para análisis de código?"
- "¿Cómo implementarías integración continua?"

---

¡Este proyecto demuestra conocimientos sólidos en .NET, arquitectura limpia, seguridad y mejores prácticas de desarrollo! Estás bien preparado para la entrevista técnica. 🚀