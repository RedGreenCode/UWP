using Microsoft.EntityFrameworkCore;

using UWPMain.Models;

namespace UWPMain.DAL
{
	public sealed class TaskContext : DbContext
	{
		internal DbSet<Task> Tasks { get; set; }

		public TaskContext()
		{
			Database.Migrate();
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlite("Filename=Tasks.db");
		}
	}
}
