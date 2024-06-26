using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyForum.Model.Model
{
    public class JwtUser : IEntity
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Collection<string> Authorities { get; set; }
        public DateTime LastPasswordResetDate { get; set; }
    }
}
