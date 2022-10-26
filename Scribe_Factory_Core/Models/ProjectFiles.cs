using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scribe_Factory_Core.Models
{
	public class ProjectFiles
	{
		public int Id { get; set; }
		public int ProjectId { get; set; }
		public string FileUrl { get; set; }
		public Project Project { get; set; }

	}
}
