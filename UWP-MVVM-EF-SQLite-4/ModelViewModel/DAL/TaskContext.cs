using Microsoft.EntityFrameworkCore;
using ModelViewModel.Models;

namespace ModelViewModel.DAL
{
	public sealed class TaskContext : DbContext
	{
		internal DbSet<Task> Tasks { get; set; }

		public TaskContext() { }

		public TaskContext(DbContextOptions<TaskContext> options)
			: base(options)
		{ }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder.UseSqlite("Filename=Tasks.db");
			}
		}
	}
}
