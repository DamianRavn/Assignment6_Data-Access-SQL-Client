using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectB.Models
{
    /// <summary>
    /// A data object meant to house a customer and the most bought genre + the amount of tracks from that genre
    /// </summary>
    public class CustomerGenre
    {
        public Customer Customer { get; set; }
        public int BoughtTracks { get; set; }
        public string Genre { get; set; }
    }
}
