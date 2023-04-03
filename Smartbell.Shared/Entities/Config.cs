using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartbell.Shared.Entities
{
    public class Config : BaseEntity
    {
        [Required]
        [Column(TypeName = "nvarchar(50)", Order = 1)]
        public string Description { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(50)", Order = 2)]
        public string Value { get; set; }

    }
}
