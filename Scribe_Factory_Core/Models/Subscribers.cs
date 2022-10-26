using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scribe_Factory_Core.Models
{
	public class Subcribers
	{
		public int Id { get; set; }
		public long ApplicationUserId { get; set; }
		public DateTime ExpiryDate { get; set; }
		public int SubscriptionId { get; set; }
		public bool IsActive { get; set; }

		public ApplicatioUser ApplicatioUser { get; set; }
		public Subscriptions Subscriptions { get; set; }

	}
}
