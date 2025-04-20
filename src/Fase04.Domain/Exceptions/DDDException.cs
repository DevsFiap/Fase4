namespace Fase04.Domain.Exceptions;

public class DDDException : DomainException
{
    public DDDException(string message) : base(message)
    {}
}