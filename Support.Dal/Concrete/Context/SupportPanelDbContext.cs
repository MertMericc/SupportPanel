using Microsoft.EntityFrameworkCore;
using Support.Core.Entity.Concrete;
using Support.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Support.Dal.Concrete.Context
{
    public class SupportPanelDbContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(local);Database=SupportPanel;Integrated Security=True;TrustServerCertificate=True");
        }
        public DbSet<SupportForm> SupportForms { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
    }
}
