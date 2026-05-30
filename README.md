# Usuarios API

API REST con ASP.NET Core 8 y Entity Framework Core (base de datos en memoria).

## Estructura del proyecto

```
UsuariosAPI/
├── Context/
│   └── AppDbContext.cs
├── Controllers/
│   └── UsuariosController.cs
├── DTOs/
│   ├── Requests/
│   │   └── UsuarioRequest.cs
│   └── Responses/
│       └── UsuarioResponse.cs
├── Exceptions/
│   └── Exceptions.cs
├── Models/
│   └── Usuario.cs
├── Services/
│   ├── Interfaces/
│   │   └── IUsuarioService.cs
│   └── UsuarioService.cs
└── Program.cs
```

## Ejecutar el proyecto

```bash
dotnet restore
dotnet run
```

Swagger disponible en: `http://localhost:5000/swagger`

> **¿Por qué base de datos en memoria?**
> Se usa `UseInMemoryDatabase` de Entity Framework Core en lugar de una base de datos física (SQL Server, SQLite, etc.). Esto permite que cualquier persona pueda clonar y ejecutar el proyecto sin necesidad de instalar ni configurar ningún motor de base de datos — ideal para pruebas y para que pueda correrlo sin ninguna configuración adicional.
 
> **¿Por qué se cargan datos al iniciar?**
> Como la base de datos en memoria comienza completamente vacía cada vez que la aplicación arranca, se agregaron 3 usuarios de prueba directamente en `Program.cs` usando el `AppDbContext`. Esto ocurre una sola vez al iniciar, antes de que la API empiece a escuchar peticiones, y permite probar todos los endpoints (GET, PUT, DELETE) de inmediato sin tener que crear registros manualmente primero.
 
---

## Endpoints

| Método | Ruta | Descripción | Código éxito |
|--------|------|-------------|--------------|
| GET | `/api/usuarios` | Obtener todos los usuarios | 200 OK |
| GET | `/api/usuarios/{id}` | Obtener usuario por ID | 200 OK |
| POST | `/api/usuarios` | Crear nuevo usuario | 201 Created |
| PUT | `/api/usuarios/{id}` | Actualizar usuario | 200 OK |
| DELETE | `/api/usuarios/{id}` | Eliminar usuario | 200 OK |

## Body para POST y PUT

```json
{
  "nombre": "Juan Pérez",
  "correo": "juan@ejemplo.com",
  "fechaDeNacimiento": "1990-03-22"
}
```

## Códigos de error

| Código | Cuándo ocurre |
|--------|---------------|
| `400 Bad Request` | El correo electrónico ya está en uso |
| `404 Not Found` | El usuario no existe |

---

## Pruebas con Swagger
 
1. Ejecutar `dotnet run`
2. Abrir `http://localhost:5000/swagger` en el navegador
3. La interfaz muestra todos los endpoints disponibles
4. Hacer clic en un endpoint → **Try it out** → completar los datos → **Execute**

## Pruebas con Postman
 
### GET — todos los usuarios
```
GET http://localhost:5000/api/usuarios
```
 
### GET — por ID
```
GET http://localhost:5000/api/usuarios/1
```
 
### POST — crear usuario
```
POST http://localhost:5000/api/usuarios
Content-Type: application/json
 
{
  "nombre": "Juan Pérez",
  "correo": "juan@ejemplo.com",
  "fechaDeNacimiento": "1990-03-22"
}
```
 
### POST — correo duplicado (debe retornar 400)
```
POST http://localhost:5000/api/usuarios
Content-Type: application/json
 
{
  "nombre": "Otro Usuario",
  "correo": "anamartinez@gmail.com",
  "fechaDeNacimiento": "2000-01-01"
}
```
 
### PUT — actualizar usuario
```
PUT http://localhost:5000/api/usuarios/1
Content-Type: application/json
 
{
  "nombre": "Ana Martínez López",
  "correo": "anamartinez@ejemplo.com",
  "fechaDeNacimiento": "1995-04-12"
}
```
 
### DELETE — eliminar usuario
```
DELETE http://localhost:5000/api/usuarios/1
```

---

## Capturas de pantalla

![GET All](screenshots/1-GetAllUsers.png)
![POST](screenshots/2-CreateUser.png)
![GET BY ID](screenshots/3-GetByIdUser.png)
![GET All](screenshots/4-GetAllNextCreateNewOneUser.png)
![POST](screenshots/5-CreateDuplicateUser.png)
![PUT](screenshots/6-UpdateUser.png)
![DELETE](screenshots/7-DeleteUser.png)
