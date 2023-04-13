using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartbell.Shared.Dtos
{
    public class ConfigResponseDto
    {
        public Guid Id { get; set; }

        [DisplayName("Description")]
        public string Description { get; set; }

		[DisplayName("Value")]
		public string Value { get; set; }
    }
}
