using Microsoft.EntityFrameworkCore;
using TVShows.DataAccessLayer.Models;

namespace TVShows.DataAccessLayer.DataAccess
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<TVShowsModel> TVShows { get; set; }
    }
}
