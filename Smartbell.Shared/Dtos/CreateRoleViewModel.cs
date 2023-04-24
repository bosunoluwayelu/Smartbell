using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartbell.Shared.Dtos
{
	public class CreateRoleViewModel
	{
		[Required]
		[DisplayName("Role Name")] 
		
		public string RoleName { get; set; }
	}
}
