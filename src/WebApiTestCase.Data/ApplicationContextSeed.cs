using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiTestCase.Common.Extensions;
using WebApiTestCase.Core.Enums;
using WebApiTestCase.Data.Entities;
using TaskStatus = WebApiTestCase.Core.Enums.TaskStatus;

namespace WebApiTestCase.Data
{
    public static class ApplicationContextSeed
    {
        public static async Task SeedDatabaseAsync(this ApplicationContext context)
        {
            if (await context.Users.AnyAsync().ConfigureAwait(false)) return;

            var users = await SeedUsersAsync(context).ConfigureAwait(false);

            await SeedTasksAsync(context, users).ConfigureAwait(false);

            await context.SaveChangesAsync().ConfigureAwait(false);
        }

        private static async Task<UserEntity[]> SeedUsersAsync(ApplicationContext context)
        {
            const int userCount = 5;

            var users = Enumerable.Range(1, userCount)
                .Select(i => new UserEntity
                {
                    FirstName = $"FirstName {i}",
                    LastName = $"LastName {i}",
                    Status = UserStatus.Active
                })
                .ToArray();

            await context.Users.AddRangeAsync(users).ConfigureAwait(false);

            return users;
        }

        private static async Task<TaskEntity[]> SeedTasksAsync(
            ApplicationContext context,
            IList<UserEntity> users)
        {
            const int taskCount = 24;

            var random = new Random(42);

            var tasks = Enumerable.Range(1, taskCount)
                .Select(i => new TaskEntity
                {
                    Name = $"Task #{i}",
                    Description = $"Task #{i} description",
                    Status = TaskStatus.NotStarted,
                    Provider = random.NextItem(users),
                    Performer = random.NextItem(users)
                })
                .ToArray();

            await context.Tasks.AddRangeAsync(tasks).ConfigureAwait(false);

            return tasks;
        }
    }
}