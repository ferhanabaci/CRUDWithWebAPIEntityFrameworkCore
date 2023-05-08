using Microsoft.EntityFrameworkCore;

namespace TelephoneDirectory.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Person> Persons { get; set; }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<TokenModel> Tokens { get; set; }
       
    }
}
