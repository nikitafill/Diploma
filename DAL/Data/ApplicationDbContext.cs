using DiplomaProject.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Experiment> Experiments { get; set; }
    public DbSet<ExperimentParameters> ExperimentParameters { get; set; }
    public DbSet<ExperimentResult> ExperimentResults { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<StudentAnswer> StudentAnswers { get; set; }
    public DbSet<Report> Reports { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Ограничение на роли пользователей
        /*modelBuilder.Entity<User>()
            .Property(u => u.Role)
            .HasDefaultValue("Student");*/

        // Один эксперимент → одни параметры
        modelBuilder.Entity<Experiment>()
            .HasOne(e => e.Parameters)
            .WithOne(p => p.Experiment)
            .HasForeignKey<ExperimentParameters>(p => p.ExperimentId)
            .OnDelete(DeleteBehavior.Cascade);

        // Один эксперимент → один результат
        modelBuilder.Entity<Experiment>()
            .HasOne(e => e.Result)
            .WithOne(r => r.Experiment)
            .HasForeignKey<ExperimentResult>(r => r.ExperimentId)
            .OnDelete(DeleteBehavior.Cascade);

        // Один эксперимент → один отчет
        modelBuilder.Entity<Experiment>()
            .HasOne(e => e.Report)
            .WithOne(r => r.Experiment)
            .HasForeignKey<Report>(r => r.ExperimentId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
