using MinimalApiWithPowerAutomate.API.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace MinimalApiWithPowerAutomate.API.DataAccessLayer
{
    public interface IApplicationDbContext
    {
        DbSet<Address> Address { get; set; }
        DbSet<Customer> Customer { get; set; }
        DbSet<CustomerAddress> CustomerAddress { get; set; }
        DbSet<Product> Product { get; set; }
        DbSet<ProductCategory> ProductCategory { get; set; }
        DbSet<ProductDescription> ProductDescription { get; set; }
        DbSet<ProductModel> ProductModel { get; set; }
        DbSet<ProductModelProductDescription> ProductModelProductDescription { get; set; }
        DbSet<SalesOrderDetail> SalesOrderDetail { get; set; }
        DbSet<SalesOrderHeader> SalesOrderHeader { get; set; }

        // Retrieve data from a generic table.
        IQueryable<T> GetData<T>(bool trackingChanges = false) where T : class;

        void Insert<T>(T entity) where T : class;

        void Update<T>(T entity) where T : class;

        void Delete<T>(T entity) where T : class;

        Task SaveAsync();
    }
}
