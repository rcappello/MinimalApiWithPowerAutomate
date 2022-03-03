using MinimalApiWithPowerAutomate.API.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace MinimalApiWithPowerAutomate.API.DataAccessLayer
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public virtual DbSet<Address> Address { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<CustomerAddress> CustomerAddress { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductCategory> ProductCategory { get; set; }
        public virtual DbSet<ProductDescription> ProductDescription { get; set; }
        public virtual DbSet<ProductModel> ProductModel { get; set; }
        public virtual DbSet<ProductModelProductDescription> ProductModelProductDescription { get; set; }
        public virtual DbSet<SalesOrderDetail> SalesOrderDetail { get; set; }
        public virtual DbSet<SalesOrderHeader> SalesOrderHeader { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<T>(entity =>
            //{
            //    entity.HasOne(e => e.Property);
            //});

            base.OnModelCreating(modelBuilder);
        }

        public IQueryable<T> GetData<T>(bool trackingChanges = false) where T : class
        {
            var set = Set<T>();
            if (trackingChanges)
            {
                return set.AsTracking();
            }

            return set.AsNoTracking();
        }

        public void Insert<T>(T entity) where T : class => Set<T>().Add(entity);

        void IApplicationDbContext.Update<T>(T entity) => Set<T>().Update(entity);

        public void Delete<T>(T entity) where T : class => Set<T>().Remove(entity);

        public Task SaveAsync() => SaveChangesAsync();
    }
}
