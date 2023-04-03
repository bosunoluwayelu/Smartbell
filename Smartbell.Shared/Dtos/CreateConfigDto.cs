using System.ComponentModel.DataAnnotations;

namespace Smartbell.Shared.Dtos
{
    public class CreateConfigDto
    {
        [Required]
        public string Description { get; set; }

        [Required]
        public string Value { get; set; }
    }
}
