using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scribe_Factory_Core.Models
{
	public class ApplicatioUser
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string PasswordHash { get; set; }
		public string Country { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string Zip { get; set; }
		public DateTime CreatedOn { get; set; }


		public ICollection<Project> Projects { get; set; }
		public ICollection<Subcribers> Subcribers { get; set; }
	}
}
