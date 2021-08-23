using ShopOnline.CustomerMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace ShopOnline.CustomerMicroservice.Data
{
    public class CustomerDatabase
    {
        public List<Customer> Customers;

        private CustomerDatabase() { }
        public static CustomerDatabase Initialize()
        {
            CustomerDatabase _customerDatabase = new CustomerDatabase();

            _customerDatabase.Customers = new List<Customer>()
            {
                new Customer { Id = Guid.Parse("d578c33a-6edc-4acd-9130-e533bfe3070e"),  Name = "Santosh", City = "Bangalore", Contact = "9886041XXX", Email = "santosh@xyz.com"},
                new Customer { Id = Guid.Parse("bd52f3ef-a80a-4292-9021-f959cff625d2"), Name = "John", City = "Bangalore", Contact = "9886041XXX", Email = "john@xyz.com"},
                new Customer { Id = Guid.Parse("f4361fb0-31ed-46c4-a360-621ab02967d6"), Name = "Andrew", City = "Bangalore", Contact = "9886041XXX", Email = "andrew@xyz.com"},
                new Customer { Id = Guid.Parse("8d337af6-dd0d-472e-a5bc-78e10b55ce62"), Name = "James", City = "Bangalore", Contact = "9886041XXX", Email = "james@xyz.com"}
            };

            return _customerDatabase;
        }

        internal async Task<Customer> GetCustomerByName(string customerName)
        {
            return Customers.Where(a => a.Name == customerName).FirstOrDefault();
        }

        public Task<List<Customer>> GetAllCustomers()
        {
            return Task.FromResult(Customers);
        }

        public async Task AddCustomer(Customer customer)
        {
            customer.Id = Guid.NewGuid();

            Customers.Add(customer);
        }

        internal async Task<Customer> GetCustomerById(Guid id)
        {
            return Customers.Where(a => a.Id == id).FirstOrDefault();
        }

        internal void DeleteCustomer(Customer customer)
        {
            Customers.Remove(customer);
        }
    }
}
