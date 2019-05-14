using Microsoft.EntityFrameworkCore;
using WebserviceMain.Database.Tables;

namespace WebserviceMain.Database.Helper
{
	public class DataContext : DbContext
	{
		public DataContext(DbContextOptions options) : base(options)
		{
		}

		public DbSet<Category> Category { get; set; }
		public DbSet<Question> Question { get; set; }
		public DbSet<Answer> Answer { get; set; }
		public DbSet<Game> Game { get; set; }
		public DbSet<Game2Category> Game2Category { get; set; }
		public DbSet<User> User { get; set; }
	}
}
