using Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.Manager
{
    public abstract class IManagerBase
    {

        internal ServiceResult<dynamic> Error<t>(string message)
        {
            return new ServiceResult<dynamic>
            {
                Message = message,
                Status = ResultStatus.processError
            };
        }

        public ServiceResult<T> Success<T>(T data, string message)
        {
            return new ServiceResult<T>
            {
                Data = data,
                Message = message,
                Status = ResultStatus.Ok
            };
        }
    }
}
