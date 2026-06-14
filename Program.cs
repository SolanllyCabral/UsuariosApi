using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using System.Text;
using System.Text.Json.Serialization;
using UsuariosApi.Context;
using UsuariosApi.Models;
using UsuariosApi.Services;
using UsuariosApi.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(x =>
        x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "UsuariosApi",
        Version = "v1",
        Description = "API con autenticación JWT, usuarios, productos, proveedores y categorías"
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Escribe solo el token JWT. Swagger agregará el Bearer automáticamente."
    });

    options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
    {
        [new OpenApiSecuritySchemeReference("Bearer", document)] = new List<string>()
    });
});

// Base de datos en memoria
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("UsuariosDb"));

builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ICategoriaService, CategoriaService>();
builder.Services.AddScoped<IProveedorService, ProveedorService>();
builder.Services.AddScoped<IProductoService, ProductoService>();

// Configuración JWT
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]!);

builder.Services.AddAuthentication(op =>
{
    op.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    op.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(opt =>
{
    opt.RequireHttpsMetadata = false;
    opt.SaveToken = true;
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});

var app = builder.Build();

// Datos iniciales en memoria
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    if (!context.Usuarios.Any())
    {
        context.Usuarios.AddRange(
            new Usuario
            {
                Nombre = "Ana Martínez",
                Correo = "anamartinez@gmail.com",
                FechaDeNacimiento = new DateTime(1995, 4, 12)
            },
            new Usuario
            {
                Nombre = "Carlos López",
                Correo = "carloslopez@hotmail.com",
                FechaDeNacimiento = new DateTime(1988, 9, 3)
            },
            new Usuario
            {
                Nombre = "María Rodríguez",
                Correo = "mariarodriguez@outlook.com",
                FechaDeNacimiento = new DateTime(2000, 1, 27)
            }
        );
    }

    if (!context.UsuariosAuth.Any())
    {
        context.UsuariosAuth.Add(new UsuarioAuth
        {
            Username = "admin",
            PasswordHash = AuthService.EncriptarSHA256("admin123")
        });
    }

    if (!context.Proveedores.Any())
    {
        context.Proveedores.AddRange(
            new Proveedor
            {
                Id = 1,
                Nombre = "Tech Distribuidora",
                Contacto = "tech@email.com"
            },
            new Proveedor
            {
                Id = 2,
                Nombre = "OfiMarket",
                Contacto = "ofimarket@email.com"
            }
        );
    }

    if (!context.Categorias.Any())
    {
        context.Categorias.AddRange(
            new Categoria
            {
                Id = 1,
                Nombre = "Electrónica"
            },
            new Categoria
            {
                Id = 2,
                Nombre = "Oficina"
            },
            new Categoria
            {
                Id = 3,
                Nombre = "Accesorios"
            }
        );
    }

    if (!context.Productos.Any())
    {
        context.Productos.AddRange(
            new Producto
            {
                Id = 1,
                Nombre = "Laptop Lenovo",
                Precio = 45000,
                Stock = 10,
                IdProveedor = 1,
                IdCategoria = 1
            },
            new Producto
            {
                Id = 2,
                Nombre = "Mouse Logitech",
                Precio = 1200,
                Stock = 50,
                IdProveedor = 1,
                IdCategoria = 3
            },
            new Producto
            {
                Id = 3,
                Nombre = "Silla de Oficina",
                Precio = 8500,
                Stock = 15,
                IdProveedor = 2,
                IdCategoria = 2
            },
            new Producto
            {
                Id = 4,
                Nombre = "Teclado Mecánico",
                Precio = 3500,
                Stock = 25,
                IdProveedor = 1,
                IdCategoria = 3
            }
        );
    }

    context.SaveChanges();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();