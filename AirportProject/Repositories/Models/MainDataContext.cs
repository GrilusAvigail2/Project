using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Repositories.Models
{
    public partial class MainDataContext : DbContext
    {
        public MainDataContext()
        {
        }

        public MainDataContext(DbContextOptions<MainDataContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Arms> Arms { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<DesignedFor> DesignedFor { get; set; }
        public virtual DbSet<Edges> Edges { get; set; }
        public virtual DbSet<Points> Points { get; set; }
        public virtual DbSet<Structures> Structures { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=F:\\AirportProject\\DB\\MainData.mdf;Integrated Security=True;Connect Timeout=30");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Arms>(entity =>
            {
                entity.HasKey(e => e.ArmId)
                    .HasName("PK__Arms__341D93A3B607302B");

                entity.Property(e => e.ArmId)
                    .HasColumnName("armId")
                    .ValueGeneratedNever();

                entity.Property(e => e.ArmName)
                    .IsRequired()
                    .HasColumnName("armName")
                    .HasMaxLength(5);

                entity.Property(e => e.FloorNumber).HasColumnName("floorNumber");

                entity.Property(e => e.StructureId).HasColumnName("structureId");

                entity.HasOne(d => d.Structure)
                    .WithMany(p => p.Arms)
                    .HasForeignKey(d => d.StructureId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Arms_ToTable");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.CategoryId)
                    .HasColumnName("categoryId")
                    .ValueGeneratedNever();

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasColumnName("categoryName")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<DesignedFor>(entity =>
            {
                entity.Property(e => e.DesignedForId)
                    .HasColumnName("designedForId")
                    .ValueGeneratedNever();

                entity.Property(e => e.DesignedForDescription)
                    .IsRequired()
                    .HasColumnName("designedForDescription")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Edges>(entity =>
            {
                entity.HasKey(e => e.EdgeId)
                    .HasName("PK__Edges__3214EC07EF896DC8");

                entity.Property(e => e.EdgeId)
                    .HasColumnName("edgeId")
                    .ValueGeneratedNever();

                entity.Property(e => e.SourceId).HasColumnName("sourceId");

                entity.Property(e => e.TargetId).HasColumnName("targetId");

                entity.Property(e => e.Weight).HasColumnName("weight");
            });

            modelBuilder.Entity<Points>(entity =>
            {
                entity.HasKey(e => e.PointId)
                    .HasName("PK__Points__3214EC07F36525C9");

                entity.Property(e => e.PointId)
                    .HasColumnName("pointId")
                    .ValueGeneratedNever();

                entity.Property(e => e.ArmId).HasColumnName("armId");

                entity.Property(e => e.CategoryId).HasColumnName("categoryId");

                entity.Property(e => e.PointName)
                    .IsRequired()
                    .HasColumnName("pointName")
                    .HasMaxLength(50);

                entity.HasOne(d => d.Arm)
                    .WithMany(p => p.Points)
                    .HasForeignKey(d => d.ArmId)
                    .HasConstraintName("FK_Points_ToTable_1");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Points)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Points_ToTable");
            });

            modelBuilder.Entity<Structures>(entity =>
            {
                entity.HasKey(e => e.StructureId)
                    .HasName("PK__Structur__9A9277054613E95A");

                entity.Property(e => e.StructureId)
                    .HasColumnName("structureId")
                    .ValueGeneratedNever();

                entity.Property(e => e.DesignedForId).HasColumnName("designedForId");

                entity.Property(e => e.FloorNumber).HasColumnName("floorNumber");

                entity.Property(e => e.StructureName)
                    .IsRequired()
                    .HasColumnName("structureName")
                    .HasMaxLength(50);

                entity.HasOne(d => d.DesignedFor)
                    .WithMany(p => p.Structures)
                    .HasForeignKey(d => d.DesignedForId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Structures_ToTable");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
