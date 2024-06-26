using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyForum.Model.ReptlieModel
{
	public interface IReptlieEntity
	{
		public int Id { get; set; }
		public string title {  get; set; }
	}
}
