using Microsoft.EntityFrameworkCore;

namespace DataAccess.Context
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options)
           : base(options)
        {
            this.ChangeTracker.LazyLoadingEnabled = false;
        }

        public DBContext()
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DBContext).Assembly);
            base.OnModelCreating(modelBuilder);

            foreach (var item in DataAccessDataConfigurations.Instance.Configurations())
            {
                modelBuilder.ApplyConfiguration(item);
            }
        }
    }
}
