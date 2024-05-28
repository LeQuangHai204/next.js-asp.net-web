namespace Api.Models
{
    public class CustomerDtoMapper : IDbEntityDtoMapper<Customer>
    {
        public Customer ConvertCreateRequestDtoToEntity(IDbEntityDto<Customer> customerDto)
        {
            if (customerDto is not CustomerCreateDto createDto)
            {
                throw new ArgumentException("Invalid type");
            }

            return new Customer
            {
                FullName = createDto.FullName,
                NickName = createDto.NickName,
                Address = createDto.Address,
                City = createDto.City,
                PostalCode = createDto.PostalCode,
                Country = createDto.Country
            };
        }

        public CustomerBasicInfoDto ConvertEntityToBasicInfoDto(Customer customer)
        {
            return new CustomerBasicInfoDto
            {
                Id = customer.Id,
                FullName = customer.FullName,
                City = customer.City
            };
        }
    }
}