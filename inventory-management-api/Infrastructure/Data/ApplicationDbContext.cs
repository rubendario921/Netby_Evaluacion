using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

//     protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
// #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https: //go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//         => optionsBuilder.UseSqlServer(
//             "Server=localhost;Database=inventory;User Id=sa;Password=Password123!;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Product__3214EC078A561923");

            entity.ToTable(tb => tb.HasTrigger("TR_Product_AuditUpdate"));

            entity.HasIndex(e => e.Code, "UQ__Product__06370DACCB114119").IsUnique();

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Category).HasMaxLength(200);
            entity.Property(e => e.Code).HasMaxLength(50);
            entity.Property(e => e.Status).HasDefaultValue(true);
            entity.Property(e => e.CreationDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.UserCreation)
                .HasMaxLength(100)
                .HasDefaultValueSql("(suser_sname())");
            entity.Property(e => e.UserDeletion).HasMaxLength(100);
            entity.Property(e => e.UserModification).HasMaxLength(100);
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Transacc__3214EC07D2EC60DC");

            entity.ToTable(tb => tb.HasTrigger("TR_Transactions_AuditUpdate"));

            entity.HasIndex(e => e.Status, "IX_Transactions_Status");

            entity.HasIndex(e => e.Date, "IX_Transactions_Date").IsDescending();

            entity.HasIndex(e => new { e.Date, e.TransactionType }, "IX_Transactions_Date_Type")
                .IsDescending(true, false);

            entity.HasIndex(e => e.ProductId, "IX_Transactions_ProductoId");

            entity.HasIndex(e => e.TransactionType, "IX_Transactions_TransactionType");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Status).HasDefaultValue(true);
            entity.Property(e => e.Date).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.CreationDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.PriceTotal).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.UnitPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TransactionType)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.UserCancellation).HasMaxLength(100);
            entity.Property(e => e.UserCreation)
                .HasMaxLength(100)
                .HasDefaultValueSql("(suser_sname())");
            entity.Property(e => e.UserModification).HasMaxLength(100);

            entity.HasOne(d => d.Product).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Transactions_Product");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}