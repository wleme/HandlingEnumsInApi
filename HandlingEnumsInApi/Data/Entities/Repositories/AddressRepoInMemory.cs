using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandlingEnumsInApi.Data.Entities.Repositories
{
    public class AddressRepoInMemory : IAddressRepo
    {

        private readonly List<Address> _addresses;

        public AddressRepoInMemory()
        {
            _addresses = new List<Address>();
        }

        public Task AddAsync(Address model)
        {
            model.Id = _addresses.Count + 1;
            _addresses.Add(model);
            return Task.CompletedTask;
        }

        public Task<ICollection<Address>> GetAsync()
        {
            return Task.FromResult<ICollection<Address>>(_addresses);
        }

        public Task<Address> GetAsync(int addressId)
        {
            return Task.FromResult<Address>(_addresses.Where(x => x.Id == addressId).FirstOrDefault());
        }
    }
}
