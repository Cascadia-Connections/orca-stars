using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace OrcaStarsWebApplication.Models
{
    public class BitDataContext : DbContext
    {
        public BitDataContext(DbContextOptions<BitDataContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Business> Businesses { get; set; }
        public DbSet<BusinessContact> Contacts { get; set; }
    }
}
