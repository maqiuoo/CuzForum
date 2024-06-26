using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyForum.Model
{
	public class Collect:IEntity
	{
		public int Id { get; set; }
		public int? TopicId { get; set; }
		public int? UserId { get; set; }
		public DateTime InTime { get; set; }
		public virtual Topic? Topic { get; set; }
		public virtual User? User {  get; set; }
	}
}
