using Book.Store.Core.Repository;
using Microsoft.Extensions.Configuration;
using Services.Common;
using User.Entities;

namespace User.Manager
{
    public class UserManager : IManagerBase, IUserManager, IDisposable
    {
        public IConfiguration _configuration = null;
        public UserManager(IConfiguration iConfig)
        {
            _configuration = iConfig;
        }
        public UserManager()
        {

        }

        public void Dispose()
        {
            //  throw new NotImplementedException();
        }

        public ServiceResult<EUser> Login(string userName, string password)
        {
            ServiceFactory factory = new ServiceFactory();
            var user = factory.GetInstance<EUser>();
            var data = user.List().Where(x => x.UserName == userName && x.Password == password).FirstOrDefault();
            if (data == null)
            {
                return new ServiceResult<EUser>()
                {
                    Data = null,
                    Message = "User Not Found",
                    Status = ResultStatus.processError
                };
            }
            return new ServiceResult<EUser>()
            {
                Data = data,
                Message = "Login Successfully",
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<bool> SignUp(EUser eUser)
        {
            try
            {
                using (ServiceFactory factory = new ServiceFactory())
                {
                    var userService = factory.GetInstance<EUser>();
                    userService.Add(eUser);
                    return new ServiceResult<bool>()
                    {
                        Data = true,
                        Message = "User Added Successfully !...",
                        Status = ResultStatus.Ok
                    };
                }
            }
            catch (Exception ex)
            {

                return new ServiceResult<bool>()
                {
                    Data = false,
                    Message = "Error in adding Case",
                    Status = ResultStatus.processError
                };
            }
        }
    }
}
