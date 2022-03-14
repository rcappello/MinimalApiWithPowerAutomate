using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MinimalApiWithPowerAutomate.API.BusinessLayer.Services.Base;
using MinimalApiWithPowerAutomate.API.DataAccessLayer;
using MinimalApiWithPowerAutomate.API.Shared.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities = MinimalApiWithPowerAutomate.API.DataAccessLayer.Entities;

namespace MinimalApiWithPowerAutomate.API.BusinessLayer.Services
{
    public class ECommerceService : BaseService, IECommerceService
    {
        public ECommerceService(IApplicationDbContext dataContext,
                        IHttpContextAccessor httpContextAccessor,
                        ILogger<ECommerceService> logger,
                        IMapper mapper)
                : base(dataContext, httpContextAccessor, logger, mapper)
        {
        }

        public async Task<ListResult<Order>> GetOrdersAsync(string searchText, int pageIndex, int itemsPerPage)
        {
            Logger.LogDebug("Trying to retrieve  a max of {ItemsCount} orders.using {SearchText} query", itemsPerPage, searchText);

            var query = DataContext.GetData<Entities.SalesOrderHeader>().
                Where(r => searchText == null || r.SalesOrderNumber.Contains(searchText) || r.BillToAddress.StateProvince.Contains(searchText)); 

            var totalCount = await query.LongCountAsync();

            Logger.LogDebug("{ItemsCount} orders found", totalCount);

            var orders = await query
                .Include(r => r.BillToAddress)
                .Include(r => r.SalesOrderDetails)
                .Include(r => r.Customer)
                .OrderBy(r => r.SalesOrderNumber)
                .Skip(pageIndex * itemsPerPage).Take(itemsPerPage + 1)      // Try to retrieve an element more than the requested number to check whether there are more data.                
                .ProjectTo<Order>(mapper.ConfigurationProvider)
                .ToListAsync();

            return new ListResult<Order>(orders.Take(itemsPerPage), totalCount, orders.Count > itemsPerPage);
        }

        public async Task<Customer> GetCustomerByIDAsync(int customerId)
        {
            Logger.LogDebug("Trying to retrieve customer with id {customerId}", customerId);

            var query = DataContext.GetData<Entities.Customer>()
                .Where(c => c.CustomerId == customerId);

            var customer = await query
                .Include(r => r.CustomerAddresses)
                .ProjectTo<Customer>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            return customer;
        }
    }
}
