namespace Inventario.Core.Exceptions;

public class StockInsuficienteException : ReglaDeNegocioException
{
    public StockInsuficienteException(string producto, int disponible, int solicitado)
        : base($"Stock insuficiente de '{producto}'. Disponible: {disponible}, solicitado: {solicitado}.") { }
}