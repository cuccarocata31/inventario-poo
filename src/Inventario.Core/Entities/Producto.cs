using Inventario.Core.Enums;
using Inventario.Core.Exceptions;

namespace Inventario.Core.Entities;

public class Producto
{
    public int Id { get; private set; }
    public string Nombre { get; private set; } = string.Empty;
    public int StockActual { get; private set; }
    public int StockMinimo { get; private set; }

    public bool TieneStockBajo => StockActual < StockMinimo;

    private Producto() { }   // lo usa EF Core

    public Producto(string nombre, int stockMinimo) => Actualizar(nombre, stockMinimo);

    public void Actualizar(string nombre, int stockMinimo)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            throw new ReglaDeNegocioException("El nombre del producto es obligatorio.");
        if (stockMinimo < 0)
            throw new ReglaDeNegocioException("El stock mínimo no puede ser negativo.");

        Nombre = nombre.Trim();
        StockMinimo = stockMinimo;
    }

    public Movimiento RegistrarEntrada(int cantidad, string? observacion)
    {
        ValidarCantidad(cantidad);
        StockActual += cantidad;
        return new Movimiento(Id, TipoMovimiento.Entrada, cantidad, observacion);
    }

    public Movimiento RegistrarSalida(int cantidad, string? observacion)
    {
        ValidarCantidad(cantidad);
        if (cantidad > StockActual)
            throw new StockInsuficienteException(Nombre, StockActual, cantidad);

        StockActual -= cantidad;
        return new Movimiento(Id, TipoMovimiento.Salida, cantidad, observacion);
    }

    private static void ValidarCantidad(int cantidad)
    {
        if (cantidad <= 0)
            throw new ReglaDeNegocioException("La cantidad debe ser mayor a cero.");
    }
}