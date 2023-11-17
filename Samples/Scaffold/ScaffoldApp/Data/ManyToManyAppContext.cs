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

    public virtual DbSet<Utilisateur> Utilisateurs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=Laptop-Work; Initial Catalog=ManyToManyApp; Integrated Security=True; TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Note>(entity =>
        {
            entity.ToTable("Note");

            entity.HasIndex(e => e.NoteId, "IX_Note_NoteId").IsUnique();

            entity.Property(e => e.Contenu).HasMaxLength(4000);
            entity.Property(e => e.DateCreation).HasPrecision(3);

            entity.HasOne(d => d.UtilisateurCreation).WithMany(p => p.Notes)
                .HasForeignKey(d => d.UtilisateurCreationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Note_UtilisateurCreation");
        });

        modelBuilder.Entity<PublicationOeuvre>(entity =>
        {
            entity.ToTable("PublicationOeuvre");

            entity.Property(e => e.Titre).HasMaxLength(250);

            entity.HasMany(d => d.Notes).WithMany(p => p.PublicationOeuvres)
                .UsingEntity<Dictionary<string, object>>(
                    "PublicationOeuvreNote",
                    r => r.HasOne<Note>().WithMany()
                        .HasForeignKey("NoteId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_PublicationOeuvreNote_Note"),
                    l => l.HasOne<PublicationOeuvre>().WithMany()
                        .HasForeignKey("PublicationOeuvreId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_PublicationOeuvreNote_PublicationOeuvre"),
                    j =>
                    {
                        j.HasKey("PublicationOeuvreId", "NoteId");
                        j.ToTable("PublicationOeuvreNote");
                    });
        });

        modelBuilder.Entity<Utilisateur>(entity =>
        {
            entity.ToTable("Utilisateur");

            entity.Property(e => e.Nom).HasMaxLength(60);
            entity.Property(e => e.Prenom).HasMaxLength(60);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
