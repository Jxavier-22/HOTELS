using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MINTHOTEL.Models.Db;

public partial class MintHotelsContext : DbContext
{
    public MintHotelsContext()
    {
    }

    public MintHotelsContext(DbContextOptions<MintHotelsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Hotel> Hotels { get; set; }

    public virtual DbSet<Propietario> Propietarios { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Visitum> Visita { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Data Source=172.17.1.62;Initial Catalog=Mint_Hotels;User ID=sa;Password=123456;Trust Server Certificate=True ");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Hotel>(entity =>
        {
            entity.HasKey(e => e.IdHotel).HasName("PK__Hotel__40D35135B6ED80D3");

            entity.ToTable("Hotel");

            entity.Property(e => e.IdHotel).HasColumnName("Id_Hotel");
            entity.Property(e => e.CodRuc)
                .HasMaxLength(50)
                .HasColumnName("Cod_RUC");
            entity.Property(e => e.Departamento).HasMaxLength(20);
            entity.Property(e => e.Direccion).HasMaxLength(50);
            entity.Property(e => e.Municipio).HasMaxLength(20);
            entity.Property(e => e.NomHotel)
                .HasMaxLength(50)
                .HasColumnName("Nom_Hotel");
            entity.Property(e => e.Tipo).HasMaxLength(20);
        });

        modelBuilder.Entity<Propietario>(entity =>
        {
            entity.HasKey(e => e.IdPropietario).HasName("PK__Propieta__7360E97DE2411743");

            entity.ToTable("Propietario");

            entity.Property(e => e.IdPropietario).HasColumnName("Id_Propietario");
            entity.Property(e => e.CodHotl).HasColumnName("cod_Hotl");
            entity.Property(e => e.Correo).HasMaxLength(20);
            entity.Property(e => e.FechaNac).HasColumnName("Fecha_Nac");
            entity.Property(e => e.Nacionalidad).HasMaxLength(20);
            entity.Property(e => e.NumIdentificion)
                .HasMaxLength(30)
                .HasColumnName("Num_Identificion");
            entity.Property(e => e.PrimerApellido)
                .HasMaxLength(20)
                .HasColumnName("Primer_Apellido");
            entity.Property(e => e.PrimerNom)
                .HasMaxLength(20)
                .HasColumnName("Primer_Nom");
            entity.Property(e => e.SegundoApellido)
                .HasMaxLength(20)
                .HasColumnName("Segundo_Apellido");
            entity.Property(e => e.SegundoNom)
                .HasMaxLength(20)
                .HasColumnName("Segundo_Nom");
            entity.Property(e => e.Sexo).HasMaxLength(10);
            entity.Property(e => e.TipoIdentificion)
                .HasMaxLength(20)
                .HasColumnName("Tipo_Identificion");

            entity.HasOne(d => d.CodHotlNavigation).WithMany(p => p.Propietarios)
                .HasForeignKey(d => d.CodHotl)
                .HasConstraintName("FK_Propietario_Hotel");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PK__Rol__55932E860C6D5794");

            entity.ToTable("Rol");

            entity.Property(e => e.IdRol).HasColumnName("Id_Rol");
            entity.Property(e => e.NomRol)
                .HasMaxLength(10)
                .HasColumnName("Nom_rol");
        });

		modelBuilder.Entity<Usuario>(entity =>
		{
			entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__63C76BE242447141");

			entity.ToTable("Usuario");

			entity.Property(e => e.IdUsuario).HasColumnName("Id_Usuario");
			entity.Property(e => e.Apellido).HasMaxLength(20);
			entity.Property(e => e.CodHotel).HasColumnName("Cod_Hotel");
			entity.Property(e => e.Contraseña).HasMaxLength(10);
			entity.Property(e => e.IdRol).HasColumnName("Id_rol");
			entity.Property(e => e.Nombre).HasMaxLength(20);
			entity.Property(e => e.NumUser)
				.HasMaxLength(20)
				.HasColumnName("Num_User");

			entity.Property(e => e.Estado)
		   .HasColumnName("Estado")
		   .HasColumnType("bit");

			entity.HasOne(d => d.CodHotelNavigation).WithMany(p => p.Usuarios)
				.HasForeignKey(d => d.CodHotel)
				.HasConstraintName("FK_Usuario_Hotel");

			entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuarios)
				.HasForeignKey(d => d.IdRol)
				.HasConstraintName("FK_Usuario_Rol");
		});

		modelBuilder.Entity<Visitum>(entity =>
        {
            entity.HasKey(e => e.IdVisita).HasName("PK__Visita__78AC1091398B12AA");

            entity.Property(e => e.IdVisita).HasColumnName("Id_Visita");
            entity.Property(e => e.CodHotel).HasColumnName("cod_hotel");
            entity.Property(e => e.FechaIngreso)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Fecha_Ingreso");
            entity.Property(e => e.FechaNac).HasColumnName("Fecha_Nac");
            entity.Property(e => e.FechaSalida)
                .HasColumnType("datetime")
                .HasColumnName("Fecha_Salida");
            entity.Property(e => e.Nacionalidad).HasMaxLength(20);
            entity.Property(e => e.NumHabitacion)
                .HasMaxLength(5)
                .HasColumnName("Num_Habitacion");
            entity.Property(e => e.NumIdentificion)
                .HasMaxLength(30)
                .HasColumnName("Num_Identificion");
            entity.Property(e => e.PrimerApellido)
                .HasMaxLength(20)
                .HasColumnName("Primer_Apellido");
            entity.Property(e => e.PrimerNombre)
                .HasMaxLength(20)
                .HasColumnName("Primer_Nombre");
            entity.Property(e => e.SegundoApellido)
                .HasMaxLength(20)
                .HasColumnName("Segundo_Apellido");
            entity.Property(e => e.SegundoNombre)
                .HasMaxLength(20)
                .HasColumnName("Segundo_Nombre");
            entity.Property(e => e.Sexo).HasMaxLength(10);
            entity.Property(e => e.TipoIdentificion)
                .HasMaxLength(20)
                .HasColumnName("Tipo_Identificion");

            entity.HasOne(d => d.CodHotelNavigation).WithMany(p => p.Visita)
                .HasForeignKey(d => d.CodHotel)
                .HasConstraintName("FK_Visita_Hotel");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
