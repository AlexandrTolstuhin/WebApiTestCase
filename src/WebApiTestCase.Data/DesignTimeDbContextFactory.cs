using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace WebApiTestCase.Data
{
    internal class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationContext>
    {
        public ApplicationContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>()
                .UseSqlServer("Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=W1TestCase;Integrated Security=True");

            return new ApplicationContext(optionsBuilder.Options);
        }
    }
}