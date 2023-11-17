using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ScaffoldApp.Models;

namespace ScaffoldApp.Data;

public partial class ManyToManyAppContext : DbContext
{
    public ManyToManyAppContext()
    {
    }

    public ManyToManyAppContext(DbContextOptions<ManyToManyAppContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Note> Notes { get; set; }

    public virtual DbSet<PublicationOeuvre> PublicationOeuvres { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=Laptop-Work; Initial Catalog=ManyToManyApp; Integrated Security=True; TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Note>(entity =>
        {
            entity.ToTable("Note");
        });

        modelBuilder.Entity<PublicationOeuvre>(entity =>
        {
            entity.ToTable("PublicationOeuvre");

            entity.HasMany(d => d.Notes).WithMany(p => p.PublicationOeuvres)
                .UsingEntity<Dictionary<string, object>>(
                    "PublicationOeuvreNote",
                    r => r.HasOne<Note>().WithMany().HasForeignKey("NoteId"),
                    l => l.HasOne<PublicationOeuvre>().WithMany().HasForeignKey("PublicationOeuvreId"),
                    j =>
                    {
                        j.HasKey("PublicationOeuvreId", "NoteId");
                        j.ToTable("PublicationOeuvreNote");
                        j.HasIndex(new[] { "NoteId" }, "IX_PublicationOeuvreNote_NoteId");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
