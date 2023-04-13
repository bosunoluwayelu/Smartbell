using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartbell.Shared.Dtos
{
    public class CreateActivityDto
    {
        [Required]
        public string Description { get; set; }

        public IFormFile ImageFilePath { get; set; }

        public IFormFile VideoFilePath { get; set; }
    }
}
