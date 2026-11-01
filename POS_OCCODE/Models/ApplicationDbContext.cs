using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace POS_OCCODE.Models;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<ComprasDevolucionesDet> ComprasDevolucionesDets { get; set; }

    public virtual DbSet<ComprasDevolucionesEnc> ComprasDevolucionesEncs { get; set; }

    public virtual DbSet<DevolucionesVentasDetalle> DevolucionesVentasDetalles { get; set; }

    public virtual DbSet<DevolucionesVentasEncabezado> DevolucionesVentasEncabezados { get; set; }

    public virtual DbSet<EntradasSalidasDetalle> EntradasSalidasDetalles { get; set; }

    public virtual DbSet<EntradasSalidasEncabezado> EntradasSalidasEncabezados { get; set; }

    public virtual DbSet<ExistenciasBodega> ExistenciasBodegas { get; set; }

    public virtual DbSet<FacturasComprasDetalle> FacturasComprasDetalles { get; set; }

    public virtual DbSet<FacturasComprasEncabezado> FacturasComprasEncabezados { get; set; }

    public virtual DbSet<FacturasVentasDetalle> FacturasVentasDetalles { get; set; }

    public virtual DbSet<FacturasVentasEncabezado> FacturasVentasEncabezados { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Proveedore> Proveedores { get; set; }

    public virtual DbSet<Sucursale> Sucursales { get; set; }

    public virtual DbSet<Vendedore> Vendedores { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=SistemaVentasBD;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente).HasName("PK__Clientes__885457EE2224AFDA");

            entity.Property(e => e.IdCliente).HasColumnName("idCliente");
            entity.Property(e => e.CodigoPostal)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Codigo_Postal");
            entity.Property(e => e.Direccion)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Nit)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ComprasDevolucionesDet>(entity =>
        {
            entity.HasKey(e => e.IdDetalle).HasName("PK__Compras___49CAE2FBA2637F11");

            entity.ToTable("Compras_Devoluciones_Det");

            entity.Property(e => e.IdDetalle).HasColumnName("idDetalle");
            entity.Property(e => e.IdDetalleFactura).HasColumnName("idDetalleFactura");
            entity.Property(e => e.IdDevolucion).HasColumnName("idDevolucion");
            entity.Property(e => e.Precio).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.IdDetalleFacturaNavigation).WithMany(p => p.ComprasDevolucionesDets)
                .HasForeignKey(d => d.IdDetalleFactura)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Compras_D__idDet__7C4F7684");

            entity.HasOne(d => d.IdDevolucionNavigation).WithMany(p => p.ComprasDevolucionesDets)
                .HasForeignKey(d => d.IdDevolucion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Compras_D__idDev__7B5B524B");
        });

        modelBuilder.Entity<ComprasDevolucionesEnc>(entity =>
        {
            entity.HasKey(e => e.IdDevolucion).HasName("PK__Compras___BFAF069A7B8EC661");

            entity.ToTable("Compras_Devoluciones_Enc");

            entity.Property(e => e.IdDevolucion).HasColumnName("idDevolucion");
            entity.Property(e => e.IdFactura).HasColumnName("idFactura");
            entity.Property(e => e.IdProveedor).HasColumnName("idProveedor");
            entity.Property(e => e.NotaCreditoFel)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Nota_Credito_Fel");
            entity.Property(e => e.SerieCreditoFel)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Serie_Credito_Fel");

            entity.HasOne(d => d.IdFacturaNavigation).WithMany(p => p.ComprasDevolucionesEncs)
                .HasForeignKey(d => d.IdFactura)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Compras_D__idFac__787EE5A0");

            entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.ComprasDevolucionesEncs)
                .HasForeignKey(d => d.IdProveedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Compras_D__idPro__778AC167");
        });

        modelBuilder.Entity<DevolucionesVentasDetalle>(entity =>
        {
            entity.HasKey(e => e.IdDetalle).HasName("PK__Devoluci__49CAE2FB8FF74C3E");

            entity.ToTable("Devoluciones_Ventas_Detalle");

            entity.Property(e => e.IdDetalle).HasColumnName("idDetalle");
            entity.Property(e => e.IdDetalleFactura).HasColumnName("idDetalleFactura");
            entity.Property(e => e.IdDevolucion).HasColumnName("idDevolucion");
            entity.Property(e => e.Precio).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.IdDetalleFacturaNavigation).WithMany(p => p.DevolucionesVentasDetalles)
                .HasForeignKey(d => d.IdDetalleFactura)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Devolucio__idDet__74AE54BC");

            entity.HasOne(d => d.IdDevolucionNavigation).WithMany(p => p.DevolucionesVentasDetalles)
                .HasForeignKey(d => d.IdDevolucion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Devolucio__idDev__73BA3083");
        });

        modelBuilder.Entity<DevolucionesVentasEncabezado>(entity =>
        {
            entity.HasKey(e => e.IdDevolucion).HasName("PK__Devoluci__BFAF069A1C6D4CC2");

            entity.ToTable("Devoluciones_Ventas_Encabezado");

            entity.Property(e => e.IdDevolucion).HasColumnName("idDevolucion");
            entity.Property(e => e.IdCliente).HasColumnName("idCliente");
            entity.Property(e => e.IdFactura).HasColumnName("idFactura");
            entity.Property(e => e.NotaCreditoFel)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Nota_Credito_Fel");
            entity.Property(e => e.SerieCreditoFel)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Serie_Credito_Fel");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.DevolucionesVentasEncabezados)
                .HasForeignKey(d => d.IdCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Devolucio__idCli__6FE99F9F");

            entity.HasOne(d => d.IdFacturaNavigation).WithMany(p => p.DevolucionesVentasEncabezados)
                .HasForeignKey(d => d.IdFactura)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Devolucio__idFac__70DDC3D8");
        });

        modelBuilder.Entity<EntradasSalidasDetalle>(entity =>
        {
            entity.HasKey(e => e.IdDetalle).HasName("PK__Entradas__49CAE2FBFD6011DD");

            entity.ToTable("Entradas_Salidas_Detalle");

            entity.Property(e => e.IdDetalle).HasColumnName("idDetalle");
            entity.Property(e => e.IdEntradaSalida).HasColumnName("idEntradaSalida");
            entity.Property(e => e.IdProducto).HasColumnName("idProducto");
            entity.Property(e => e.IdSucursal).HasColumnName("idSucursal");
            entity.Property(e => e.Precio).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.IdEntradaSalidaNavigation).WithMany(p => p.EntradasSalidasDetalles)
                .HasForeignKey(d => d.IdEntradaSalida)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Entradas___idEnt__6D0D32F4");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.EntradasSalidasDetalles)
                .HasForeignKey(d => d.IdProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Entradas___idPro__6B24EA82");

            entity.HasOne(d => d.IdSucursalNavigation).WithMany(p => p.EntradasSalidasDetalles)
                .HasForeignKey(d => d.IdSucursal)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Entradas___idSuc__6C190EBB");
        });

        modelBuilder.Entity<EntradasSalidasEncabezado>(entity =>
        {
            entity.HasKey(e => e.IdEntradaSalida).HasName("PK__Entradas__E75A27FE134F9C2F");

            entity.ToTable("Entradas_Salidas_Encabezado");

            entity.Property(e => e.IdEntradaSalida).HasColumnName("idEntradaSalida");
            entity.Property(e => e.IdProveedor).HasColumnName("idProveedor");
            entity.Property(e => e.Idcliente).HasColumnName("idcliente");
            entity.Property(e => e.Motivo)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.TipoEntradaSalida)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Tipo_EntradaSalida");

            entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.EntradasSalidasEncabezados)
                .HasForeignKey(d => d.IdProveedor)
                .HasConstraintName("FK__Entradas___idPro__6754599E");

            entity.HasOne(d => d.IdclienteNavigation).WithMany(p => p.EntradasSalidasEncabezados)
                .HasForeignKey(d => d.Idcliente)
                .HasConstraintName("FK__Entradas___idcli__68487DD7");
        });

        modelBuilder.Entity<ExistenciasBodega>(entity =>
        {
            entity.HasKey(e => new { e.IdSucursal, e.IdProducto }).HasName("PK__Existenc__C778235F53B6FD89");

            entity.ToTable("Existencias_Bodega");

            entity.Property(e => e.IdSucursal).HasColumnName("idSucursal");
            entity.Property(e => e.IdProducto).HasColumnName("idProducto");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.ExistenciasBodegas)
                .HasForeignKey(d => d.IdProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Existenci__idPro__5535A963");

            entity.HasOne(d => d.IdSucursalNavigation).WithMany(p => p.ExistenciasBodegas)
                .HasForeignKey(d => d.IdSucursal)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Existenci__idSuc__5441852A");
        });

        modelBuilder.Entity<FacturasComprasDetalle>(entity =>
        {
            entity.HasKey(e => e.IdDetalle).HasName("PK__Facturas__49CAE2FB8B2DF401");

            entity.ToTable("Facturas_Compras_Detalle", tb => tb.HasTrigger("trg_sumar_existencias"));

            entity.Property(e => e.IdDetalle).HasColumnName("idDetalle");
            entity.Property(e => e.IdFactura).HasColumnName("idFactura");
            entity.Property(e => e.IdProducto).HasColumnName("idProducto");
            entity.Property(e => e.IdSucursal).HasColumnName("idSucursal");
            entity.Property(e => e.Precio).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.IdFacturaNavigation).WithMany(p => p.FacturasComprasDetalles)
                .HasForeignKey(d => d.IdFactura)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Facturas___idFac__6477ECF3");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.FacturasComprasDetalles)
                .HasForeignKey(d => d.IdProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Facturas___idPro__628FA481");

            entity.HasOne(d => d.IdSucursalNavigation).WithMany(p => p.FacturasComprasDetalles)
                .HasForeignKey(d => d.IdSucursal)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Facturas___idSuc__6383C8BA");
        });

        modelBuilder.Entity<FacturasComprasEncabezado>(entity =>
        {
            entity.HasKey(e => e.IdFactura).HasName("PK__Facturas__3CD5687ECC192CD7");

            entity.ToTable("Facturas_Compras_Encabezado");

            entity.Property(e => e.IdFactura).HasColumnName("idFactura");
            entity.Property(e => e.IdProveedor).HasColumnName("idProveedor");
            entity.Property(e => e.NumeroFel)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Numero_Fel");
            entity.Property(e => e.SerieFel)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Serie_Fel");

            entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.FacturasComprasEncabezados)
                .HasForeignKey(d => d.IdProveedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Facturas___idPro__5FB337D6");
        });

        modelBuilder.Entity<FacturasVentasDetalle>(entity =>
        {
            entity.HasKey(e => e.IdDetalle).HasName("PK__Facturas__49CAE2FBAA219CE0");

            entity.ToTable("Facturas_Ventas_Detalle", tb => tb.HasTrigger("trg_restar_existencias"));

            entity.Property(e => e.IdDetalle).HasColumnName("idDetalle");
            entity.Property(e => e.IdFactura).HasColumnName("idFactura");
            entity.Property(e => e.IdProducto).HasColumnName("idProducto");
            entity.Property(e => e.IdSucursal).HasColumnName("idSucursal");
            entity.Property(e => e.Precio).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.IdFacturaNavigation).WithMany(p => p.FacturasVentasDetalles)
                .HasForeignKey(d => d.IdFactura)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Facturas___idFac__5CD6CB2B");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.FacturasVentasDetalles)
                .HasForeignKey(d => d.IdProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Facturas___idPro__5AEE82B9");

            entity.HasOne(d => d.IdSucursalNavigation).WithMany(p => p.FacturasVentasDetalles)
                .HasForeignKey(d => d.IdSucursal)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Facturas___idSuc__5BE2A6F2");
        });

        modelBuilder.Entity<FacturasVentasEncabezado>(entity =>
        {
            entity.HasKey(e => e.IdFactura).HasName("PK__Facturas__3CD5687E3F2B4C9D");

            entity.ToTable("Facturas_Ventas_Encabezado");

            entity.Property(e => e.IdFactura).HasColumnName("idFactura");
            entity.Property(e => e.IdCliente).HasColumnName("idCliente");
            entity.Property(e => e.NumeroFel)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Numero_Fel");
            entity.Property(e => e.SerieFel)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Serie_Fel");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.FacturasVentasEncabezados)
                .HasForeignKey(d => d.IdCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Facturas___idCli__5812160E");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProducto).HasName("PK__Producto__07F4A132CF8227EE");

            entity.Property(e => e.IdProducto).HasColumnName("idProducto");
            entity.Property(e => e.Nombre)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.PrecioCompra)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("Precio_Compra");
            entity.Property(e => e.PrecioVenta)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("Precio_Venta");
        });

        modelBuilder.Entity<Proveedore>(entity =>
        {
            entity.HasKey(e => e.IdProveedor).HasName("PK__Proveedo__A3FA8E6B897F4E0F");

            entity.Property(e => e.IdProveedor).HasColumnName("idProveedor");
            entity.Property(e => e.CodigoPostal)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Codigo_Postal");
            entity.Property(e => e.Direccion)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.NitProveedor)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("Nit_Proveedor");
            entity.Property(e => e.NombreProveedor)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("Nombre_Proveedor");
            entity.Property(e => e.Telefono)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Sucursale>(entity =>
        {
            entity.HasKey(e => e.IdSucursal).HasName("PK__Sucursal__F707694C0AD8E313");

            entity.Property(e => e.IdSucursal).HasColumnName("idSucursal");
            entity.Property(e => e.Direccion)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.NombreSucursal)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("Nombre_Sucursal");
            entity.Property(e => e.Telefono)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Vendedore>(entity =>
        {
            entity.HasKey(e => e.IdVendedor).HasName("PK__Vendedor__A6693F933B460FF5");

            entity.Property(e => e.IdVendedor).HasColumnName("idVendedor");
            entity.Property(e => e.Direccion)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.NombreVendedor)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("Nombre_Vendedor");
            entity.Property(e => e.Telefono)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
