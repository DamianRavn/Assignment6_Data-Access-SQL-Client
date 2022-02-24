using ProjectB.Models;
using System.Collections.Generic;

namespace ProjectB.Repositories
{
    /// <summary>
    /// Interface used with the Chinook server. Implements generic repository
    /// </summary>
    public interface ICustomerRepository : IRepository<Customer>
    {
        /// <summary>
        /// The returned customers should correspond approximately to the name given. The name can be first or last name.
        /// </summary>
        /// <param name="name">A name to find, first or last</param>
        /// <returns>a list of customers corresponding approximately to the name given</returns>
        public List<Customer> GetByName(string name);

        /// <summary>
        /// When you need 'a page of' customers, where you can decide what a 'page' is by adjusting limit and offset
        /// </summary>
        /// <param name="offset">how many customers are skipped</param>
        /// <param name="limit">how many customers are selected</param>
        /// <returns>The list of customers specified</returns>
        public List<Customer> GetCustomerPage(int offset, int limit = 30);

        /// <summary>
        /// How many customers per country
        /// </summary>
        /// <returns>a list of customers per country</returns>
        public List<CustomerCountry> GetCountryCustomerPopulation();

        /// <summary>
        /// Who are the highest spenders?
        /// </summary>
        /// <returns>a list of total money spent by each customer</returns>
        public List<CustomerSpender> GetHighestSpenders();

        /// <summary>
        /// Find a customers favorite genre
        /// </summary>
        /// <param name="customer">the specific customer</param>
        /// <returns>object with needed information</returns>
        public CustomerGenre GetCustomersPopularGenre(Customer customer);
    }
}
