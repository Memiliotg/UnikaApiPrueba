using Microsoft.EntityFrameworkCore;
using UnikaApiPrueba.Models;

namespace UnikaApiPrueba.Helper
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext (DbContextOptions<DataBaseContext> options) : base(options) { }

        public DbSet<Person> Persons { get; set; }
    }
}
