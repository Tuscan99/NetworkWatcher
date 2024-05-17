using NetworkWatcher.Models;
using Microsoft.EntityFrameworkCore;

namespace NetworkWatcher.DbContexts
{
	public class AppDBContext : DbContext
	{
		public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }

        public DbSet<Client> Clients { get; set; }
		public DbSet<LogItem> LogItems { get; set; }
		public DbSet<OuterLogItem> OuterLogItems { get; set; }
    }
}
