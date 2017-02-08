using ModelViewModel.DAL;
using ModelViewModel.Models;

namespace ModelViewModel.ViewModels
{
	public class TaskViewModel : NotificationBase<Task>
	{
		private readonly IRepository _repository;

		public TaskViewModel()
		{
			_repository = new Repository();
		}

		public TaskViewModel(IRepository repository)
		{
			_repository = repository;
		}

		public string Name
		{
			get { return This.Name; }
			set { SetProperty(This.Name, value, () => This.Name = value); }
		}

		public void Save()
		{
			_repository.SaveTask(This);
		}

		public void Load()
		{
			var task = _repository.LoadTask() ?? new Task();
			SetModel(task);
			Name = task.Name;
		}
	}
}
