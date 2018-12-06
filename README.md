This project shows how to handle enums in .net core APIs.(space, space)

- First, let's create a new enum
``` csharp {.line-numbers}
    public enum AddressType: int
    {
        Home = 10,
        Office =20
    }
```
You don't want to make your api consumers understand your enums. They should send Home or Office instead of 10 or 20.

- Create a new dto that will receive the data in your post method and add a custom validator that will make sure the string supplied is valid.
The custom validator tries to convert the string supplied to a valid enum. It also re-sets the string to the string representation of the enum. Ie. if you send up "hOme", the string will be converted to "Home"

``` csharp {.line-numbers}
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

- In the controller you need to validate the model which will be invalid if they send up something different than home or office.
It 
``` csharp {.line-numbers}
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddressDto addressDto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
```
The controller also converts 
0. First item.
d