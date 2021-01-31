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

            CreateMap<CustomerAddress, CustomerAddressViewModel>()
                .ForMember(customerAddressVM => customerAddressVM.Address, cfg => cfg.MapFrom(customerAddress => customerAddress));

            CreateMap<CustomerAddress, AddressViewModel>()
                .ForMember(x => x.Id, cfg => cfg.MapFrom(x => x.AddressId))
                .ReverseMap();

            CreateMap<Customer, CustomerViewModel>()
                .ReverseMap();

            CreateMap<AddressViewModel, Address>()
                .ReverseMap();

            CreateMap<ViaCEP, AddressViewModel>();

            #endregion
        }
    }
}
