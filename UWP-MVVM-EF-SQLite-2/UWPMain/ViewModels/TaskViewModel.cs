using UWPMain.Models;

namespace UWPMain.ViewModels
{
	public class TaskViewModel : NotificationBase<Task>
	{
		private Task _task;

		public TaskViewModel()
		{
			_task = new Task();
		}

		public string Name
		{
			get { return This.Name; }
			set { SetProperty(This.Name, value, () => This.Name = value); }
		}
	}
}
