using Inventario.Core.Interfaces;
using Inventario.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Inventario.Data;

public static class DependencyInjection
{
    // Solo este proyecto conoce SQLite
    public static IServiceCollection AddInventarioData(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<InventarioDbContext>(o => o.UseSqlite(connectionString));
        services.AddScoped<IProductoRepository, ProductoRepository>();
        services.AddScoped<IMovimientoRepository, MovimientoRepository>();
        services.AddScoped<IUnitOfWork, EfUnitOfWork>();
        return services;
    }
}