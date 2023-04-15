using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace VerduleriaApi.Models
{
    public class VerduleriaContext : DbContext
    {
        public VerduleriaContext(DbContextOptions<VerduleriaContext> opciones)
            : base(opciones)
        {
        }

        //Entidades
        public DbSet<Carrito> Carrito { get; set; }
        public DbSet<DetalleCarrito> DetalleCarrito { get; set; }
        public DbSet<Producto> Producto { get; set; }
        public DbSet<ProductoPromocion> ProductoPromocion { get; set; }
        public DbSet<Promocion> Promocion { get; set; }
        public DbSet<Rol> Rol { get; set; }
        public DbSet<TipoProducto> TipoProducto { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Compra> Compra { get; set; }
        public DbSet<DetalleCompra> DetalleCompra { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Carrito>(Carrito =>
            {
                Carrito.ToTable("Carrito");
                Carrito.HasKey(x => x.Id);
                Carrito.Property(x => x.Id);
            });

            modelBuilder.Entity<Compra>(Compra =>
            {
                Compra.ToTable("Compra");
                Compra.HasKey(x => x.Id);
                Compra.Property(x => x.Id)
                .ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<DetalleCarrito>(DetalleCarrito =>
            {
                DetalleCarrito.ToTable("DetalleCarrito");
                DetalleCarrito.HasKey(x => x.Id);
                DetalleCarrito.Property(x => x.Id)
                .ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<DetalleCompra>(DetalleCompra =>
            {
                DetalleCompra.ToTable("DetalleCompra");
                DetalleCompra.HasKey(x => x.Id);
                DetalleCompra.Property(x => x.Id)
                .ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Producto>(Producto =>
            {
                Producto.ToTable("Producto");
                Producto.HasKey(x => x.Id);
                Producto.Property(x => x.Id)
                .ValueGeneratedOnAdd();
                Producto.Property(x => x.Nombre)
                .IsRequired()
                .HasMaxLength(100);
                Producto.Property(x => x.Descripcion)
                .IsRequired()
                .HasMaxLength(200);
                Producto.Property(x => x.Cantidad)
                .IsRequired()
                .HasMaxLength(20);
                Producto.Property(x => x.Activo)
                .IsRequired();
            });

            modelBuilder.Entity<ProductoPromocion>(ProductoPromocion =>
            {
                ProductoPromocion.ToTable("ProductoPromocion");
                ProductoPromocion.HasKey(x => x.Id);
                ProductoPromocion.Property(x => x.Id)
                .ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Promocion>(Promocion =>
            {
                Promocion.ToTable("Promocion");
                Promocion.HasKey(x => x.Id);
                Promocion.Property(x => x.Id)
                .ValueGeneratedOnAdd();
                Promocion.Property(x => x.Nombre)
                .IsRequired()
                .HasMaxLength(100);
                Promocion.Property(x => x.Descripcion)
                .HasMaxLength(200);
                Promocion.Property(x => x.Activo)
                .IsRequired();
            });

            modelBuilder.Entity<Rol>(Rol =>
            {
                Rol.ToTable("Rol");
                Rol.HasKey(x => x.Id);
                Rol.Property(x => x.Id)
                .ValueGeneratedOnAdd();
                Rol.Property(x => x.Descripcion)
                .IsRequired()
                .HasMaxLength(100);
            });

            modelBuilder.Entity<TipoProducto>(TipoProducto =>
            {
                TipoProducto.ToTable("TipoProducto");
                TipoProducto.HasKey(x => x.Id);
                TipoProducto.Property(x => x.Id)
                .ValueGeneratedOnAdd();
                TipoProducto.Property(x => x.Nombre)
                .IsRequired()
                .HasMaxLength(100);
            });

            modelBuilder.Entity<Usuario>(Usuario =>
            {
                Usuario.ToTable("Usuario");
                Usuario.HasKey(x => x.Id);
                Usuario.Property(x => x.Id)
                .ValueGeneratedOnAdd();
                Usuario.Property(x => x.Correo)
                .IsRequired()
                .HasMaxLength(100);
                Usuario.Property(x => x.NombreCompleto)
                .HasMaxLength(200);
                Usuario.Property(x => x.Ubicacion)
                .IsRequired()
                .HasMaxLength(200);
            });

            modelBuilder.Entity<Producto>().HasOne(x => x.TipoProducto)
                .WithMany(a => a.Productos)
                .HasForeignKey(l => l.IdTipo);

            modelBuilder.Entity<Usuario>().HasOne(x => x.Rol)
                .WithMany(a => a.Usuarios)
                .HasForeignKey(l => l.IdRol);

            modelBuilder.Entity<ProductoPromocion>().HasOne(x => x.Promocion)
                .WithMany(a => a.ProductoPromocion)
                .HasForeignKey(l => l.IdPromocion);

            modelBuilder.Entity<ProductoPromocion>().HasOne(x => x.Producto)
                .WithMany(a => a.ProductoPromocion)
                .HasForeignKey(l => l.IdProducto);

            modelBuilder.Entity<DetalleCarrito>().HasOne(x => x.Carrito)
                .WithMany(a => a.DetalleCarrito)
                .HasForeignKey(l => l.IdCarrito);

            modelBuilder.Entity<DetalleCarrito>().HasOne(x => x.Producto)
                .WithMany(a => a.DetalleCarrito)
                .HasForeignKey(l => l.IdProducto);

            modelBuilder.Entity<DetalleCompra>().HasOne(x => x.Compra)
                .WithMany(a => a.DetalleCompra)
                .HasForeignKey(l => l.IdCompra);

            modelBuilder.Entity<DetalleCompra>().HasOne(x => x.Producto)
                .WithMany(a => a.DetalleCompra)
                .HasForeignKey(l => l.IdProducto);

            /*modelBuilder.Entity<Promocion>().HasOne(x => x.TipoPromocion)
                .WithMany(a => a.Promociones)
                .HasForeignKey(l => l.IdTipo);

            modelBuilder.Entity<TipoPromocion>().HasOne(x => x.ProductoRegala)
                .WithMany(a => a.TipoPromociones)
                .HasForeignKey(l => l.IdProductoRegala);*/
        }
    }
}