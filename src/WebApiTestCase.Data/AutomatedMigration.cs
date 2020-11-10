using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace WebApiTestCase.Data
{
    public static class AutomatedMigration
    {
        public static async Task DatabaseMigrateAsync(this IServiceProvider services)
        {
            var context = services.GetRequiredService<ApplicationContext>();

            if (context.Database.IsSqlServer())
                context.Database.Migrate();

            await context.SeedDatabaseAsync();
        }
    }
}