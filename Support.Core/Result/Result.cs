using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Support.Core.Result
{
    public class Result : IResult
    {
        public Result(bool success, string message, string messageCode) : this(success)
        {
            Message = message;
            MessageCode = messageCode;
        }
        public Result(bool success)
        {
            Success = success;
        }
        public bool Success { get; }
        public string Message { get; }
        public string MessageCode { get; }

    }
}
