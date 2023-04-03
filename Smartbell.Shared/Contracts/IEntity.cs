using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartbell.Shared.Contracts
{
    public interface IEntity
    {
        public Guid Id { get; set; }
    }
}
