using System;
using System.Collections.Generic;
using Ejercicio3.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Ejercicio3.Models;

public partial class PracticaCSharpContext : DbContext
{
    
    // Contexto de la base de datos generado por Scaffold-DbContext.
    
    public PracticaCSharpContext()
    {
    }

    public PracticaCSharpContext(DbContextOptions<PracticaCSharpContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Alumno> Alumnos { get; set; }

    public virtual DbSet<Instructor> Instructors { get; set; }

    public virtual DbSet<Persona> Personas { get; set; }

    public virtual DbSet<Reunion> Reunions { get; set; }
    //public virtual DbSet<AlumnoReunion> AlumnoReunions { get; set; }
    public DbSet<AlumnoReunionView> AlumnoReunionViews { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost; User Id=sa; Password=hola!1234; Database=practica_CSharp; TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Alumno>(entity =>
        {
            entity.HasKey(e => e.IdPersona).HasName("PK__alumno__228148B08A4928EE");

            entity.ToTable("alumno");

            entity.Property(e => e.IdPersona)
                .ValueGeneratedNever()
                .HasColumnName("id_persona");
            entity.Property(e => e.Carrera)
                .HasMaxLength(64)
                .HasColumnName("carrera");
            entity.Property(e => e.Especialidad)
                .HasMaxLength(64)
                .HasColumnName("especialidad");
            entity.Property(e => e.Matricula)
                .HasMaxLength(64)
                .HasColumnName("matricula");
            entity.Property(e => e.Semestre)
                .HasMaxLength(64)
                .HasColumnName("semestre");

            entity.HasOne(d => d.IdPersonaNavigation).WithOne(p => p.Alumno)
                .HasForeignKey<Alumno>(d => d.IdPersona)
                .HasConstraintName("FK_Alumno_Persona");

            entity.HasMany(d => d.IdReunions).WithMany(p => p.IdAlumnos)
                .UsingEntity<Dictionary<string, object>>(
                    "AlumnoReunion",
                    r => r.HasOne<Reunion>().WithMany()
                        .HasForeignKey("IdReunion")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK2_Alumno_Reunion"),
                    l => l.HasOne<Alumno>().WithMany()
                        .HasForeignKey("IdAlumno")
                        .HasConstraintName("FK1_Alumno_Reunion"),
                    j =>
                    {
                        j.HasKey("IdAlumno", "IdReunion").HasName("PK__alumno_r__48581939C4534228");
                        j.ToTable("alumno_reunion");
                        j.IndexerProperty<int>("IdAlumno").HasColumnName("id_alumno");
                        j.IndexerProperty<int>("IdReunion").HasColumnName("id_reunion");
                    });
        });

        modelBuilder.Entity<Instructor>(entity =>
        {
            entity.HasKey(e => e.IdPersona).HasName("PK__instruct__228148B03C741CF9");

            entity.ToTable("instructor");

            entity.Property(e => e.IdPersona)
                .ValueGeneratedNever()
                .HasColumnName("id_persona");
            entity.Property(e => e.Folio)
                .HasMaxLength(64)
                .HasColumnName("folio");

            entity.HasOne(d => d.IdPersonaNavigation).WithOne(p => p.Instructor)
                .HasForeignKey<Instructor>(d => d.IdPersona)
                .HasConstraintName("FK_Instructor_Persona");

            entity.HasMany(d => d.IdReunions).WithMany(p => p.IdInstructors)
                .UsingEntity<Dictionary<string, object>>(
                    "InstructorReunion",
                    r => r.HasOne<Reunion>().WithMany()
                        .HasForeignKey("IdReunion")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK2_Instructor_Reunion"),
                    l => l.HasOne<Instructor>().WithMany()
                        .HasForeignKey("IdInstructor")
                        .HasConstraintName("FK1_Instructor_Reunion"),
                    j =>
                    {
                        j.HasKey("IdInstructor", "IdReunion").HasName("PK__instruct__39E3F2DA1D4C2CFF");
                        j.ToTable("instructor_reunion");
                        j.IndexerProperty<int>("IdInstructor").HasColumnName("id_instructor");
                        j.IndexerProperty<int>("IdReunion").HasColumnName("id_reunion");
                    });
        });

        modelBuilder.Entity<Persona>(entity =>
        {
            entity.HasKey(e => e.IdPersona).HasName("PK__persona__228148B0381F0B1D");

            entity.ToTable("persona");

            entity.Property(e => e.IdPersona).HasColumnName("id_persona");
            entity.Property(e => e.ApellidoDos)
                .HasMaxLength(64)
                .HasColumnName("apellido_dos");
            entity.Property(e => e.ApellidoUno)
                .HasMaxLength(64)
                .HasColumnName("apellido_uno");
            entity.Property(e => e.DNacimiento)
                .HasColumnType("date")
                .HasColumnName("D_nacimiento");
            entity.Property(e => e.NombreDos)
                .HasMaxLength(64)
                .HasColumnName("nombre_dos");
            entity.Property(e => e.NombreUno)
                .HasMaxLength(64)
                .HasColumnName("nombre_uno");
            entity.Property(e => e.TipoRol)
                .HasMaxLength(64)
                .HasColumnName("tipo_rol");
        });

        modelBuilder.Entity<Reunion>(entity =>
        {
            entity.HasKey(e => e.IdReunion).HasName("PK__reunion__52FBEC833DEAA20C");

            entity.ToTable("reunion");

            entity.Property(e => e.IdReunion).HasColumnName("id_reunion");
            entity.Property(e => e.Fecha)
                .HasColumnType("date")
                .HasColumnName("fecha");
            entity.Property(e => e.Hora).HasColumnName("hora");
            entity.Property(e => e.Lugar)
                .HasMaxLength(100)
                .HasColumnName("lugar");
            entity.Property(e => e.Tema)
                .HasMaxLength(200)
                .HasColumnName("tema");
        });
        
        /*modelBuilder.Entity<AlumnoReunion>(entity =>
        {
            entity.HasKey(e => new { e.IdAlumno, e.IdReunion }).HasName("PK_alumno_reunion");

            entity.ToTable("alumno_reunion");

            entity.Property(e => e.IdAlumno).HasColumnName("id_alumno");
            entity.Property(e => e.IdReunion).HasColumnName("id_reunion");

            entity.HasOne(d => d.IdAlumnoNavigation)
                .WithMany(p => p.AlumnoReunions)
                .HasForeignKey(d => d.IdAlumno)
                .HasConstraintName("FK1_Alumno_Reunion");

            entity.HasOne(d => d.IdReunionNavigation)
                .WithMany(p => p.AlumnoReunions)
                .HasForeignKey(d => d.IdReunion)
                .HasConstraintName("FK2_Alumno_Reunion");
        });*/
        
        modelBuilder.Entity<AlumnoReunionView>().ToView("AlumnoReunionView").HasKey(v => v.id_persona);

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
