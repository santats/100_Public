using Microsoft.AspNetCore.Mvc;
using ShopOnline.CustomerMicroservice.Data;
using ShopOnline.CustomerMicroservice.Entities;
using System;
using System.Threading.Tasks;

namespace ShopOnline.CustomerMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private CustomerDatabase _context;
        public CustomerController()
        {
            _context = CustomerDatabase.Initialize();
        }
        [HttpPost]
        public async Task<IActionResult> Add(Customer customer)
        {
            var existingProduct = await _context.GetCustomerByName(customer.Name);

            if (existingProduct != null)
                return Conflict();

            await _context.AddCustomer(customer);

            return Ok(customer.Id);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _context.GetAllCustomers();

            if (products == null)
                return NotFound();

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var product = await _context.GetCustomerById(id);

            if (product == null)
                return NotFound();

            return Ok(product);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var product = await _context.GetCustomerById(id);

            if (product == null)
                return NotFound();

            _context.DeleteCustomer(product);

            return Ok(product.Id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, Customer customerData)
        {
            var customer = await _context.GetCustomerById(id);

            if (customer == null)
            {
                return NotFound();
            }
            else
            {
                customer.Name = customerData.Name;
                customer.City = customerData.City;
                customer.Contact = customerData.Contact;
                customer.Email = customerData.Email;

                return Ok(customer.Id);
            }
        }
    }
}