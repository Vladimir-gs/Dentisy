using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Shared.Models;
using Server;

namespace Server.Context;

public partial class DentisyContext : DbContext
{
    public DentisyContext()
    {
    }

    public DentisyContext(DbContextOptions<DentisyContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ArchivosPacientes> ArchivosPacientes { get; set; }

    public virtual DbSet<Citas> Citas { get; set; }

    public virtual DbSet<DetalleFactura> DetalleFactura { get; set; }

    public virtual DbSet<EstadosPiezas> EstadosPiezas { get; set; }

    public virtual DbSet<Facturas> Facturas { get; set; }

    public virtual DbSet<HistorialClinico> HistorialClinico { get; set; }

    public virtual DbSet<HistorialTratamientos> HistorialTratamientos { get; set; }

    public virtual DbSet<Odontogramas> Odontogramas { get; set; }

    public virtual DbSet<Pacientes> Pacientes { get; set; }

    public virtual DbSet<PiezasDentales> PiezasDentales { get; set; }

    public virtual DbSet<Productos> Productos { get; set; }

    public virtual DbSet<Roles> Roles { get; set; }

    public virtual DbSet<Tratamientos> Tratamientos { get; set; }

    public virtual DbSet<TratamientosPiezas> TratamientosPiezas { get; set; }

    public virtual DbSet<Usuarios> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=VLADIMIR\\SQLEXPRESS;Database=Dentisy;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ArchivosPacientes>(entity =>
        {
            entity.HasKey(e => e.IdArchivo).HasName("PK__Archivos__26B9211161EF1717");

            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Tipo).HasMaxLength(50);

            entity.HasOne(d => d.IdPacienteNavigation).WithMany(p => p.ArchivosPacientes)
                .HasForeignKey(d => d.IdPaciente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ArchivosPacientes_Paciente");
        });

        modelBuilder.Entity<Citas>(entity =>
        {
            entity.HasKey(e => e.IdCita).HasName("PK__Citas__394B02022A7A9977");

            entity.Property(e => e.Estado)
                .HasMaxLength(30)
                .HasDefaultValue("Pendiente");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FechaFin).HasColumnType("datetime");
            entity.Property(e => e.FechaInicio).HasColumnType("datetime");

            entity.HasOne(d => d.IdDoctorNavigation).WithMany(p => p.Citas)
                .HasForeignKey(d => d.IdDoctor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Citas_Doctor");

            entity.HasOne(d => d.IdPacienteNavigation).WithMany(p => p.Citas)
                .HasForeignKey(d => d.IdPaciente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Citas_Pacientes");
        });

        modelBuilder.Entity<DetalleFactura>(entity =>
        {
            entity.HasKey(e => e.IdDetalle).HasName("PK__DetalleF__E43646A5A15019D3");

            entity.Property(e => e.Cantidad).HasDefaultValue(1);
            entity.Property(e => e.Precio).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Subtotal).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdFacturaNavigation).WithMany(p => p.DetalleFactura)
                .HasForeignKey(d => d.IdFactura)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetalleFactura_Factura");

            entity.HasOne(d => d.IdTratamientoNavigation).WithMany(p => p.DetalleFactura)
                .HasForeignKey(d => d.IdTratamiento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetalleFactura_Tratamiento");
        });

        modelBuilder.Entity<EstadosPiezas>(entity =>
        {
            entity.HasKey(e => e.IdEstadoPieza).HasName("PK__EstadosP__97885543DF8DB09E");

            entity.Property(e => e.Estado).HasMaxLength(100);

            entity.HasOne(d => d.IdOdontogramaNavigation).WithMany(p => p.EstadosPiezas)
                .HasForeignKey(d => d.IdOdontograma)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EstadosPiezas_Odontograma");

            entity.HasOne(d => d.IdPiezaNavigation).WithMany(p => p.EstadosPiezas)
                .HasForeignKey(d => d.IdPieza)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EstadosPiezas_Pieza");
        });

        modelBuilder.Entity<Facturas>(entity =>
        {
            entity.HasKey(e => e.IdFactura).HasName("PK__Facturas__50E7BAF1F8061533");

            entity.HasIndex(e => e.NumeroFactura, "UQ__Facturas__CF12F9A687A5BC0A").IsUnique();

            entity.Property(e => e.Descuento)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(10, 2)");
            entity.Property(e => e.EstadoPago)
                .HasMaxLength(30)
                .HasDefaultValue("Pendiente");
            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Itbis)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("ITBIS");
            entity.Property(e => e.MetodoPago).HasMaxLength(50);
            entity.Property(e => e.NumeroFactura).HasMaxLength(50);
            entity.Property(e => e.Subtotal)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Total)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdPacienteNavigation).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.IdPaciente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Facturas_Pacientes");
        });

        modelBuilder.Entity<HistorialClinico>(entity =>
        {
            entity.HasKey(e => e.IdHistorial).HasName("PK__Historia__9CC7DBB4A5583E2C");

            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.IdDoctorNavigation).WithMany(p => p.HistorialClinico)
                .HasForeignKey(d => d.IdDoctor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Historial_Doctor");

            entity.HasOne(d => d.IdPacienteNavigation).WithMany(p => p.HistorialClinico)
                .HasForeignKey(d => d.IdPaciente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Historial_Paciente");
        });

        modelBuilder.Entity<HistorialTratamientos>(entity =>
        {
            entity.HasKey(e => e.IdHistorialTratamiento).HasName("PK__Historia__EA787101D128B208");

            entity.Property(e => e.Precio)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdHistorialNavigation).WithMany(p => p.HistorialTratamientos)
                .HasForeignKey(d => d.IdHistorial)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HistorialTratamientos_Historial");

            entity.HasOne(d => d.IdTratamientoNavigation).WithMany(p => p.HistorialTratamientos)
                .HasForeignKey(d => d.IdTratamiento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HistorialTratamientos_Tratamientos");
        });

        modelBuilder.Entity<Odontogramas>(entity =>
        {
            entity.HasKey(e => e.IdOdontograma).HasName("PK__Odontogr__6D5E26D44E95F3C3");

            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.IdDoctorNavigation).WithMany(p => p.Odontogramas)
                .HasForeignKey(d => d.IdDoctor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Odontogramas_Doctor");

            entity.HasOne(d => d.IdPacienteNavigation).WithMany(p => p.Odontogramas)
                .HasForeignKey(d => d.IdPaciente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Odontogramas_Paciente");
        });

        modelBuilder.Entity<Pacientes>(entity =>
        {
            entity.HasKey(e => e.IdPaciente).HasName("PK__Paciente__C93DB49BA30FAD19");

            entity.HasIndex(e => e.Cedula, "UQ__Paciente__B4ADFE380F12BC52").IsUnique();

            entity.Property(e => e.Apellidos).HasMaxLength(100);
            entity.Property(e => e.Cedula).HasMaxLength(20);
            entity.Property(e => e.ContactoEmergencia).HasMaxLength(100);
            entity.Property(e => e.Correo).HasMaxLength(150);
            entity.Property(e => e.Estado).HasDefaultValue(true);
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Nombres).HasMaxLength(100);
            entity.Property(e => e.Sexo).HasMaxLength(20);
            entity.Property(e => e.Telefono).HasMaxLength(20);
            entity.Property(e => e.TelefonoEmergencia).HasMaxLength(20);
            entity.Property(e => e.TipoSangre).HasMaxLength(10);
        });

        modelBuilder.Entity<PiezasDentales>(entity =>
        {
            entity.HasKey(e => e.IdPieza).HasName("PK__PiezasDe__40735AA6F924FAF9");

            entity.HasIndex(e => e.Numero, "UQ__PiezasDe__7E532BC6FD39B9FA").IsUnique();

            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Numero).HasMaxLength(10);
            entity.Property(e => e.Tipo).HasMaxLength(50);
        });

        modelBuilder.Entity<Productos>(entity =>
        {
            entity.HasKey(e => e.IdProducto).HasName("PK__Producto__098892102DB197E2");

            entity.HasIndex(e => e.Codigo, "UQ__Producto__06370DAC742511B5").IsUnique();

            entity.Property(e => e.Codigo).HasMaxLength(50);
            entity.Property(e => e.Estado).HasDefaultValue(true);
            entity.Property(e => e.Nombre).HasMaxLength(150);
            entity.Property(e => e.PrecioCompra)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(10, 2)");
            entity.Property(e => e.PrecioVenta)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(10, 2)");
            entity.Property(e => e.StockActual).HasDefaultValue(0);
            entity.Property(e => e.StockMinimo).HasDefaultValue(0);
        });

        modelBuilder.Entity<Roles>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PK__Roles__2A49584C415C6B25");

            entity.HasIndex(e => e.Nombre, "UQ__Roles__75E3EFCF7851E7AD").IsUnique();

            entity.Property(e => e.Nombre).HasMaxLength(50);
        });

        modelBuilder.Entity<Tratamientos>(entity =>
        {
            entity.HasKey(e => e.IdTratamiento).HasName("PK__Tratamie__5CB7E7535DD6BFE4");

            entity.Property(e => e.Nombre).HasMaxLength(150);
            entity.Property(e => e.Precio).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<TratamientosPiezas>(entity =>
        {
            entity.HasKey(e => e.IdTratamientoPieza).HasName("PK__Tratamie__82A4D864192E18CE");

            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.IdEstadoPiezaNavigation).WithMany(p => p.TratamientosPiezas)
                .HasForeignKey(d => d.IdEstadoPieza)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TratamientosPiezas_Estado");

            entity.HasOne(d => d.IdTratamientoNavigation).WithMany(p => p.TratamientosPiezas)
                .HasForeignKey(d => d.IdTratamiento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TratamientosPiezas_Tratamiento");
        });

        modelBuilder.Entity<Usuarios>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuarios__5B65BF97275D8CF7");

            entity.HasIndex(e => e.Correo, "UQ__Usuarios__60695A192E54AB80").IsUnique();

            entity.Property(e => e.Apellido).HasMaxLength(100);
            entity.Property(e => e.Correo).HasMaxLength(150);
            entity.Property(e => e.Estado).HasDefaultValue(true);
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Telefono).HasMaxLength(20);

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdRol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Usuarios_Roles");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
