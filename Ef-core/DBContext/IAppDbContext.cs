using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Efcore.DBContext
{
    public interface IAppDbContext
    {
        DbSet<Drink> Drinks { get; set; }
        DbSet<Food> Foods { get; set; }
        // Add other members you need to expose
    }
}
