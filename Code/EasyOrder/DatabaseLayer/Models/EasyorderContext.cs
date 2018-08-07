using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DatabaseLayer.Models
{
    public partial class EasyorderContext : DbContext
    {
        public EasyorderContext()
        {
        }

       

        public virtual DbSet<Detalledeorden> Detalledeorden { get; set; }
        public virtual DbSet<Empleado> Empleado { get; set; }
        public virtual DbSet<Orden> Orden { get; set; }
        public virtual DbSet<Persona> Persona { get; set; }
        public virtual DbSet<Producto> Producto { get; set; }
        public virtual DbSet<Restaurante> Restaurante { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        public EasyorderContext(DbContextOptions<EasyorderContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Detalledeorden>(entity =>
            {
                entity.HasKey(e => new { e.Idorden, e.Idproducto, e.Nombreproducto, e.Iddetalle });

                entity.ToTable("DETALLEDEORDEN");

                entity.HasIndex(e => e.Idproducto)
                    .HasName("DETALLEDEORDEN2_FK");

                entity.Property(e => e.Idorden).HasColumnName("IDORDEN");

                entity.Property(e => e.Idproducto).HasColumnName("IDPRODUCTO");

                entity.Property(e => e.Nombreproducto)
                    .HasColumnName("NOMBREPRODUCTO")
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.Iddetalle).HasColumnName("IDDETALLE");

                entity.Property(e => e.Cantproducto).HasColumnName("CANTPRODUCTO");

                entity.Property(e => e.Precioparcial)
                    .HasColumnName("PRECIOPARCIAL")
                    .HasColumnType("decimal(4, 2)");

                entity.HasOne(d => d.IdordenNavigation)
                    .WithMany(p => p.Detalledeorden)
                    .HasForeignKey(d => d.Idorden)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DETALLED_DETALLEDE_ORDEN");

                entity.HasOne(d => d.IdproductoNavigation)
                    .WithMany(p => p.Detalledeorden)
                    .HasForeignKey(d => d.Idproducto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DETALLED_DETALLEDE_PRODUCTO");
            });

            modelBuilder.Entity<Empleado>(entity =>
            {
                entity.HasKey(e => new { e.Idpersona, e.Idempleado });

                entity.ToTable("EMPLEADO");

                entity.Property(e => e.Idpersona).HasColumnName("IDPERSONA");

                entity.Property(e => e.Idempleado).HasColumnName("IDEMPLEADO");

                entity.Property(e => e.Password)
                    .HasColumnName("PASSWORD")
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .HasColumnName("USERNAME")
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdpersonaNavigation)
                    .WithMany(p => p.Empleado)
                    .HasForeignKey(d => d.Idpersona)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EMPLEADO_HERENCIA_PERSONA");
            });

            modelBuilder.Entity<Orden>(entity =>
            {
                entity.HasKey(e => e.Idorden);

                entity.ToTable("ORDEN");

                entity.HasIndex(e => new { e.Idpersona, e.Idempleado })
                    .HasName("SOLICITUD_FK");

                entity.Property(e => e.Idorden)
                    .HasColumnName("IDORDEN")
                    .ValueGeneratedNever();

                entity.Property(e => e.Estadoorden)
                    .HasColumnName("ESTADOORDEN")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.Idempleado).HasColumnName("IDEMPLEADO");

                entity.Property(e => e.Idpersona).HasColumnName("IDPERSONA");

                entity.Property(e => e.Numeromesa).HasColumnName("NUMEROMESA");

                entity.Property(e => e.Preciototal).HasColumnName("PRECIOTOTAL");

                entity.HasOne(d => d.Id)
                    .WithMany(p => p.Orden)
                    .HasForeignKey(d => new { d.Idpersona, d.Idempleado })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ORDEN_SOLICITUD_EMPLEADO");
            });

            modelBuilder.Entity<Persona>(entity =>
            {
                entity.HasKey(e => e.Idpersona);

                entity.ToTable("PERSONA");

                entity.Property(e => e.Idpersona)
                    .HasColumnName("IDPERSONA")
                    .ValueGeneratedNever();

                entity.Property(e => e.Cedulapersona)
                    .HasColumnName("CEDULAPERSONA")
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.Nombrepersona)
                    .HasColumnName("NOMBREPERSONA")
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.Telefonopersona)
                    .HasColumnName("TELEFONOPERSONA")
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasKey(e => e.Idproducto);

                entity.ToTable("PRODUCTO");

                entity.Property(e => e.Idproducto)
                    .HasColumnName("IDPRODUCTO")
                    .ValueGeneratedNever();

                entity.Property(e => e.Descripcionproducto)
                    .HasColumnName("DESCRIPCIONPRODUCTO")
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Disponibilidadproducto).HasColumnName("DISPONIBILIDADPRODUCTO");

                entity.Property(e => e.Nombreproducto)
                    .IsRequired()
                    .HasColumnName("NOMBREPRODUCTO")
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.Precioproducto).HasColumnName("PRECIOPRODUCTO");
            });

            modelBuilder.Entity<Restaurante>(entity =>
            {
                entity.HasKey(e => e.Idrestaurante);

                entity.ToTable("RESTAURANTE");

                entity.Property(e => e.Idrestaurante)
                    .HasColumnName("IDRESTAURANTE")
                    .ValueGeneratedNever();

                entity.Property(e => e.Direccionrestaurante).HasColumnName("DIRECCIONRESTAURANTE");

                entity.Property(e => e.Fonorestaurante).HasColumnName("FONORESTAURANTE");

                entity.Property(e => e.Nombrerestaurante)
                    .IsRequired()
                    .HasColumnName("NOMBRERESTAURANTE");

                entity.Property(e => e.Rucrestaurante)
                    .IsRequired()
                    .HasColumnName("RUCRESTAURANTE");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
        }
    }
}
