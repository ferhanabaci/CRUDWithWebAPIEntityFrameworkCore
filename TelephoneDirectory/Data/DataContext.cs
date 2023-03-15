using Microsoft.EntityFrameworkCore;

namespace TelephoneDirectory.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Person> Persons { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<TokenModel> Tokens { get; set; }
        public List<User> DummyUsers()
        {
            var users = new List<User>() {
            new User() { Id = 1, Password = "password1", UserName="Deneme1"},
            new User() { Id = 2, Password = "password2", UserName="Deneme2"},
            new User() { Id = 3, Password = "password3", UserName="Deneme3"},
            new User() { Id = 4, Password = "password4", UserName="Deneme4"},
            };
            return users;
        }

    }
}
