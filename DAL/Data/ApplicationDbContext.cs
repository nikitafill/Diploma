using DiplomaProject.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    public DbSet<User> Users { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<StudentAnswer> StudentAnswers { get; set; }
    public DbSet<RingRadius> RingsRadiuses { get; set; }
    public DbSet<VideoAnalysis> VideoAnalyses { get; set; }
    public DbSet<ExperimentGroup> ExperimentGroups { get; set; }
    public DbSet<ExperimentGroupQuestion> ExperimentGroupQuestions { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
