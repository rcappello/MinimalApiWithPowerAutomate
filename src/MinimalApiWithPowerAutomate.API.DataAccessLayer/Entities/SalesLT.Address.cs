using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalApiWithPowerAutomate.API.DataAccessLayer.Entities
{
    [Table("SalesLT.Address")]
    public class Address
    {
        public Address()
        {
            this.CustomerAddress = new HashSet<CustomerAddress>();
            this.SalesOrderHeader = new HashSet<SalesOrderHeader>();
            this.SalesOrderHeader1 = new HashSet<SalesOrderHeader>();
        }

        public int AddressID { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string StateProvince { get; set; }
        public string CountryRegion { get; set; }
        public string PostalCode { get; set; }
        public System.Guid rowguid { get; set; }
        public System.DateTime ModifiedDate { get; set; }

        public virtual ICollection<CustomerAddress> CustomerAddress { get; set; }
        public virtual ICollection<SalesOrderHeader> SalesOrderHeader { get; set; }
        public virtual ICollection<SalesOrderHeader> SalesOrderHeader1 { get; set; }
    }
}
