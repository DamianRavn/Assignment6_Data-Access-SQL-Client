
namespace ProjectB.Models
{
    /// <summary>
    /// A data object meant to house a customer from the customer table in the chinook server
    /// </summary>
    public class Customer
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
