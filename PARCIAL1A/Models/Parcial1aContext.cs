using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PARCIAL1A.Models;

public partial class Parcial1aContext : DbContext
{
    public Parcial1aContext()
    {
    }

    public Parcial1aContext(DbContextOptions<Parcial1aContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AutorLibro> AutorLibros { get; set; }

    public virtual DbSet<Autor> Autores { get; set; }

    public virtual DbSet<Libro> Libros { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source = LOCALHOST; Initial Catalog = PARCIAL1A; User id = Diego; Password = admin; Connect Timeout = 120; Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AutorLibro>(entity =>
        {
            entity.HasKey(e => new { e.AutorId, e.LibroId }).HasName("PK__AutorLib__36D0F7E7540D7E6E");

            entity.ToTable("AutorLibro");
        });

        modelBuilder.Entity<Autor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Autores__3214EC0721DC0A11");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Libro>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Libros__3214EC0764F9822E");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Titulo)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Posts__3214EC079ED6965E");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Contenido)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FechaPublicacion).HasColumnType("datetime");
            entity.Property(e => e.Titulo)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
