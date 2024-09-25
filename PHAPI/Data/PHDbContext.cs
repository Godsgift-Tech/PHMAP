using Microsoft.EntityFrameworkCore;
using PHAPI.Models.Domain;

namespace PHAPI.Data
{
    public class PHDbContext: DbContext
    {
        public PHDbContext(DbContextOptions<PHDbContext>options):base(options)
        
        { 
        }
        public DbSet<Difficulty> Difficulties{ get; set; }
        public DbSet<LGA> LGAs{ get; set; }

        public DbSet <Map> Maps{ get; set; }
        
    }
}
