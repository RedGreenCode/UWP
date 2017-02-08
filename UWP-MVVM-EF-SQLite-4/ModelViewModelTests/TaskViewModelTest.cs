using System;
using System.Linq;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

using Xunit;

using ModelViewModel.ViewModels;
using ModelViewModel.DAL;
using ModelViewModel.Models;

namespace ModelViewModelTests
{
	public class TaskViewModelTest
	{
		// Helper methods
		private static SqliteConnection GetConnection()
		{
			var connection = new SqliteConnection("DataSource=:memory:");
			connection.Open();
			return connection;
		}

		private static TaskContext GetContext(SqliteConnection connection)
		{
			var options = new DbContextOptionsBuilder<TaskContext>().UseSqlite(connection).Options;
			var context = new TaskContext(options);
			return context;
		}

		private static void MigrateDatabase(SqliteConnection connection)
		{
			using (var context = GetContext(connection))
			{
				context.Database.Migrate();
			}
		}

		// Integration tests

		[Fact]
		public void Load_WhenDatabaseIsEmpty_ReturnsTaskWithNullName()
		{
			var connection = GetConnection();
			try
			{
				MigrateDatabase(connection);

				using (var context = GetContext(connection))
				{
					var repository = new Repository(context);
					var tvm = new TaskViewModel(repository);
					tvm.Load();
					Assert.Equal(null, tvm.Name);
				}
			}
			finally
			{
				connection.Close();
			}
		}

		[Fact]
		public void Load_WhenDatabaseContainsATask_ReturnsCorrectTask()
		{
			var taskName = Guid.NewGuid().ToString();
			var connection = GetConnection();
			try
			{
				MigrateDatabase(connection);
				using (var context = GetContext(connection))
				{
					var repository = new Repository(context);
					var tvm = new TaskViewModel(repository) {Name = taskName};
					tvm.Save();
				}
				using (var context = GetContext(connection))
				{
					var repository = new Repository(context);
					var tvm = new TaskViewModel(repository);
					tvm.Load();
					Assert.Equal(taskName, tvm.Name);
				}
			}
			finally
			{
				connection.Close();
			}
		}

		[Fact]
		public void Load_WhenExistingTaskIsUpdated_ReturnsCorrectTaskName()
		{
			const string taskName1 = "TaskName1";
			const string taskName2 = "TaskName2";
			var connection = GetConnection();
			try
			{
				MigrateDatabase(connection);

				// Add a task
				using (var context = GetContext(connection))
				{
					var repository = new Repository(context);
					var tvm = new TaskViewModel(repository) {Name = taskName1};
					tvm.Save();
				}
				// Load and update the task
				using (var context = GetContext(connection))
				{
					var repository = new Repository(context);
					var tvm = new TaskViewModel(repository);
					tvm.Load();
					tvm.Name = taskName2;
					tvm.Save();
				}
				using (var context = GetContext(connection))
				{
					var repository = new Repository(context);
					var tvm = new TaskViewModel(repository);
					tvm.Load();
					Assert.Equal(taskName2, tvm.Name);
				}
			}
			finally
			{
				connection.Close();
			}
		}

		[Fact]
		public void Task_UsingContextDirectly_CanBeSavedAndRetrieved()
		{
			var taskName1 = Guid.NewGuid().ToString();
			var connection = GetConnection();
			try
			{
				MigrateDatabase(connection);

				// Add a task
				using (var context = GetContext(connection))
				{
					var t = new Task {Name = taskName1};
					context.Tasks.Add(t);
					context.SaveChanges();
				}
				// Load the task
				using (var context = GetContext(connection))
				{
					var tasks = context.Tasks;
					var task = tasks.FirstOrDefault();
					Assert.Equal(taskName1, task.Name);
				}
			}
			finally
			{
				connection.Close();
			}
		}

		[Fact]
		public void Task_UsingContextDirectly_CanBeSavedUpdatedAndRetrieved()
		{
			const string taskName1 = "TaskName1";
			const string taskName2 = "TaskName2";
			var connection = GetConnection();
			try
			{
				MigrateDatabase(connection);

				// Add a task
				using (var context = GetContext(connection))
				{
					var t = new Task { Name = taskName1 };
					context.Tasks.Add(t);
					context.SaveChanges();
				}
				// Load and update the task
				using (var context = GetContext(connection))
				{
					var tasks = context.Tasks;
					var task = tasks.FirstOrDefault();
					task.Name = taskName2;
					context.SaveChanges();
				}
				using (var context = GetContext(connection))
				{
					var tasks = context.Tasks;
					var task = tasks.FirstOrDefault();
					Assert.Equal(taskName2, task.Name);
				}
			}
			finally
			{
				connection.Close();
			}
		}

		[Fact]
		public void TestAttachAndUpdate()
		{
			var taskName1 = Guid.NewGuid().ToString();
			var taskName2 = Guid.NewGuid().ToString();
			var connection = GetConnection();
			try
			{
				MigrateDatabase(connection);

				// Add a task
				using (var context = GetContext(connection))
				{
					var t = new Task { Name = taskName1 };
					context.Tasks.Add(t);
					context.SaveChanges();
				}
				// Load and update the task
				using (var context = GetContext(connection))
				{
					var tasks = context.Tasks;
					var task = tasks.FirstOrDefault();
					context.Attach(task);
					context.Update(task);
					task.Name = taskName2;
					context.SaveChanges();
				}
				using (var context = GetContext(connection))
				{
					var tasks = context.Tasks;
					var task = tasks.FirstOrDefault();
					Assert.Equal(taskName2, task.Name);
				}
			}
			finally
			{
				connection.Close();
			}
		}

		// Unit tests

		[Fact]
		public void Load_WhenStubDatabaseContainsATask_ReturnsCorrectTask()
		{
			var repository = new RepositoryStub();
			var tvm = new TaskViewModel(repository);
			tvm.Load();
			Assert.Equal("TestTask", tvm.Name);
		}


	}
}
