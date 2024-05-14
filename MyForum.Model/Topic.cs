using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyForum.Model
{
	public class Topic : IEntity
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Url { get; set; }
		public string Content { get; set; }
		public string Tab { get; set; }
		public DateTime InTime { get; set; }
		public DateTime ModifyTime { get; set; }
		public int UserId { get; set; }
		// 评论数
		public int? CommentCount { get; set; }
		// 收藏数
		public int? CollectCount { get; set; }
		// 浏览数
		public int? View { get; set; }
		// 置顶
		public bool? Top { get; set; }
		// 加精
		public bool? Good { get; set; }
		[NotMapped] // 根据你的序列化框架选择忽略属性的方式
		public User User { get; set; }

	}
}
