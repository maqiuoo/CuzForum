using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyForum.Model
{
	public class EmploymentData : IEntity
	{
		public int Id {  get; set; }
		public string Type { get; set; }
		public string Region {  get; set; }
		public int Population {  get; set; }
		public int Year { get; set; }
	}
}
