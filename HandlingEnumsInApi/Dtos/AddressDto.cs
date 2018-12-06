using HandlingEnumsInApi.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandlingEnumsInApi.Dtos
{
    public class AddressDto : IValidatableObject
    {
        [Required]
        public string StreetName { get; set; }
        public string AddressType { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!Enum.TryParse(AddressType,true,out AddressType result))
            {
                yield return new ValidationResult("Invalid address type", new[] { nameof(AddressType) });
            }

            AddressType = result.ToString(); //normalize Type
        }
    }
}
