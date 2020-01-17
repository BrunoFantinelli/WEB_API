using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WEB_API.Models;

namespace WEB_API.Data
{
    public class WEB_APIContext : DbContext
    {
        public WEB_APIContext (DbContextOptions<WEB_APIContext> options)
            : base(options)
        {
        }

        public DbSet<WEB_API.Models.User> User { get; set; }
    }
}
