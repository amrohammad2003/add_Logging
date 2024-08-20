using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Efcore.Configuration
{
    public class DrinkConfiguration : IEntityTypeConfiguration<Drink>
    {
        public void Configure(EntityTypeBuilder<Drink> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                   .UseIdentityColumn();

            builder.Property(x => x.Name)
                .HasColumnType("VARCHAR")
                .HasMaxLength(255).IsRequired();

            builder.Property(x => x.Price)
                .HasPrecision(15, 2);

            builder.Property(x => x.Size)
                .HasPrecision(15, 2);


            builder.ToTable("Drinks");

            // Seed initial data
            builder.HasData(LoadDrinks());
        }

        private static List<Drink> LoadDrinks()
        {
            return new List<Drink>
            {
                new Drink { Id = 1, Name = "Coffee", Price = 1000m,Size="Large"},
                new Drink { Id = 2, Name = "Tea", Price = 2000m,Size= "XLarge"},
                new Drink { Id = 3, Name = "Juice", Price = 1500m,Size= "XXLarge"},
                new Drink { Id = 4, Name = "Water", Price = 1200m,Size= "Medeuim"},
                new Drink { Id = 5, Name = "Smoothie", Price = 3000m,Size= "Small"},
            };
        }
    }
}
