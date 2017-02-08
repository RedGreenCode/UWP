using System;

using ModelViewModel.DAL;
using ModelViewModel.Models;

namespace ModelViewModelTests
{
	public class RepositoryStub : IRepository
	{
		public Task LoadTask()
		{
			return new Task {Name = "TestTask"};
		}

		public void SaveTask(Task model)
		{
			// do nothing
		}
	}
}
