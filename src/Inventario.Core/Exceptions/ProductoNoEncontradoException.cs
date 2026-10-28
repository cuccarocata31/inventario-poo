namespace Inventario.Core.Exceptions;

public class ProductoNoEncontradoException : Exception
{
    public ProductoNoEncontradoException(int id) : base($"No existe un producto con id {id}.") { }
}