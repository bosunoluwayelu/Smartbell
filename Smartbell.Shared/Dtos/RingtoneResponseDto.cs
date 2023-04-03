using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartbell.Shared.Dtos
{
    public class RingtoneResponseDto
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string RingtoneFilePath { get; set; }
    }
}
