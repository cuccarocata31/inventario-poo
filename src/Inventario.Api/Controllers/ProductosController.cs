using Inventario.Core.DTOs;
using Inventario.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Inventario.Api.Controllers;

[ApiController]
[Route("api/productos")]
public class ProductosController : ControllerBase
{
    private readonly IProductoService _service;

    public ProductosController(IProductoService service) => _service = service;

    /// <summary>Lista todos los productos.</summary>
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ProductoDto>>> Listar() =>
        Ok(await _service.ObtenerTodosAsync());

    /// <summary>Obtiene un producto por su id.</summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductoDto>> ObtenerPorId(int id) =>
        Ok(await _service.ObtenerPorIdAsync(id));

    /// <summary>Crea un producto (nace con stock 0).</summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ProductoDto>> Crear(CrearProductoDto dto)
    {
        var creado = await _service.CrearAsync(dto);
        return CreatedAtAction(nameof(ObtenerPorId), new { id = creado.Id }, creado);
    }

    /// <summary>Modifica nombre y stock mínimo de un producto.</summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductoDto>> Actualizar(int id, ActualizarProductoDto dto) =>
        Ok(await _service.ActualizarAsync(id, dto));

    /// <summary>Elimina un producto (solo si no tiene movimientos).</summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Eliminar(int id)
    {
        await _service.EliminarAsync(id);
        return NoContent();
    }

    /// <summary>Consulta el stock actual de un producto.</summary>
    [HttpGet("{id:int}/stock")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<StockDto>> ObtenerStock(int id) =>
        Ok(await _service.ObtenerStockAsync(id));

    /// <summary>Lista productos con stock bajo. Con ?limite=X trae los de menos de X unidades; sin límite usa el stock mínimo de cada producto.</summary>
    [HttpGet("stock-bajo")]
    public async Task<ActionResult<IReadOnlyList<ProductoDto>>> StockBajo([FromQuery] int? limite) =>
        Ok(await _service.ObtenerConStockBajoAsync(limite));
}