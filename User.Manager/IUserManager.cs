using Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using User.Entities;

namespace User.Manager
{
    public interface IUserManager
    {
        ServiceResult<EUser> Login(string userName, string password);
    }
}
