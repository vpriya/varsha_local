using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForeignNationalAPI.Models
{
    public class FNDetailContext:DbContext
    {
        public FNDetailContext(DbContextOptions<FNDetailContext> options):base(options)
        {

        }

        public DbSet<FNDetail> FN_Details { get; set; } //property
    }
}
