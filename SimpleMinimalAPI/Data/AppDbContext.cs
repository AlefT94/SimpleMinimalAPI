using Microsoft.EntityFrameworkCore;
using SimpleMinimalAPI.Students;

namespace SimpleMinimalAPI.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;port=3306;userid=root;password=1234;database=simpleMinimalAPI;");

            //Show log in console
            optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
            //Enable parameter created in EntityFramework
            optionsBuilder.EnableSensitiveDataLogging();

            base.OnConfiguring(optionsBuilder);
        }
    }
}
