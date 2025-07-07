using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FGC_PWA2025.Models;

public partial class PeliculasDbContext : DbContext
{
    public PeliculasDbContext()
    {
    }

    public PeliculasDbContext(DbContextOptions<PeliculasDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Pelicula> Peliculas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Pelicula>(entity =>
        {

            entity.HasKey(e => e.ID);
            entity.Property(e => e.ID).UseIdentityColumn();
            entity.ToTable("peliculas");

            entity.Property(e => e.Costo)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("costo");
            entity.Property(e => e.Duracion)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("duracion");
            entity.Property(e => e.Fecha).HasColumnName("fecha");
            entity.Property(e => e.Genero)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("genero");
            entity.Property(e => e.Titulo)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("titulo");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
