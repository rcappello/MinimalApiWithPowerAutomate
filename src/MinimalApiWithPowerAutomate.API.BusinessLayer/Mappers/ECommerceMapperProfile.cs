using AutoMapper;
using MinimalApiWithPowerAutomate.API.Shared.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities = MinimalApiWithPowerAutomate.API.DataAccessLayer.Entities;

namespace MinimalApiWithPowerAutomate.API.BusinessLayer.Mappers
{
    public class ECommerceMapperProfile : Profile
    {
        public ECommerceMapperProfile()
        {
            CreateMap<Entities.SalesOrderHeader, Order>()
                .ForMember(dto => dto.OrderDetailsCount,
                    opt => opt.MapFrom(source => source.SalesOrderDetails.Count()))
                .ForMember(dto => dto.LongBillToAddress,
                    opt => opt.MapFrom(source => source.BillToAddress != null ? $"{source.BillToAddress.AddressLine1} - {source.BillToAddress.City} - {source.BillToAddress.StateProvince} - {source.BillToAddress.CountryRegion} - {source.BillToAddress.PostalCode}" : String.Empty))
                .ForMember(dto => dto.LongShipToAddress,
                    opt => opt.MapFrom(source => source.ShipToAddress != null ? $"{source.ShipToAddress.AddressLine1} - {source.ShipToAddress.City} - {source.ShipToAddress.StateProvince} - {source.ShipToAddress.CountryRegion} - {source.ShipToAddress.PostalCode}" : String.Empty))
                .ForMember(dto => dto.CustomerLongName,
                    opt => opt.MapFrom(source => source.Customer != null ? $"{source.Customer.Title} {source.Customer.FirstName} {source.Customer.LastName}".Trim() : String.Empty));

            CreateMap<Entities.Customer, Customer>();
        }
    }
}
