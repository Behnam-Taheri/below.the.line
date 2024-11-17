using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Query.DataModel.Models;

public partial class BTLDBScaffoldContext : DbContext
{
    private readonly string ConnectionString;
    public BTLDBScaffoldContext(string  connectionString)
    {
        ConnectionString = connectionString;
    }

    public BTLDBScaffoldContext(DbContextOptions<BTLDBScaffoldContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Discount> Discounts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(ConnectionString);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Discount>(entity =>
        {
            entity.ToTable("Discount");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
