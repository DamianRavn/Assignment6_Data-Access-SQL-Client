using ProjectB.Models;
using System.Collections.Generic;

namespace ProjectB.Repositories
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        public List<Customer> GetByName(string name);

        public List<Customer> GetCustomerPage(int limit, int offset);

        public List<CustomerCountry> GetCountryCustomerPopulation();

        public List<CustomerSpender> GetHighestSpenders();

        public CustomerGenre GetCustomersPopularGenre(Customer customer);
    }
}
