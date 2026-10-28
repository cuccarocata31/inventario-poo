namespace Inventario.Core.Interfaces;

public interface IUnitOfWork
{
    Task<int> GuardarCambiosAsync();
}