using CommandAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace commandAPI.Data {
    public class CommadContext : DbContext {
        public CommadContext(DbContextOptions<CommadContext> opt ) :base (opt)
        {
            
        }
         public DbSet<Command> Commands{ get; set; }
         
            

    }


}