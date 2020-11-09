using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApiTestCase.Core.Enums;
using WebApiTestCase.Data;
using WebApiTestCase.Data.Entities;
using TaskStatus = WebApiTestCase.Core.Enums.TaskStatus;

namespace WebApiTestCase.Application.Test.Services
{
    public abstract class ApplicationContextServiceTest
    {
        protected ApplicationContext Context;

        protected virtual void Setup()
        {
            var databaseName = Guid.NewGuid().ToString("N");

            Debug.WriteLine(databaseName);

            var builder = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName);

            Context = new ApplicationContext(builder.Options);
        }

        protected async Task SeedContextAsync()
        {
            var users = await SeedUsersAsync();

            await SeedTasksAsync(users);

            await Context.SaveChangesAsync();
        }

        private async Task<UserEntity[]> SeedUsersAsync()
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

            await Context.Users.AddRangeAsync(users);

            return users;
        }

        private async Task SeedTasksAsync(IList<UserEntity> users)
        {
            var counter = 1;

            foreach (var provider in users)
                foreach (var performer in users)
                    await Context.Tasks.AddAsync(new TaskEntity
                    {
                        Name = $"Task #{counter}",
                        Description = $"Task #{counter++} description",
                        Status = TaskStatus.NotStarted,
                        Provider = provider,
                        Performer = performer
                    });
        }
    }
}