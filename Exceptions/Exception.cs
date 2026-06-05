namespace UsuariosApi.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string mensaje) : base(mensaje) { }
    }

    public class CorreoDuplicadoException : Exception
    {
        public CorreoDuplicadoException(string correo)
            : base($"El correo electrónico '{correo}' ya está en uso.") { }
    }

    public class UnauthorizedException : Exception
    {
        public UnauthorizedException(string mensaje) : base(mensaje) { }
    }
}
