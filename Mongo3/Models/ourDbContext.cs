using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Mongo3.Models
{
    public class ourDbContext: DbContext
    {
        public DbSet<UserAccount> userAccount { get; set; }


    }
}