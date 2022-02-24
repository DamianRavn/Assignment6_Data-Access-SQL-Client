using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectB.Models
{
    /// <summary>
    /// A data object meant to house a country and the amount of customers in it
    /// </summary>
    public class CustomerCountry
    {
        public string Country { get; set; }
        public int Population { get; set; }
    }
}
