using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OnlineExammination.Domain.Entities;

namespace OnlineExammination.Persistence.Data
{
    public class OnlineExamminationDbContext:DbContext
    {
        public OnlineExamminationDbContext(DbContextOptions options):base(options)
        {
           

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationShip in modelBuilder.Model.GetEntityTypes().SelectMany(x => x.GetForeignKeys()))
            {
                relationShip.DeleteBehavior = DeleteBehavior.Restrict;

            }
        }
        #region Tables


        public DbSet<User> Users { get; set; }
        public DbSet<Program> Programs { get; set; }
        public DbSet<Semester> Semesters { get; set; }
        public DbSet<QuestionPaper> Papers { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<Result> Results { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<AppFile> AppFiles { get; set; }


        #endregion Tables
    }
}
