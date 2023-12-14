using System;
using System.Collections.Generic;
using Entidades.Clases;
using Microsoft.EntityFrameworkCore;

namespace BDEventos.Models;

public partial class BDEventoContext : DbContext
{
    public BDEventoContext()
    {
    }

    public BDEventoContext(DbContextOptions<BDEventoContext> options)
        : base(options)
    {

    }

    public virtual DbSet<Evento> Eventos { get; set; }

    public virtual DbSet<Grupo> Grupos { get; set; }

    public virtual DbSet<Invitado> Invitados { get; set; }

    public virtual DbSet<Menu> Menus { get; set; }

    public virtual DbSet<Mesa> Mesas { get; set; }

    public virtual DbSet<OpcionMenu> OpcionMenus { get; set; }

    public virtual DbSet<Perfil> Perfils { get; set; }

    public virtual DbSet<Permiso> Permisos { get; set; }

    public virtual DbSet<TipoEvento> TipoEventos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }


    public DbSet<UspConsultarTipoEvento> UspConsultarTipoEventos { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Modern_Spanish_CI_AS");

        modelBuilder.Entity<Evento>(entity =>
        {
            entity.HasKey(e => e.Idevento).HasName("PK__Evento__C8A2BCFE6A1C5CE2");

            entity.ToTable("Evento", "EVENTO");

            entity.Property(e => e.Idevento).HasColumnName("idevento");
            entity.Property(e => e.Comentario)
                .HasColumnType("text")
                .HasColumnName("comentario");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValueSql("('ACTIVO')")
                .HasColumnName("estado");
            entity.Property(e => e.Fecha)
                .HasColumnType("datetime")
                .HasColumnName("fecha");
            entity.Property(e => e.IdtipoEvento).HasColumnName("idtipo_evento");
            entity.Property(e => e.Idusuario).HasColumnName("idusuario");
            entity.Property(e => e.Nombre)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Presupuesto)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("presupuesto");

            /*
            entity.HasOne(d => d.IdtipoEventoNavigation).WithMany(p => p.Eventos)
                .HasForeignKey(d => d.IdtipoEvento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_idtipo_evento");
            */
        });

        modelBuilder.Entity<Grupo>(entity =>
        {
            entity.HasKey(e => e.Idgrupo).HasName("PK__Grupo__F8D5E6CE5B37D5D7");

            entity.ToTable("Grupo", "EVENTO");

            entity.Property(e => e.Idgrupo).HasColumnName("idgrupo");
            entity.Property(e => e.Comentario)
                .HasColumnType("text")
                .HasColumnName("comentario");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValueSql("('ACTIVO')")
                .HasColumnName("estado");
            entity.Property(e => e.Idevento).HasColumnName("idevento");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");

            /*entity.HasOne(d => d.IdeventoNavigation).WithMany(p => p.Grupos)
                .HasForeignKey(d => d.Idevento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_idevento_grupo");
            */
        });

        modelBuilder.Entity<Invitado>(entity =>
        {
            entity.HasKey(e => e.Idinvitado).HasName("PK__Invitado__954BCA54D48F7F42");

            entity.ToTable("Invitado", "EVENTO");

            entity.Property(e => e.Idinvitado).HasColumnName("idinvitado");
            entity.Property(e => e.Apellidos)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("apellidos");
            entity.Property(e => e.Comentario)
                .HasColumnType("text")
                .HasColumnName("comentario");
            entity.Property(e => e.Correo)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("correo");
            entity.Property(e => e.Direccion)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("direccion");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValueSql("('ACTIVO')")
                .HasColumnName("estado");
            entity.Property(e => e.Idevento).HasColumnName("idevento");
            entity.Property(e => e.Idgrupo).HasColumnName("idgrupo");
            entity.Property(e => e.Idmenu).HasColumnName("idmenu");
            entity.Property(e => e.Idmesa).HasColumnName("idmesa");
            entity.Property(e => e.InvitacionEnviada)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("invitacion_enviada");
            entity.Property(e => e.Nombres)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombres");
            entity.Property(e => e.Sexo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("sexo");
            entity.Property(e => e.Telefono)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("telefono");
            entity.Property(e => e.TipoInvitado)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tipo_invitado");

            /*
            entity.HasOne(d => d.IdeventoNavigation).WithMany(p => p.Invitados)
                .HasForeignKey(d => d.Idevento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_idevento");

            entity.HasOne(d => d.IdgrupoNavigation).WithMany(p => p.Invitados)
                .HasForeignKey(d => d.Idgrupo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_idgrupo");

            entity.HasOne(d => d.IdmenuNavigation).WithMany(p => p.Invitados)
                .HasForeignKey(d => d.Idmenu)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_idmenu");

            entity.HasOne(d => d.IdmesaNavigation).WithMany(p => p.Invitados)
                .HasForeignKey(d => d.Idmesa)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_idmesa");
            */
        });

        modelBuilder.Entity<Menu>(entity =>
        {
            entity.HasKey(e => e.Idmenu).HasName("PK__Menu__753CC8502761DAD0");

            entity.ToTable("Menu", "EVENTO");

            entity.Property(e => e.Idmenu).HasColumnName("idmenu");
            entity.Property(e => e.Comentario)
                .HasColumnType("text")
                .HasColumnName("comentario");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValueSql("('ACTIVO')")
                .HasColumnName("estado");
            entity.Property(e => e.Idevento).HasColumnName("idevento");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");

            /*
             * entity.HasOne(d => d.IdeventoNavigation).WithMany(p => p.Menus)
                .HasForeignKey(d => d.Idevento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_idevento_menu");
            */
        });

        modelBuilder.Entity<Mesa>(entity =>
        {
            entity.HasKey(e => e.Idmesa).HasName("PK__Mesa__753C7295BC306EE7");

            entity.ToTable("Mesa", "EVENTO");

            entity.Property(e => e.Idmesa).HasColumnName("idmesa");
            entity.Property(e => e.Comentario)
                .HasColumnType("text")
                .HasColumnName("comentario");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValueSql("('ACTIVO')")
                .HasColumnName("estado");
            entity.Property(e => e.Idevento).HasColumnName("idevento");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.NumeroInvitados).HasColumnName("numero_invitados");
            entity.Property(e => e.TipoInvitado)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tipo_invitado");
            entity.Property(e => e.TipoMesa)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tipo_mesa");

            /*
            entity.HasOne(d => d.IdeventoNavigation).WithMany(p => p.Mesas)
                .HasForeignKey(d => d.Idevento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_idevento_mesa");
            */
        });

        modelBuilder.Entity<OpcionMenu>(entity =>
        {
            entity.HasKey(e => e.IdopcionMenu).HasName("PK__OpcionMe__A0F6FA1F2E6D3B36");

            entity.ToTable("OpcionMenu", "SESION");

            entity.Property(e => e.IdopcionMenu).HasColumnName("idopcion_menu");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValueSql("('ACTIVO')")
                .HasColumnName("estado");
            entity.Property(e => e.IdopcionMenuRef).HasColumnName("idopcion_menu_ref");
            entity.Property(e => e.Link)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("link");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Perfil>(entity =>
        {
            entity.HasKey(e => e.Idperfil).HasName("PK__Perfil__02F50746BC806C1A");

            entity.ToTable("Perfil", "SESION");

            entity.Property(e => e.Idperfil).HasColumnName("idperfil");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValueSql("('ACTIVO')")
                .HasColumnName("estado");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Permiso>(entity =>
        {
            entity.HasKey(e => e.Idpermiso).HasName("PK__Permiso__85C7F900EDABAB0B");

            entity.ToTable("Permiso", "SESION");

            entity.Property(e => e.Idpermiso).HasColumnName("idpermiso");
            entity.Property(e => e.IdopcionMenu).HasColumnName("idopcion_menu");
            entity.Property(e => e.Idperfil).HasColumnName("idperfil");


            /*
            entity.HasOne(d => d.IdopcionMenuNavigation).WithMany(p => p.Permisos)
                .HasForeignKey(d => d.IdopcionMenu)
                .HasConstraintName("FK_idopcion_mene_permiso");

            
            entity.HasOne(d => d.IdperfilNavigation).WithMany(p => p.Permisos)
                .HasForeignKey(d => d.Idperfil)
                .HasConstraintName("FK_idperfil_permiso");
            */
        });

        modelBuilder.Entity<TipoEvento>(entity =>
        {
            entity.HasKey(e => e.IdtipoEvento).HasName("PK__TipoEven__ECEB46ED4F3C0893");

            entity.ToTable("TipoEvento", "EVENTO");

            entity.Property(e => e.IdtipoEvento).HasColumnName("idtipo_evento");
            entity.Property(e => e.Comentario)
                .HasColumnType("text")
                .HasColumnName("comentario");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValueSql("('ACTIVO')")
                .HasColumnName("estado");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Idusuario).HasName("PK__Usuario__080A9743C7CA468F");

            entity.ToTable("Usuario", "SESION");

            entity.Property(e => e.Idusuario).HasColumnName("idusuario");
            entity.Property(e => e.Clave)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("clave");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValueSql("('ACTIVO')")
                .HasColumnName("estado");
            entity.Property(e => e.Idperfil).HasColumnName("idperfil");
            entity.Property(e => e.Usuario1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("usuario");

            /*
            entity.HasOne(d => d.IdperfilNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.Idperfil)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_idperfil");
            */
        });

        // Configurar el procedimiento almacenado
        modelBuilder.Entity<UspConsultarTipoEvento>().HasNoKey().ToView(null);

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
