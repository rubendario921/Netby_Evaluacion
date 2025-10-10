using inventory_management_api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace inventory_management_api.Infrastructure.Persistence.Context;

public partial class InventoryContext : DbContext
{
    public InventoryContext()
    {
    }

    public InventoryContext(DbContextOptions<InventoryContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Transaccione> Transacciones { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Producto__3214EC078A561923");

            entity.ToTable(tb => tb.HasTrigger("TR_Producto_AuditUpdate"));

            entity.HasIndex(e => e.Codigo, "UQ__Producto__06370DACCB114119").IsUnique();

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Categoria).HasMaxLength(200);
            entity.Property(e => e.Codigo).HasMaxLength(50);
            entity.Property(e => e.Estado).HasDefaultValue(true);
            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Nombre).HasMaxLength(200);
            entity.Property(e => e.Precio).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.UsuarioCreacion)
                .HasMaxLength(100)
                .HasDefaultValueSql("(suser_sname())");
            entity.Property(e => e.UsuarioEliminacion).HasMaxLength(100);
            entity.Property(e => e.UsuarioModificacion).HasMaxLength(100);
        });

        modelBuilder.Entity<Transaccione>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Transacc__3214EC07D2EC60DC");

            entity.ToTable(tb => tb.HasTrigger("TR_Transacciones_AuditUpdate"));

            entity.HasIndex(e => e.Estado, "IX_Transacciones_Estado");

            entity.HasIndex(e => e.Fecha, "IX_Transacciones_Fecha").IsDescending();

            entity.HasIndex(e => new { e.Fecha, e.TipoTransaccion }, "IX_Transacciones_Fecha_Tipo")
                .IsDescending(true, false);

            entity.HasIndex(e => e.ProductoId, "IX_Transacciones_ProductoId");

            entity.HasIndex(e => e.TipoTransaccion, "IX_Transacciones_TipoTransaccion");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Estado).HasDefaultValue(true);
            entity.Property(e => e.Fecha).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.PrecioTotal).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.PrecioUnitario).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TipoTransaccion)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.UsuarioAnulacion).HasMaxLength(100);
            entity.Property(e => e.UsuarioCreacion)
                .HasMaxLength(100)
                .HasDefaultValueSql("(suser_sname())");
            entity.Property(e => e.UsuarioModificacion).HasMaxLength(100);

            entity.HasOne(d => d.Producto).WithMany(p => p.Transacciones)
                .HasForeignKey(d => d.ProductoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Transacciones_Producto");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}