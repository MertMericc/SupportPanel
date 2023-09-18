using Support.Core.Entity.Concrete;
using Support.Core.EntityFramework;
using Support.Dal.Abstract;
using Support.Dal.Concrete.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Support.Dal.Concrete.EntityFramework
{
    public class EfUserOperationClaimDal : EfEntityRepositoryBase<UserOperationClaim, SupportPanelDbContext>, IUserOperationClaimDal
    {
    }
}
