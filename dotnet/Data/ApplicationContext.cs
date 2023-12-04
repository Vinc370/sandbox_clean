using dotnet.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }

        public DbSet<Database> users { get; set; }
        public DbSet<UserData> datas { get; set; }
        public DbSet<Person> person { get; set; }
    }
}
