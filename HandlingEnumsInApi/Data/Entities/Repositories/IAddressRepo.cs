using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandlingEnumsInApi.Data.Entities.Repositories
{
    public interface IAddressRepo
    {
        Task<ICollection<Address>> GetAsync();
        Task AddAsync(Address model);
        Task<Address> GetAsync(int addressId);
        Task SaveAllAsync();
    }
}
