using HandlingEnumsInApi.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandlingEnumsInApi.Dtos
{
    public class AddressResponseDto
    {
        public int Id { get; set; }
        public string StreetName { get; set; }
        public AddressType Type{ get; set; }
    }
}
