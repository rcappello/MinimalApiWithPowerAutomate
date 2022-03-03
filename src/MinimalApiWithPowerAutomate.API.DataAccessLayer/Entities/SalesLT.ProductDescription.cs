using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalApiWithPowerAutomate.API.DataAccessLayer.Entities
{
    [Table("SalesLT.ProductDescription")]
    public class ProductDescription
    {
        public ProductDescription()
        {
            this.ProductModelProductDescription = new HashSet<ProductModelProductDescription>();
        }

        public int ProductDescriptionID { get; set; }
        public string Description { get; set; }
        public Guid rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }
        public virtual ICollection<ProductModelProductDescription> ProductModelProductDescription { get; set; }
    }
}
