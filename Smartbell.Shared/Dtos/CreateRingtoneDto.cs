using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartbell.Shared.Dtos
{
    public class CreateRingtoneDto
    {
        [Required]
        public string Description { get; set; }

        [Required]
        public string RingtoneFilePath { get; set; }

        public IFormFile Image { get; set; }
    }
}
