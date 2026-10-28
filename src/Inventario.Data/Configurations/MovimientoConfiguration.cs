using Inventario.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inventario.Data.Configurations;

public class MovimientoConfiguration : IEntityTypeConfiguration<Movimiento>
{
    public void Configure(EntityTypeBuilder<Movimiento> builder)
    {
        builder.HasKey(m => m.Id);
        builder.Property(m => m.Tipo).HasConversion<string>().HasMaxLength(10); // guarda "Entrada"/"Salida"
        builder.Property(m => m.Observacion).HasMaxLength(250);

        builder.HasOne<Producto>().WithMany()
               .HasForeignKey(m => m.ProductoId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(m => new { m.ProductoId, m.Fecha });
    }
}