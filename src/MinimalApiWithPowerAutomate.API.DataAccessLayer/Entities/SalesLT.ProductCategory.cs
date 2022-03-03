using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalApiWithPowerAutomate.API.DataAccessLayer.Entities
{
    [Table("SalesLT.ProductCategory")]
    public class ProductCategory
    {
        public ProductCategory()
        {
            this.Product = new HashSet<Product>();
            this.ProductCategory1 = new HashSet<ProductCategory>();
        }

        public int ProductCategoryID { get; set; }
        public Nullable<int> ParentProductCategoryID { get; set; }
        public string Name { get; set; }
        public Guid rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual ICollection<Product> Product { get; set; }
        public virtual ICollection<ProductCategory> ProductCategory1 { get; set; }
        public virtual ProductCategory ProductCategory2 { get; set; }
    }
}
