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
    public class EfUserDal : EfEntityRepositoryBase<User, SupportPanelDbContext>, IUserDal
    {
        public List<OperationClaim> GetClaims(User user)
        {
            using (var context = new SupportPanelDbContext())
            {
                var result = from claims in context.OperationClaims
                             join userclaim in context.UserOperationClaims
                             on claims.Id equals userclaim.OperationClaimId
                             where userclaim.UserId == user.Id
                             select new OperationClaim { Id = claims.Id, Name = claims.Name };
                return result.ToList();
            }
        }
    }
}
