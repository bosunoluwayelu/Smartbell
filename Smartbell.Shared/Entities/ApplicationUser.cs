using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartbell.Shared.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [Column(TypeName = "nvarchar(50)", Order = 1)]
        public string FirstName { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(50)", Order = 2)]
        public string LastName { get; set; }
    }
}
