using Smartbell.Shared.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartbell.Shared.Entities
{
    public class BaseEntity : IEntity
    {
        [Column(Order = 0)]
        public Guid Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string CreatedBy { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string UpdatedBy { get; set; }

        [Required]
        public DateTime UpdatedDate { get; set; }
    }
}
