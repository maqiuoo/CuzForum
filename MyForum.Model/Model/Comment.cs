using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MyForum.Model
{
	public class Comment : IEntity
	{
		public int Id { get; set; }
		public int? TopicId { get; set; }
		public int? UserId { get; set; }
		public string Content { get; set; }
		public DateTime InTime { get; set; }
		public int? CommentId { get; set; }
		public virtual Topic? Topic {  get; set; }
		public virtual User? User {  get; set; }
		public virtual Comment? SecondComment {  get; set; }

	}
}
