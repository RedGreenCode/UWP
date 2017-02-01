using System.Linq;
using ModelViewModel.Models;

namespace ModelViewModel.DAL
{
	public class Repository
	{
		public void SaveTask(Task model)
		{
			using (var db = new TaskContext())
			{
				if (model.Id > 0)
				{
					db.Attach(model);
					db.Update(model);
				}
				else
				{
					db.Add(model);
				}
				db.SaveChanges();
			}
		}

		public Task LoadTask()
		{
			using (var db = new TaskContext())
			{
				return (from t in db.Tasks
						select t).LastOrDefault();
			}
		}
	}
}
