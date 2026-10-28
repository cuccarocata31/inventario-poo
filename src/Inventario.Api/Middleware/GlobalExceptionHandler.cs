using Inventario.Core.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Inventario.Api.Middleware;

public class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken ct)
    {
        var (status, titulo) = exception switch
        {
            ProductoNoEncontradoException => (StatusCodes.Status404NotFound, "Recurso no encontrado"),
            ReglaDeNegocioException       => (StatusCodes.Status400BadRequest, "Regla de negocio incumplida"),
            _                             => (0, string.Empty)
        };

        if (status == 0) return false;   // error inesperado: lo maneja el 500 por defecto

        context.Response.StatusCode = status;
        await context.Response.WriteAsJsonAsync(
            new ProblemDetails { Status = status, Title = titulo, Detail = exception.Message }, ct);
        return true;
    }
}