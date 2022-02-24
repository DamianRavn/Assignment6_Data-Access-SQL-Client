using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectB.Models
{
    /// <summary>
    /// A data object meant to house a customer from the customer table and the correspinding amount of money the customer spend on tracks
    /// </summary>
    public class CustomerSpender
    {
        public Customer Customer { get; set; }
        public decimal Amount { get; set; }
    }
}
