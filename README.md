# Usuarios API

API REST con ASP.NET Core 10 y Entity Framework Core utilizando base de datos en memoria.
El proyecto incluye autenticación JWT, CRUD de usuarios, CRUD de categorías, CRUD de proveedores, CRUD de productos y endpoints de agregación sobre productos.

---

## Descripción general

Este proyecto fue desarrollado como una API REST utilizando ASP.NET Core y Entity Framework Core con enfoque Code First.

La API permite:

* Gestionar usuarios.
* Autenticar usuarios mediante JWT.
* Refrescar tokens.
* Gestionar categorías.
* Gestionar proveedores.
* Gestionar productos.
* Relacionar productos con proveedores y categorías.
* Consultar información agregada de productos usando LINQ.

---

## Estructura del proyecto

```txt
UsuariosApi/
├── Context/
│   └── AppDbContext.cs
│
├── Controllers/
│   ├── AuthController.cs
│   ├── CategoriasController.cs
│   ├── ProductosController.cs
│   ├── ProveedoresController.cs
│   └── UsuariosController.cs
│
├── DTOs/
│   ├── Requests/
│   │   ├── AuthRequest.cs
│   │   ├── CategoriaRequest.cs
│   │   ├── ProductoRequest.cs
│   │   ├── ProveedorRequest.cs
│   │   └── UsuarioRequest.cs
│   │
│   └── Responses/
│       ├── AuthResponse.cs
│       ├── CategoriaResponse.cs
│       ├── ProductoResponse.cs
│       ├── ProductoResumenResponse.cs
│       ├── ProveedorResponse.cs
│       └── UsuarioResponse.cs
│
├── Exceptions/
│   └── Exceptions.cs
│
├── Models/
│   ├── Categoria.cs
│   ├── Producto.cs
│   ├── Proveedor.cs
│   ├── Usuario.cs
│   └── UsuarioAuth.cs
│
├── Services/
│   ├── Interfaces/
│   │   ├── IAuthService.cs
│   │   ├── ICategoriaService.cs
│   │   ├── IProductoService.cs
│   │   ├── IProveedorService.cs
│   │   └── IUsuarioService.cs
│   │
│   ├── AuthService.cs
│   ├── CategoriaService.cs
│   ├── ProductoService.cs
│   ├── ProveedorService.cs
│   └── UsuarioService.cs
│
├── Program.cs
└── appsettings.json
```

---

## Arquitectura utilizada

El proyecto está organizado por capas simples:

```txt
Controller → Service → DbContext
```

### Controllers

Reciben las peticiones HTTP y devuelven las respuestas correspondientes.

### Services

Contienen la lógica principal del negocio, validaciones y operaciones contra la base de datos.

### Interfaces

Definen los contratos de los servicios.

### DTOs

Separan los datos que recibe y devuelve la API.

* `Requests`: datos que llegan desde Swagger, Postman o un cliente externo.
* `Responses`: datos que devuelve la API.

### Models

Representan las entidades principales del sistema y sus relaciones.

---

## Ejecutar el proyecto

```bash
dotnet restore
dotnet run
```

Swagger disponible en:

```txt
https://localhost:7002/swagger
```

---

## Base de datos en memoria

Se usa `UseInMemoryDatabase` de Entity Framework Core en lugar de una base de datos física como SQL Server u otro motor.

Esto permite que cualquier persona pueda clonar y ejecutar el proyecto sin necesidad de instalar ni configurar una base de datos externa.

---

## Datos iniciales

Como la base de datos en memoria se reinicia cada vez que la aplicación arranca, se agregaron datos iniciales directamente en `Program.cs`.

Estos datos permiten probar los endpoints inmediatamente.

Se cargan datos de prueba para:

* Usuarios.
* Usuario de autenticación.
* Categorías.
* Proveedores.
* Productos.

---

## Credenciales de prueba

| Campo    | Valor      |
| -------- | ---------- |
| username | `admin`    |
| password | `admin123` |

---

## Autenticación JWT

La API utiliza autenticación mediante JSON Web Tokens.

Para acceder a los endpoints protegidos, primero se debe iniciar sesión y obtener un token JWT.

Luego el token debe enviarse en las peticiones protegidas mediante el encabezado:

```txt
Authorization: Bearer {token}
```

---

## Endpoints

## Autenticación

Estos endpoints son públicos.

| Método | Ruta                                          | Descripción                              |
| ------ | --------------------------------------------- | ---------------------------------------- |
| GET    | `/api/Auth/generarContrasena?pass={password}` | Genera el hash SHA-256 de una contraseña |
| POST   | `/api/Auth/login`                             | Iniciar sesión y obtener token JWT       |
| POST   | `/api/Auth/refresh`                           | Renovar el token JWT                     |

---

## Usuarios

Estos endpoints requieren token JWT.

| Método | Ruta                 | Descripción                | Código éxito |
| ------ | -------------------- | -------------------------- | ------------ |
| GET    | `/api/Usuarios`      | Obtener todos los usuarios | 200 OK       |
| GET    | `/api/Usuarios/{id}` | Obtener usuario por ID     | 200 OK       |
| POST   | `/api/Usuarios`      | Crear nuevo usuario        | 201 Created  |
| PUT    | `/api/Usuarios/{id}` | Actualizar usuario         | 200 OK       |
| DELETE | `/api/Usuarios/{id}` | Eliminar usuario           | 200 OK       |

---

## Categorías

Estos endpoints requieren token JWT.

| Método | Ruta                   | Descripción                  | Código éxito |
| ------ | ---------------------- | ---------------------------- | ------------ |
| GET    | `/api/Categorias`      | Obtener todas las categorías | 200 OK       |
| GET    | `/api/Categorias/{id}` | Obtener categoría por ID     | 200 OK       |
| POST   | `/api/Categorias`      | Crear nueva categoría        | 201 Created  |
| PUT    | `/api/Categorias/{id}` | Actualizar categoría         | 200 OK       |
| DELETE | `/api/Categorias/{id}` | Eliminar categoría           | 200 OK       |

---

## Proveedores

Estos endpoints requieren token JWT.

| Método | Ruta                    | Descripción                   | Código éxito |
| ------ | ----------------------- | ----------------------------- | ------------ |
| GET    | `/api/Proveedores`      | Obtener todos los proveedores | 200 OK       |
| GET    | `/api/Proveedores/{id}` | Obtener proveedor por ID      | 200 OK       |
| POST   | `/api/Proveedores`      | Crear nuevo proveedor         | 201 Created  |
| PUT    | `/api/Proveedores/{id}` | Actualizar proveedor          | 200 OK       |
| DELETE | `/api/Proveedores/{id}` | Eliminar proveedor            | 200 OK       |

---

## Productos

Estos endpoints requieren token JWT.

| Método | Ruta                  | Descripción                 | Código éxito |
| ------ | --------------------- | --------------------------- | ------------ |
| GET    | `/api/Productos`      | Obtener todos los productos | 200 OK       |
| GET    | `/api/Productos/{id}` | Obtener producto por ID     | 200 OK       |
| POST   | `/api/Productos`      | Crear nuevo producto        | 201 Created  |
| PUT    | `/api/Productos/{id}` | Actualizar producto         | 200 OK       |
| DELETE | `/api/Productos/{id}` | Eliminar producto           | 200 OK       |

---

## Consultas y agregaciones de productos

Estos endpoints requieren token JWT.

| Método | Ruta                                     | Descripción                                                                                                     |
| ------ | ---------------------------------------- | --------------------------------------------------------------------------------------------------------------- |
| GET    | `/api/Productos/resumen`                 | Devuelve el producto con precio más alto, producto con precio más bajo, suma total de precios y precio promedio |
| GET    | `/api/Productos/categoria/{idCategoria}` | Devuelve los productos de una categoría específica                                                              |
| GET    | `/api/Productos/proveedor/{idProveedor}` | Devuelve los productos de un proveedor específico                                                               |
| GET    | `/api/Productos/total`                   | Devuelve la cantidad total de productos registrados                                                             |

---

## Relaciones entre entidades

Se agregaron las siguientes entidades:

* Producto.
* Proveedor.
* Categoría.

Las relaciones definidas son:

* Un producto pertenece a una categoría.
* Un producto tiene un proveedor.
* Un proveedor puede proporcionar varios productos.
* Una categoría puede tener varios productos.

Representación general:

```txt
Proveedor 1 ---- * Producto * ---- 1 Categoría
```

---

## Modelos principales

## Usuario

| Campo             | Tipo     | Descripción                     |
| ----------------- | -------- | ------------------------------- |
| Id                | int      | Identificador único del usuario |
| Nombre            | string   | Nombre del usuario              |
| Correo            | string   | Correo electrónico del usuario  |
| FechaDeNacimiento | DateTime | Fecha de nacimiento del usuario |

---

## UsuarioAuth

| Campo                  | Tipo     | Descripción                           |
| ---------------------- | -------- | ------------------------------------- |
| Id                     | int      | Identificador único                   |
| Username               | string   | Nombre de usuario para autenticación  |
| PasswordHash           | string   | Contraseña encriptada con SHA-256     |
| RefreshToken           | string   | Token para renovar autenticación      |
| RefreshTokenExpiration | DateTime | Fecha de expiración del refresh token |

---

## Categoría

| Campo  | Tipo   | Descripción                         |
| ------ | ------ | ----------------------------------- |
| Id     | int    | Identificador único de la categoría |
| Nombre | string | Nombre de la categoría              |

---

## Proveedor

| Campo    | Tipo   | Descripción                           |
| -------- | ------ | ------------------------------------- |
| Id       | int    | Identificador único del proveedor     |
| Nombre   | string | Nombre del proveedor                  |
| Contacto | string | Información de contacto del proveedor |

---

## Producto

| Campo       | Tipo    | Descripción                      |
| ----------- | ------- | -------------------------------- |
| Id          | int     | Identificador único del producto |
| Nombre      | string  | Nombre del producto              |
| Precio      | decimal | Precio del producto              |
| Stock       | int     | Cantidad disponible              |
| IdProveedor | int     | Identificador del proveedor      |
| IdCategoria | int     | Identificador de la categoría    |

---

## Códigos de respuesta

| Código             | Cuándo ocurre                                                            |
| ------------------ | ------------------------------------------------------------------------ |
| `200 OK`           | Operación exitosa                                                        |
| `201 Created`      | Registro creado correctamente                                            |
| `400 Bad Request`  | Datos inválidos, correo duplicado, entidad duplicada o relación inválida |
| `401 Unauthorized` | Sin token, token inválido o credenciales incorrectas                     |
| `404 Not Found`    | El registro solicitado no existe                                         |

---

# Cómo probar con Postman

## Paso 1 — Login

```http
POST https://localhost:7002/api/Auth/login
Content-Type: application/json
```

Body:

```json
{
  "username": "admin",
  "password": "admin123"
}
```

Guarda el `token` y el `refreshToken` de la respuesta.

---

## Paso 2 — Usar el token

En cada petición protegida, ir a:

```txt
Authorization → Bearer Token
```

Luego pegar el token recibido.

---

# Pruebas de usuarios

## GET — todos los usuarios

```http
GET https://localhost:7002/api/Usuarios
Authorization: Bearer {token}
```

---

## GET — usuario por ID

```http
GET https://localhost:7002/api/Usuarios/1
Authorization: Bearer {token}
```

---

## POST — crear usuario

```http
POST https://localhost:7002/api/Usuarios
Authorization: Bearer {token}
Content-Type: application/json
```

Body:

```json
{
  "nombre": "Juan Pérez",
  "correo": "juanperez@gmail.com",
  "fechaDeNacimiento": "1990-03-22"
}
```

---

## POST — correo duplicado

Debe retornar `400 Bad Request`.

```http
POST https://localhost:7002/api/Usuarios
Authorization: Bearer {token}
Content-Type: application/json
```

Body:

```json
{
  "nombre": "Otro Usuario",
  "correo": "anamartinez@gmail.com",
  "fechaDeNacimiento": "2000-01-01"
}
```

---

## PUT — actualizar usuario

```http
PUT https://localhost:7002/api/Usuarios/1
Authorization: Bearer {token}
Content-Type: application/json
```

Body:

```json
{
  "nombre": "Ana Martínez López",
  "correo": "anamartinez@gmail.com",
  "fechaDeNacimiento": "1995-04-12"
}
```

---

## DELETE — eliminar usuario

```http
DELETE https://localhost:7002/api/Usuarios/3
Authorization: Bearer {token}
```

---

# Pruebas de categorías

## GET — todas las categorías

```http
GET https://localhost:7002/api/Categorias
Authorization: Bearer {token}
```

---

## GET — categoría por ID

```http
GET https://localhost:7002/api/Categorias/1
Authorization: Bearer {token}
```

---

## POST — crear categoría

```http
POST https://localhost:7002/api/Categorias
Authorization: Bearer {token}
Content-Type: application/json
```

Body:

```json
{
  "nombre": "Hogar"
}
```

---

## PUT — actualizar categoría

```http
PUT https://localhost:7002/api/Categorias/1
Authorization: Bearer {token}
Content-Type: application/json
```

Body:

```json
{
  "nombre": "Electrónica Actualizada"
}
```

---

## DELETE — eliminar categoría

```http
DELETE https://localhost:7002/api/Categorias/3
Authorization: Bearer {token}
```

Nota: no se puede eliminar una categoría que tenga productos asociados.

---

# Pruebas de proveedores

## GET — todos los proveedores

```http
GET https://localhost:7002/api/Proveedores
Authorization: Bearer {token}
```

---

## GET — proveedor por ID

```http
GET https://localhost:7002/api/Proveedores/1
Authorization: Bearer {token}
```

---

## POST — crear proveedor

```http
POST https://localhost:7002/api/Proveedores
Authorization: Bearer {token}
Content-Type: application/json
```

Body:

```json
{
  "nombre": "CompuGlobal",
  "contacto": "compuglobal@email.com"
}
```

---

## PUT — actualizar proveedor

```http
PUT https://localhost:7002/api/Proveedores/1
Authorization: Bearer {token}
Content-Type: application/json
```

Body:

```json
{
  "nombre": "Tech Distribuidora Actualizada",
  "contacto": "tech.actualizado@email.com"
}
```

---

## DELETE — eliminar proveedor

```http
DELETE https://localhost:7002/api/Proveedores/2
Authorization: Bearer {token}
```

Nota: no se puede eliminar un proveedor que tenga productos asociados.

---

# Pruebas de productos

## GET — todos los productos

```http
GET https://localhost:7002/api/Productos
Authorization: Bearer {token}
```

---

## GET — producto por ID

```http
GET https://localhost:7002/api/Productos/1
Authorization: Bearer {token}
```

---

## POST — crear producto

```http
POST https://localhost:7002/api/Productos
Authorization: Bearer {token}
Content-Type: application/json
```

Body:

```json
{
  "nombre": "Teclado Mecánico",
  "precio": 3500,
  "stock": 25,
  "idProveedor": 1,
  "idCategoria": 1
}
```

---

## PUT — actualizar producto

```http
PUT https://localhost:7002/api/Productos/1
Authorization: Bearer {token}
Content-Type: application/json
```

Body:

```json
{
  "nombre": "Laptop Lenovo Actualizada",
  "precio": 47000,
  "stock": 12,
  "idProveedor": 1,
  "idCategoria": 1
}
```

---

## DELETE — eliminar producto

```http
DELETE https://localhost:7002/api/Productos/1
Authorization: Bearer {token}
```

---

# Pruebas de consultas y agregaciones

## GET — resumen de productos

```http
GET https://localhost:7002/api/Productos/resumen
Authorization: Bearer {token}
```

Este endpoint devuelve:

* Producto con el precio más alto.
* Producto con el precio más bajo.
* Suma total de precios.
* Precio promedio.

---

## GET — productos por categoría

```http
GET https://localhost:7002/api/Productos/categoria/1
Authorization: Bearer {token}
```

---

## GET — productos por proveedor

```http
GET https://localhost:7002/api/Productos/proveedor/1
Authorization: Bearer {token}
```

---

## GET — cantidad total de productos

```http
GET https://localhost:7002/api/Productos/total
Authorization: Bearer {token}
```

---

## POST — refrescar token

```http
POST https://localhost:7002/api/Auth/refresh
Content-Type: application/json
```

Body:

```json
{
  "refreshToken": "el_refresh_token_del_login"
}
```

---

# Capturas de pantalla

## Práctica 5 — CRUD básico

### Obtener todos los usuarios

![GET All](screenshots/1-GetAllUsers.png)

### Crear usuario

![POST](screenshots/2-CreateUser.png)

### Obtener usuario por ID

![GET by ID](screenshots/3-GetByIdUser.png)

### Obtener todos los usuarios después de crear uno nuevo

![GET All](screenshots/4-GetAllNextCreateNewOneUser.png)

### Intentar crear usuario con correo duplicado

![POST duplicado](screenshots/5-CreateDuplicateUser.png)

### Actualizar usuario

![PUT](screenshots/6-UpdateUser.png)

### Eliminar usuario

![DELETE](screenshots/7-DeleteUser.png)

---

## Práctica 6 — Autenticación JWT

### Login — obtener token

![Login](screenshots/8-Login.png)

### Obtener usuarios con token

![GET con token](screenshots/9-ObtenerUsuariosConAutenticacion.png)

### Colocar token en POST

![Token en POST](screenshots/10-ColocarTokenEnPost.png)

### Crear usuario con autenticación

![POST con auth](screenshots/11-CrearUsuarioConAutenticacion.png)

### Obtener con nuevo usuario creado

![GET nuevo](screenshots/12-ObtenerConNuevoUsuario.png)

### Obtener usuario por ID con autenticación

![GET by ID](screenshots/13-ObtenerUsuarioPorID.png)

### Colocar token en PUT

![Token en PUT](screenshots/14-ColocarTokenEnPut.png)

### Actualizar usuario con autenticación

![PUT con auth](screenshots/15-ActualizarUsuarioConAutenticacion.png)

### Colocar token en DELETE

![Token en DELETE](screenshots/16-ColocarTokenEnDelete.png)

### Eliminar usuario con autenticación

![DELETE con auth](screenshots/17-EliminarUsuarioConAutenticacion.png)

### Listado final luego de cambios

![GET final](screenshots/18-ListadoFinalLuegoDeCambios.png)

---

## Práctica 7 — Productos, proveedores y categorías

### Obtener todas las categorías

![GET categorias](screenshots/19-GetAllCategorias.png)

### Crear categoría

![POST categoria](screenshots/20-CreateCategoria.png)

### Obtener categoría por ID

![GET categoria by ID](screenshots/21-GetCategoriaByID.png)

### Actualizar categoría

![PUT categoria](screenshots/22-UpdateCategoria.png)

### Eliminar categoría

![DELETE categoria](screenshots/23-DeleteCategoria.png)

### Obtener todos los proveedores

![GET proveedores](screenshots/24-GetAllProveedores.png)

### Crear proveedor

![POST proveedor](screenshots/25-CreateProveedor.png)

### Obtener proveedor por ID

![GET proveedor by ID](screenshots/26-GetProveedorByID.png)

### Actualizar proveedor

![PUT proveedor](screenshots/27-UpdateProveedor.png)

### Eliminar proveedor

![DELETE proveedor](screenshots/28-DeleteProveedor.png)

### Obtener todos los productos

![GET productos](screenshots/29-GetAllProductos.png)

### Crear producto

![POST producto](screenshots/30-CreateProducto.png)

### Obtener producto por ID

![GET producto by ID](screenshots/31-GetProductoByID.png)

### Actualizar producto

![PUT producto](screenshots/32-UpdateProducto.png)

### Eliminar producto

![DELETE producto](screenshots/33-DeleteProducto.png)

### Resumen de productos

![Resumen productos](screenshots/34-ResumenProductos.png)

### Productos por categoría

![Productos categoria](screenshots/35-ProductosPorCategoria.png)

### Productos por proveedor

![Productos proveedor](screenshots/36-ProductosPorProveedor.png)

### Cantidad total de productos

![Total productos](screenshots/37-TotalProductos.png)

---


## Notas finales

Este proyecto utiliza una base de datos en memoria, por lo que los datos se reinician cada vez que se detiene y vuelve a ejecutar la aplicación.

Para probar los endpoints protegidos es necesario autenticarse primero y utilizar el token JWT generado por el endpoint de login.
