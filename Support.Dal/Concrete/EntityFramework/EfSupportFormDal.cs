using Support.Core.EntityFramework;
using Support.Dal.Abstract;
using Support.Dal.Concrete.Context;
using Support.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Support.Dal.Concrete.EntityFramework
{
    public class EfSupportFormDal:EfEntityRepositoryBase<SupportForm,SupportPanelDbContext>,ISupportFormDal
    {
    }
}
