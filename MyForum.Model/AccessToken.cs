using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyForum.Model
{
	public class AccessToken : IEntity
	{
		public int Id { get; set; }
		public string Token { get; set; }
		public int UserId { get; set; }
		public DateTime InTime { get; set; }

	}
}
