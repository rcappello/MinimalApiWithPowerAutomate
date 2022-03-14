using MinimalApiWithPowerAutomate.API.Shared.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalApiWithPowerAutomate.API.BusinessLayer.Services
{
    public interface IECommerceService
    {
        Task<ListResult<Order>> GetOrdersAsync(string searchText, int pageIndex, int itemsPerPage);

        Task<Customer> GetCustomerByIDAsync(int customerId);
    }
}
