using AutoMapper;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Wiz.Template.API.AutoMapper;
using Wiz.Template.API.Services;
using Wiz.Template.API.ViewModels.Customer;
using Wiz.Template.Domain.Interfaces.Notifications;
using Wiz.Template.Domain.Interfaces.Repository;
using Wiz.Template.Domain.Interfaces.Services;
using Wiz.Template.Domain.Interfaces.UoW;
using Wiz.Template.Domain.Models;
using Wiz.Template.Domain.Models.Dapper;
using Wiz.Template.Domain.Notifications;
using Wiz.Template.Unit.Tests.Mocks;
using Xunit;

namespace Wiz.Template.Unit.Tests.Services
{
    public class CustomerServiceTest
    {
        private readonly Mock<ICustomerRepository> _customerRepositoryMock;
        private readonly Mock<IViaCEPService> _viaCEPServiceMock;
        private readonly IDomainNotification _domainNotification;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly IMapper _mapper;

        public CustomerServiceTest()
        {
            _customerRepositoryMock = new Mock<ICustomerRepository>();
            _viaCEPServiceMock = new Mock<IViaCEPService>();
            _domainNotification = new DomainNotification();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfiles())));
        }

        [Fact]
        public async Task GetAll_ReturnCustomerAddressViewModelTestAsync()
        {
            var cep = "17052520";

            _customerRepositoryMock.Setup(x => x.GetAllAsync())
                .ReturnsAsync(CustomerMock.CustomerAddressModelFaker.Generate(3));

            _viaCEPServiceMock.Setup(x => x.GetByCEPAsync(cep))
                .ReturnsAsync(ViaCEPMock.ViaCEPModelFaker.Generate());

            var customerService = new CustomerService(_customerRepositoryMock.Object,
                _viaCEPServiceMock.Object, _domainNotification,
                _unitOfWorkMock.Object, _mapper);

            var customeMethod = await customerService.GetAllAsync();

            var customerResult = Assert.IsAssignableFrom<IEnumerable<CustomerAddressViewModel>>(customeMethod);

            Assert.NotNull(customerResult);
            Assert.NotEmpty(customerResult);
        }

        [Fact]
        public async Task GetById_ReturnCustomerViewModelTestAsync()
        {
            var customerId = CustomerMock.CustomerIdViewModelFaker.Generate();

            _customerRepositoryMock.Setup(x => x.GetByIdAsync(customerId.Id))
                .ReturnsAsync(CustomerMock.CustomerModelFaker.Generate());

            var customerService = new CustomerService(_customerRepositoryMock.Object,
                _viaCEPServiceMock.Object, _domainNotification,
                _unitOfWorkMock.Object, _mapper);

            var customeMethod = await customerService.GetByIdAsync(customerId);

            var customerResult = Assert.IsAssignableFrom<CustomerViewModel>(customeMethod);

            Assert.NotNull(customerResult);
        }

        [Fact]
        public async Task GetAddressByIdAsync_ReturnCustomerAddressViewModelTestAsync()
        {
            var customerId = CustomerMock.CustomerIdViewModelFaker.Generate();

            _customerRepositoryMock.Setup(x => x.GetAddressByIdAsync(customerId.Id))
                .ReturnsAsync(CustomerMock.CustomerAddressModelFaker.Generate());

            var customerService = new CustomerService(_customerRepositoryMock.Object,
                _viaCEPServiceMock.Object, _domainNotification,
                _unitOfWorkMock.Object, _mapper);

            var customeMethod = await customerService.GetAddressByIdAsync(customerId);

            var customerResult = Assert.IsAssignableFrom<CustomerAddressViewModel>(customeMethod);

            Assert.NotNull(customerResult);
        }

        [Fact]
        public async Task GetAddressByNameAsync_ReturnCustomerAddressViewModelTestAsync()
        {
            var customerName = CustomerMock.CustomerNameViewModelFaker.Generate();

            _customerRepositoryMock.Setup(x => x.GetByNameAsync(customerName.Name))
                .ReturnsAsync(CustomerMock.CustomerAddressModelFaker.Generate());

            var customerService = new CustomerService(_customerRepositoryMock.Object,
                _viaCEPServiceMock.Object, _domainNotification,
                _unitOfWorkMock.Object, _mapper);

            var customeMethod = await customerService.GetAddressByNameAsync(customerName);

            var customerResult = Assert.IsAssignableFrom<CustomerAddressViewModel>(customeMethod);

            Assert.NotNull(customerResult);
        }

        [Fact]
        public void Add_ReturnCustomerViewModelTestAsync()
        {
            var customer = CustomerMock.CustomerViewModelFaker.Generate();

            _customerRepositoryMock.Setup(x => x.GetByNameAsync(customer.Name))
                .ReturnsAsync(CustomerMock.CustomerAddressModelFaker.Generate());

            var customerService = new CustomerService(_customerRepositoryMock.Object,
                _viaCEPServiceMock.Object, _domainNotification,
                _unitOfWorkMock.Object, _mapper);

            customerService.Add(customer);

            Assert.NotNull(customer);
        }

        [Fact]
        public void Update_SucessTestAsync()
        {
            var customer = CustomerMock.CustomerViewModelFaker.Generate();

            var customerService = new CustomerService(_customerRepositoryMock.Object,
                _viaCEPServiceMock.Object, _domainNotification,
                _unitOfWorkMock.Object, _mapper);

            customerService.Update(customer);

            Assert.NotNull(customer);
        }

        [Fact]
        public void Remove_SucessTestAsync()
        {
            var customer = CustomerMock.CustomerViewModelFaker.Generate();

            var customerService = new CustomerService(_customerRepositoryMock.Object,
                _viaCEPServiceMock.Object, _domainNotification,
                _unitOfWorkMock.Object, _mapper);

            customerService.Remove(customer);

            Assert.NotNull(customer);
        }
    }
}
