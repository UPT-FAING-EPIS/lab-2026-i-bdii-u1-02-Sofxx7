using ClienteAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ClienteAPI.Data;

public partial class BdClientesContext : DbContext
{
    public BdClientesContext()
    {
    }

    public BdClientesContext(DbContextOptions<BdClientesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<ClientesDocumento> ClientesDocumentos { get; set; }

    public virtual DbSet<TiposDocumento> TiposDocumentos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it to a user secret configuration and use it here
        => optionsBuilder.UseSqlServer("Server=(local);Database=BD_CLIENTES;User Id=sa;Password=Upt.2022;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente);

            entity.ToTable("CLIENTES");

            entity.Property(e => e.IdCliente).HasColumnName("ID_CLIENTE");
            entity.Property(e => e.NomCliente)
                .HasMaxLength(100)
                .HasColumnName("NOM_CLIENTE");
        });

        modelBuilder.Entity<ClientesDocumento>(entity =>
        {
            entity.HasKey(e => new { e.IdCliente, e.IdTipoDocumento });

            entity.ToTable("CLIENTES_DOCUMENTOS");

            entity.Property(e => e.IdCliente).HasColumnName("ID_CLIENTE");
            entity.Property(e => e.IdTipoDocumento).HasColumnName("ID_TIPO_DOCUMENTO");
            entity.Property(e => e.NumDocumento)
                .HasMaxLength(15)
                .HasColumnName("NUM_DOCUMENTO");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.ClientesDocumentos)
                .HasForeignKey(d => d.IdCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CLIENTES_DOCUMENTOS_X_CLIENTE");

            entity.HasOne(d => d.IdTipoDocumentoNavigation).WithMany(p => p.ClientesDocumentos)
                .HasForeignKey(d => d.IdTipoDocumento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CLIENTES_DOCUMENTOS_X_TIPO_DOCUMENTO");
        });

        modelBuilder.Entity<TiposDocumento>(entity =>
        {
            entity.HasKey(e => e.IdTipoDocumento);

            entity.ToTable("TIPOS_DOCUMENTOS");

            entity.Property(e => e.IdTipoDocumento).HasColumnName("ID_TIPO_DOCUMENTO");
            entity.Property(e => e.DesTipoDocumento)
                .HasMaxLength(50)
                .HasColumnName("DES_TIPO_DOCUMENTO");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
