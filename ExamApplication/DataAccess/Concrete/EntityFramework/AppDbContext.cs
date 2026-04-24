using ExamApplication.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace ExamApplication.DataAccess.Concrete.EntityFramework
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Exam> Exams { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ── SUBJECT ──────────────────────────────────────────────
            modelBuilder.Entity<Subject>(entity =>
            {
                entity.HasKey(s => s.Id);

                entity.Property(s => s.Code)
                      .HasColumnType("char(3)")
                      .IsRequired();

                entity.HasIndex(s => s.Code)
                      .IsUnique();

                entity.Property(s => s.Name)
                      .HasMaxLength(30)
                      .IsRequired();

                entity.Property(s => s.Class)
                      .HasColumnType("numeric(2,0)")
                      .IsRequired();

                entity.Property(s => s.TeacherFirstName)
                      .HasMaxLength(20)
                      .IsRequired();

                entity.Property(s => s.TeacherLastName)
                      .HasMaxLength(20)
                      .IsRequired();
            });

            // ── STUDENT ──────────────────────────────────────────────
            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(s => s.Id);

                entity.Property(s => s.Number)
                      .HasColumnType("numeric(5,0)")
                      .IsRequired();

                entity.HasIndex(s => s.Number)
                      .IsUnique();

                entity.Property(s => s.FirstName)
                      .HasMaxLength(30)
                      .IsRequired();

                entity.Property(s => s.LastName)
                      .HasMaxLength(30)
                      .IsRequired();

                entity.Property(s => s.Class)
                      .HasColumnType("numeric(2,0)")
                      .IsRequired();
            });

            // ── EXAM ─────────────────────────────────────────────────
            modelBuilder.Entity<Exam>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.SubjectCode)
                      .HasColumnType("char(3)")
                      .IsRequired();

                entity.Property(e => e.StudentNumber)
                      .HasColumnType("numeric(5,0)")
                      .IsRequired();

                entity.Property(e => e.ExamDate)
                      .HasColumnType("date")
                      .IsRequired();

                entity.Property(e => e.Grade)
                      .HasColumnType("numeric(1,0)")
                      .IsRequired();

                // Optional: prevent duplicate exam entries
                entity.HasIndex(e => new { e.SubjectCode, e.StudentNumber })
                      .IsUnique();

                // FK → Subject (via Code, not Id)
                entity.HasOne(e => e.Subject)
                      .WithMany(s => s.Exams)
                      .HasPrincipalKey(s => s.Code)
                      .HasForeignKey(e => e.SubjectCode)
                      .OnDelete(DeleteBehavior.Restrict);

                // FK → Student (via Number, not Id)
                entity.HasOne(e => e.Student)
                      .WithMany(s => s.Exams)
                      .HasPrincipalKey(s => s.Number)
                      .HasForeignKey(e => e.StudentNumber)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // ── USER ─────────────────────────────────────────────────
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);

                entity.Property(u => u.UserName)
                      .HasMaxLength(50)
                      .IsRequired();

                entity.HasIndex(u => u.UserName)
                      .IsUnique();

                entity.Property(u => u.PasswordHash).IsRequired();
                entity.Property(u => u.PasswordSalt).IsRequired();
            });
        }
    }
}
