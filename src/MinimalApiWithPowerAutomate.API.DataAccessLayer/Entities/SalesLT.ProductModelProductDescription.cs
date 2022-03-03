using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalApiWithPowerAutomate.API.DataAccessLayer.Entities
{
    [Table("SalesLT.ProductModelProductDescription")]
    public class ProductModelProductDescription
    {
        public int ProductModelID { get; set; }
        public int ProductDescriptionID { get; set; }
        public string Culture { get; set; }
        public System.Guid rowguid { get; set; }
        public System.DateTime ModifiedDate { get; set; }

        public virtual ProductDescription ProductDescription { get; set; }
        public virtual ProductModel ProductModel { get; set; }
    }
}
