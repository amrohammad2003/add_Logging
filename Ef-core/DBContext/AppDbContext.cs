using Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace Efcore.DBContext

{
    public class AppDbContext : DbContext 
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
        }

        public DbSet<Drink> Drinks { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<LogEntry> Errors { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<LogEntry>().ToTable("Errors"); // Ensure this matches your table name

            //modelBuilder.ApplyConfiguration(new DrinkConfiguration());
            //modelBuilder.ApplyConfiguration(new FoodConfiguration());

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }

    
}