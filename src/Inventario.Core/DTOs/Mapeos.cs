using Inventario.Core.Entities;

namespace Inventario.Core.DTOs;

public static class Mapeos
{
    public static ProductoDto ADto(this Producto p) =>
        new(p.Id, p.Nombre, p.StockActual, p.StockMinimo, p.TieneStockBajo);

    public static MovimientoDto ADto(this Movimiento m) =>
        new(m.Id, m.ProductoId, m.Tipo.ToString(), m.Cantidad, m.Fecha, m.Observacion);
}