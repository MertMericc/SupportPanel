using Support.Core.Entity.Concrete;
using Support.Core.Result;
using Support.Entity.Concrete;
using Support.Entity.DTO.SupportFormDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Support.BLL.Abstract
{
    public interface ISupportFormService
    {
        IDataResult<bool>Add(SupportFormAddDto supportFormAddDto);
        IDataResult<bool>Update(SupportFormUpdateDto supportFormUpdateDto);
        IDataResult<bool>ChangeStatus(int formId, int statusId);
        IDataResult<List<SupportFormListDto>> GetSupportFormsByUserId(int userId);
        IDataResult<List<SupportFormListDto>> GetList();
        IDataResult<SupportForm> Get(Expression<Func<SupportForm, bool>> filter);
        IDataResult<List<SupportFormListDto>> GetActiveSupportList(int? userId);
        IDataResult<List<SupportFormListDto>> GetWaitingSupportList(int? userId);
        IDataResult<List<SupportFormListDto>> GetCancelledSupportList(int? userId);

    }
}
