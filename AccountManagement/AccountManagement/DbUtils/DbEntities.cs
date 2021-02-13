using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using AccountManagement.Domain;
using System.Data;
using System.Reflection;
using System.Configuration;

namespace AccountManagement.DbUtils
{
    class DbEntities:DbContext
    {
        //private static string connStr = ConfigurationManager.AppSettings["dbContextConnStr"].ToString();
       // private static string connStr = ConfigurationManager.connectionStrings.ToString();

        public DbEntities() : base("name= BloggingContext")
        {

        }

        public virtual DbSet<Users> usersDbset { get; set; }
        public virtual DbSet<AccountInformation> accountInfoDbset { get; set; }



    }
}

