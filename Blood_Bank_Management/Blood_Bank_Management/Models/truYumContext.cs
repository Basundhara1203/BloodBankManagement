
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Blood_Bank_Management.Models;

namespace Blood_Bank_Management

{
    public class truYumContext : DbContext
    {
        public truYumContext() : base("TruYumContext") { }

       public  DbSet<HospInfo> HospInfos{ get;set ;}
        public DbSet<DonorInfo> DonorInfos { get; set; }
        public DbSet<RoleUser> RoleUsers { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<BloodStock> BloodStocks { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }

    }
}
