using AutoMapper;
using System.Diagnostics.CodeAnalysis;
using Wiz.Template.API.ViewModels.Address;
using Wiz.Template.API.ViewModels.Customer;
using Wiz.Template.Domain.Models;
using Wiz.Template.Domain.Models.Dapper;
using Wiz.Template.Domain.Models.Services;

namespace Wiz.Template.API.AutoMapper
{
    [ExcludeFromCodeCoverage]
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            #region Customer

            CreateMap<CustomerAddress, CustomerAddressViewModel>();
            CreateMap<Customer, CustomerViewModel>().ReverseMap();
            CreateMap<ViaCEP, AddressViewModel>();
            CreateMap<Address, AddressViewModel>().ReverseMap();

            #endregion
        }
    }
}
