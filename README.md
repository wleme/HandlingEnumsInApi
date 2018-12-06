# This project shows how to handle enums in .Net Core APIs.

- First, let's create our model and a new enum
``` csharp 
    public class Address
    {
        public int Id { get; set; }
        public string StreetName { get; set; }
        public AddressType AddressType { get; set; }
    }

    public enum AddressType: int
    {
        Home = 10,
        Office =20
    }
```
You don't want to make your api consumers understand your enums. They should send / get Home or Office instead of 10 or 20.

- Create a new dto that will receive the data in your post method and add a custom validator that will make sure the string supplied is valid.
The custom validator tries to convert the string supplied to a valid enum. It also re-sets the string to the string representation of the enum. Ie. if you send up "hOme", the string will be converted back to "Home"

``` csharp 
    public class AddressDto : IValidatableObject
    {
        [Required]
        public string StreetName { get; set; }
        public string Type { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!Enum.TryParse(Type,true,out AddressType result))
            {
                yield return new ValidationResult("Invalid address type", new[] { nameof(AddressType) });
            }

            Type = result.ToString(); //normalize Type
        }
    }
```

- In the controller you need to validate the model which, according to our custom validator above, will not be valid if they send up something different than home or office. It also converts the dto to the model using automapper,
adds the model and converts the model to a response dto.

``` csharp 
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddressDto addressDto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var model = _mapper.Map<AddressDto, Address>(addressDto);
                await _addressRepo.AddAsync(model);
                var output = _mapper.Map<Address, AddressResponseDto>(model);
                return Created($"/api/addresses/{model.Id}", output);

            }
            catch (Exception e)
            {
                _log.LogError($"error adding address {e}");
            }

            return BadRequest();
        }
```

- Now let's take a look at our response dto

The different between the request dto and the response dto is the response has an Id field and the AddressType is the num itself and no longer a string.
``` csharp
    public class AddressResponseDto
    {
        public int Id { get; set; }
        public string StreetName { get; set; }
        public AddressType AddressType{ get; set; }
    }
```

