using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartbell.Shared.Dtos
{
    public class ActivityResponseDto
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string ImageFilePath { get; set; }
        public string VideoFilePath { get; set; }
    }
}
