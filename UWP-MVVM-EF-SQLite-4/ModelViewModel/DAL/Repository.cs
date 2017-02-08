using System.Linq;
using Microsoft.EntityFrameworkCore;
using ModelViewModel.Models;

namespace ModelViewModel.DAL
{
	public class Repository : IRepository
	{
		private readonly TaskContext _taskContext;

		public Repository(TaskContext taskContext)
		{
			_taskContext = taskContext;
		}

		public Repository()
		{
			_taskContext = null;
		}

		public void SaveTask(Task model)
		{
			TaskContext taskContext;
			taskContext = _taskContext ?? new TaskContext();

			if (model.Id == 0) taskContext.Add(model);
			else taskContext.Entry(model).State = EntityState.Modified;
			taskContext.SaveChanges();
		}

		public Task LoadTask()
		{
			var taskContext = _taskContext ?? new TaskContext();

			var task = (from t in taskContext.Tasks
					select t).LastOrDefault();
			return task;
		}
	}
}
