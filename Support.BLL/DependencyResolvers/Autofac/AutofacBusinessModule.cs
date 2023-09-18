using Autofac;
using Microsoft.AspNetCore.Identity;
using Support.BLL.Abstract;
using Support.BLL.Concrete;
using Support.Core.Security;
using Support.Dal.Abstract;
using Support.Dal.Concrete.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Support.BLL.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
           

            builder.RegisterType<JwtHelper>().As<ITokenHelper>();

            builder.RegisterType<UserManager>().As<IUserService>();
            builder.RegisterType<EfUserDal>().As<IUserDal>();

            builder.RegisterType<AuthManager>().As<IAuthService>();

            builder.RegisterType<EmailManager>().As<IEmailService>();

            builder.RegisterType<EfUserOperationClaimDal>().As<IUserOperationClaimDal>();

            builder.RegisterType<SupportFormManager>().As<ISupportFormService>();
            builder.RegisterType<EfSupportFormDal>().As<ISupportFormDal>();






        }
    }
}
