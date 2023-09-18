using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Support.Core.Result
{
    public interface IDataResult<out T> : IResult
    {
        T Data { get; }
    }
}
