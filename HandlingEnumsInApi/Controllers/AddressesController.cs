using AutoMapper;
using HandlingEnumsInApi.Data.Entities;
using HandlingEnumsInApi.Data.Entities.Repositories;
using HandlingEnumsInApi.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandlingEnumsInApi.Controllers
{
    [Route("/api/addresses")]
    public class AddressesController : Controller
    {
        private readonly IAddressRepo _addressRepo;
        private readonly ILogger<AddressesController> _log;
        private readonly IMapper _mapper;

        public AddressesController(IAddressRepo addressRepo,
            ILogger<AddressesController> log,
            IMapper mapper)
        {
            this._addressRepo = addressRepo;
            this._log = log;
            this._mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddressDto addressDto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var model = _mapper.Map<AddressDto, Address>(addressDto);
                await _addressRepo.AddAsync(model);
                await _addressRepo.SaveAllAsync();
                var output = _mapper.Map<Address, AddressResponseDto>(model);
                return Created($"/api/addresses/{model.Id}", output);

            }
            catch (Exception e)
            {
                _log.LogError($"error adding address {e}");
            }

            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var addresses = await _addressRepo.GetAsync();
                return Ok(_mapper.Map<IEnumerable<Address>, IEnumerable<AddressResponseDto>>(addresses));
            }
            catch (Exception e)
            {
                _log.LogError($"error getting addresses {e}");
            }

            return BadRequest();
        }
        [Route("{addressId:int}")]
        [HttpGet]
        public async Task<IActionResult> Get(int addressId)
        {
            try
            {
                var address = await _addressRepo.GetAsync(addressId);
                if (address == null) return NotFound();
                return Ok(_mapper.Map<Address,AddressResponseDto>(address));
            }
            catch (Exception e)
            {
                _log.LogError($"error getting address {e}");
            }

            return BadRequest();
        }
    }
}
