using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DS_AD_API_Demo.Repository
{
    public class DSDbContext : IdentityDbContext
    {
        public DSDbContext(DbContextOptions<DSDbContext> options)
          : base(options)
        {
        }


    }
}
