using Microsoft.EntityFrameworkCore;

namespace Server.Context
{
    public class Context: DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }
        
    }
}
