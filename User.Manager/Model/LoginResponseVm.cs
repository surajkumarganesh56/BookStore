using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Entities;

namespace User.Manager.Model
{
    public class LoginResponseVm
    {
        public int UserId { get; set; }
        public int OfficeId { get; set; }
        public UserType UserType { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}
