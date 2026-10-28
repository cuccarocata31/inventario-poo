using Inventario.Core.Interfaces;

namespace Inventario.Data.Repositories;

public class EfUnitOfWork : IUnitOfWork
{
    private readonly InventarioDbContext _context;

    public EfUnitOfWork(InventarioDbContext context) => _context = context;

    public Task<int> GuardarCambiosAsync() => _context.SaveChangesAsync();
}