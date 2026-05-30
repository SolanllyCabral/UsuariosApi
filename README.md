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

> La base de datos es en memoria. Al iniciar la app se cargan 3 usuarios de prueba automáticamente.

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

## Capturas de pantalla

![GET All](screenshots/1-GetAllUsers.png)
![POST](screenshots/2-CreateUser.png)
![GET BY ID](screenshots/3-GetByIdUser.png)
![GET All](screenshots/4-GetAllNextCreateNewOneUser.png)
![POST](screenshots/5-CreateDuplicateUser.png)
![PUT](screenshots/6-UpdateUser.png)
![DELETE](screenshots/7-DeleteUser.png)
