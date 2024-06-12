using Api.Model;

namespace Api.Services
{
    public class CustomerService
    {
        private readonly IEntityRepository<Customer> _customerRepository;

        public CustomerService(IEntityRepository<Customer> customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<BasicInfoDto<Customer>> CreateCustomerAsync(CreateDto<Customer> customerDto)
        {
            return await _customerRepository.AddAsync(customerDto);
        }

        public async Task<BasicInfoDto<Customer>?> GetCustomerByIdAsync(object id)
        {
            return await _customerRepository.GetByIdAsync(id);
        }

        public IAsyncEnumerable<BasicInfoDto<Customer>> GetCustomersAsync(EntityQueryDto<Customer> query)
        {
            return _customerRepository.GetAllAsync(query);
        }

        public async Task<BasicInfoDto<Customer>?> UpdateCustomerAsync(UpdateDto<Customer> customerDto, object id)
        {
            return await _customerRepository.UpdateAsync(customerDto, id);
        }

        public async Task DeleteCustomerAsync(object id)
        {
            await _customerRepository.DeleteAsync(id);
        }
    }
}