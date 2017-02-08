using ModelViewModel.Models;

namespace ModelViewModel.DAL
{
	public interface IRepository
	{
		Task LoadTask();
		void SaveTask(Task model);
	}
}