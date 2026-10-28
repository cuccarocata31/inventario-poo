using HotChocolate;
using HotChocolate.Execution;
using Inventario.Core.Exceptions;

namespace Inventario.Api.GraphQL;

// Para que el cliente vea "Stock insuficiente..." y no "Unexpected Execution Error"
public class DominioErrorFilter : IErrorFilter
{
    public IError OnError(IError error) => error.Exception switch
    {
        ProductoNoEncontradoException ex => error.WithMessage(ex.Message).WithCode("PRODUCTO_NO_ENCONTRADO"),
        ReglaDeNegocioException ex       => error.WithMessage(ex.Message).WithCode("REGLA_DE_NEGOCIO"),
        _ => error
    };
}