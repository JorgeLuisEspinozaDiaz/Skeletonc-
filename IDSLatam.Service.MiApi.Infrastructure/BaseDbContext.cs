using IDSLatam.Common.Application.Helpers;
using IDSLatam.Common.Core.Base;
using IDSLatam.Service.MiApi.Core.Entities;
using IDSLatam.Service.MiApi.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;

namespace IDSLatam.Service.MiApi.Infrastructure
{
    public class BaseDbContext : DbContext
    {

        private readonly DateTimeHelper _helper = new DateTimeHelper();
        public BaseDbContext(DbContextOptions<BaseDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("ca");
            ModelConfig(modelBuilder);
        }
        private void ModelConfig(ModelBuilder modelBuilder)
        {
            new TestConfiguration(modelBuilder.Entity<Test>());
            modelBuilder.Entity<Test>().ToTable("Tests");

        }

        public DbSet<Test> Tests { get; set; }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<EntityBase>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = _helper.DateTimePst();
                        entry.Entity.Modified = _helper.DateTimePst();
                        break;
                    case EntityState.Modified:
                        entry.Entity.Modified = _helper.DateTimePst();
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);

        }

    }
}