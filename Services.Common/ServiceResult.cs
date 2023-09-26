using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Common
{
    public class ServiceResult<t>
    {
        public ResultStatus Status { get; set; }
        public string Message { get; set; }
        public t Data { get; set; }
        public string Token { get; set; }
    }
    public enum ResultStatus
    {
        InvalidToken,
        Ok,
        ParameterError,
        dataBaseError,
        processError,
        unHandeledError
    }
}
