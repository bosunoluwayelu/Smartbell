using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartbell.Shared.Dtos
{
    public class ConfigResponseDto
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }
    }
}
