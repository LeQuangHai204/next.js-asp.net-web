using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using Api.Model;
using Api.Model.Dtos;
using Api.Services;

namespace Api.Controllers
{
    [Route("api/v0/customers")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerService _customerService;

        public CustomerController(CustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        // [Authorize]
        public IActionResult GetAll([FromQuery] CustomerQueryDto query)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            IAsyncEnumerable<IEntityDto<Customer>> customers;

            try
            {
                customers = _customerService.GetCustomersAsync(query);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Get all customers failed: " + ex.Message);
            }

            return Ok(customers);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            IEntityDto<Customer>? customer;

            try
            {
                customer = await _customerService.GetCustomerByIdAsync(id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Get customer by id failed: " + ex.Message);
            }

            return customer != null ? Ok(customer) : NotFound("Customer not found");
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CustomerCreateDto customer)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            IEntityDto<Customer> newCustomer;

            try
            {
                newCustomer = await _customerService.CreateCustomerAsync(customer);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Create customer failed: " + ex.Message);
            }

            return Created(null as Uri, newCustomer);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] CustomerUpdateDto customer)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            IEntityDto<Customer>? updatedCustomer;

            try
            {
                updatedCustomer = await _customerService.UpdateCustomerAsync(customer, id);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Update customer failed: " + ex.Message);
            }

            return updatedCustomer != null ? Ok(updatedCustomer) : NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                await _customerService.DeleteCustomerAsync(id);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Delete customer failed: " + ex.Message);
            }

            return NoContent();
        }
    }
}