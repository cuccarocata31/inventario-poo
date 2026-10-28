using Inventario.Core.DTOs;
using Inventario.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Inventario.Api.Controllers;

[ApiController]
[Route("api/productos/{productoId:int}")]
public class MovimientosController : ControllerBase
{
    private readonly IMovimientoService _service;

    public MovimientosController(IMovimientoService service) => _service = service;

    /// <summary>Registra una entrada de stock.</summary>
    [HttpPost("entradas")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MovimientoDto>> RegistrarEntrada(int productoId, RegistrarMovimientoDto dto)
    {
        var movimiento = await _service.RegistrarEntradaAsync(productoId, dto);
        return Created($"/api/productos/{productoId}/movimientos", movimiento);
    }

    /// <summary>Registra una salida de stock. Falla con 400 si no hay stock suficiente.</summary>
    [HttpPost("salidas")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MovimientoDto>> RegistrarSalida(int productoId, RegistrarMovimientoDto dto)
    {
        var movimiento = await _service.RegistrarSalidaAsync(productoId, dto);
        return Created($"/api/productos/{productoId}/movimientos", movimiento);
    }

    /// <summary>Historial de movimientos del producto (el más reciente primero).</summary>
    [HttpGet("movimientos")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IReadOnlyList<MovimientoDto>>> Historial(int productoId) =>
        Ok(await _service.ObtenerHistorialAsync(productoId));
}