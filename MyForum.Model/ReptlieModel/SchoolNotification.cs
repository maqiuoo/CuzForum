using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyForum.Model.ReptlieModel
{
    public class SchoolNotification : IReptlieEntity
	{
        public int Id { get; set; }
        public string title { get; set; }
        public string link { get; set; }
        public string date { get; set; }
    }
}
