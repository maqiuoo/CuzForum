using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyForum.Model
{
	public class Notification : IEntity
	{
		public int Id { get; set; }
		public int TopicId { get; set; }
		public int UserId { get; set; }
		public int TargetUserId { get; set; }
		public string Action {  get; set; }
		public DateTime InTime {  get; set; }
		public bool Read {  get; set; }
		[NotMapped]
		public Topic Topic { get; set; }
		[NotMapped]
		public User User { get; set; }	
	}
}
