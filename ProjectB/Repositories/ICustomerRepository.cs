using ProjectB.Models;
using System.Collections.Generic;

namespace ProjectB.Repositories
{
    public interface ICustomerRepository
    {
        public Customer GetCustomer(int id);

        public List<Customer> GetAllCustomers();

        public bool AddNewCustomer(Customer customer);

        public bool UpdateCustomer(Customer customer);

        public bool DeleteCustomer(int id);

    }
}
