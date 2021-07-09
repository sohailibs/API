using BOL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
   public class Context : DbContext
    {
        public Context() : base("DPS")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<Context, DAL.Migrations.Configuration>());
            Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<Users> Users { get; set; }

    }
}
