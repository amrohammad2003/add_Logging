using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Efcore.Configuration
{
    public class FoodConfiguration : IEntityTypeConfiguration<Food>
    {

        public void Configure(EntityTypeBuilder<Food> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                   .UseIdentityColumn();

            builder.Property(x => x.Name)
                .HasColumnType("VARCHAR")
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(x => x.Price)
                .HasPrecision(15, 2);

            builder.ToTable("Foods");

            builder.HasData(LoadCourses());
        }

        private static List<Food> LoadCourses()
        {
            return new List<Food>
            {
                new Food { Id = 1, Name = " Falafel", Price = 1000m},
                new Food { Id = 2, Name = "Fish", Price = 2000m},
                new Food { Id = 3, Name = "Makloba", Price = 1500m},
                new Food { Id = 4, Name = "Sushi", Price = 1200m},
                new Food { Id = 5, Name = "Homos", Price = 3000m},
            };
        }
    }
}