using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace R401TP2.Models.EntityFramework;

public partial class SeriesDbContext : DbContext
{
    public SeriesDbContext()
    {
    }

    public SeriesDbContext(DbContextOptions<SeriesDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Serie> Series { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Server=localhost;port=5432;Database=SeriesDB;uid=postgres;password=postgres;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Serie>(entity =>
        {
            entity.HasKey(e => e.Serieid).HasName("serie_pkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
