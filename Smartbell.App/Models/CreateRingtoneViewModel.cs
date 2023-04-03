using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Smartbell.App.Models
{
    public class CreateRingtoneViewModel
    {
        public Guid Id { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [DisplayName("File Path")]
        public string RingtoneFilePath { get; set; }
    }
}
