# TravelRequests.Api

API de gestión de solicitudes de viajes desarrollada con .NET 9.0.

## Requisitos Previos

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download)
- SQL Server (LocalDB o instancia completa)
- Un IDE compatible con .NET (Visual Studio 2022, VS Code, Rider)

## Configuración del Proyecto

1. Clonar el repositorio:
```bash
git clone [url-del-repositorio]
cd TravelRequests.Api
```

2. Restaurar las dependencias:
```bash
dotnet restore
```

3. Aplicar las migraciones de la base de datos:
```bash
cd src/TravelRequests.Api
dotnet ef database update
```

## Ejecución del Proyecto

1. Navegar al directorio del proyecto API:
```bash
cd src/TravelRequests.Api
```

2. Ejecutar el proyecto:
```bash
dotnet run
```

La API estará disponible en: http://localhost:5000

## Estructura del Proyecto

- **TravelRequests.Api**: Proyecto principal de la API
- **TravelRequests.Application**: Lógica de negocio y DTOs
- **TravelRequests.Domain**: Entidades y interfaces del dominio
- **TravelRequests.Infrastructure**: Implementaciones de persistencia y servicios

## Documentación de la API

La documentación completa de la API está disponible a través de Swagger UI en:
http://localhost:5000/swagger

## Pruebas de API

El archivo `TravelRequests.Api.http` contiene ejemplos de todas las peticiones HTTP disponibles. Puede ser utilizado con la extensión REST Client de VS Code o importado a Postman.

## Notas Adicionales

- Asegúrese de tener las variables de entorno correctamente configuradas en el archivo `appsettings.json`
- Para las peticiones autenticadas, se requiere incluir el token JWT en el header `Authorization: Bearer {token}`
- El token se obtiene al hacer login en el sistema # Prueba-Tecnica-NET
