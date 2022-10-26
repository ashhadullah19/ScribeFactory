using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scribe_Factory_Core.Models
{
	public class Subscriptions
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string Price { get; set; }
		public string StripeUrl { get; set; }
		public string WebHookUrl { get; set; }

		public ICollection<Subcribers> Subcribers { get; set; }
	}
}
