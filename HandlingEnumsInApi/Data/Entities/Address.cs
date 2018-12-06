using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandlingEnumsInApi.Data.Entities
{
    public class Address
    {
        public int Id { get; set; }
        public string StreetName { get; set; }
        public AddressType AddressType { get; set; }
    }
}
