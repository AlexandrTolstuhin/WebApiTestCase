using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using WebApiTestCase.Data.Entities;
using WebApiTestCase.Data.Entities.Common;

namespace WebApiTestCase.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        { }

        public DbSet<TaskEntity> Tasks { get; set; }

        public DbSet<UserEntity> Users { get; set; }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            SetAuditValues();

            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override int SaveChanges()
        {
            SetAuditValues();

            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
        {
            SetAuditValues();

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            SetAuditValues();

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<UserEntity>()
                .HasMany(u => u.PerformerTasks)
                .WithOne(t => t.Performer);

            modelBuilder
                .Entity<UserEntity>()
                .HasMany(u => u.ProviderTasks)
                .WithOne(t => t.Provider);

            base.OnModelCreating(modelBuilder);
        }

        private void SetAuditValues()
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                var now = DateTime.Now;
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = now;
                        entry.Entity.ModifiedDate = now;
                        break;

                    case EntityState.Modified:
                        entry.Entity.ModifiedDate = now;
                        break;
                }
            }
        }
    }
}