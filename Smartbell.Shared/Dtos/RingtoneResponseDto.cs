using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartbell.Shared.Dtos
{
    public class RingtoneResponseDto
    {
        public Guid Id { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [DisplayName("File Path")]
        public string RingtoneFilePath { get; set; }
    }
}
