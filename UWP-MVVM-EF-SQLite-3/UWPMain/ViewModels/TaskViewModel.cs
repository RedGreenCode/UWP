using UWPMain.Models;
using UWPMain.DAL;

namespace UWPMain.ViewModels
{
	public class TaskViewModel : NotificationBase<Task>
	{
		private Task _task;
		private readonly Repository _repository;

		public TaskViewModel()
		{
			_task = new Task();
			_repository = new Repository();
		}

		public string Name
		{
			get { return This.Name; }
			set { SetProperty(This.Name, value, () => This.Name = value); }
		}

		private int Id
		{
			set { SetProperty(This.Id, value, () => This.Id = value); }
		}

		public void Save()
		{
			_repository.SaveTask(This);
		}

		public void Load()
		{
			_task = _repository.LoadTask();
			Name = _task.Name;
			Id = _task.Id;
		}
	}
}
