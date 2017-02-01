using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Xunit;

using ModelViewModel.ViewModels;

namespace ModelViewModelTests
{
	public class TaskViewModelTest
	{
		[Fact]
		public void Test1()
		{
			var tvm = new TaskViewModel {Name = "Test1"};
			tvm.Save();
			tvm.Load();
			Assert.Equal("Test1", tvm.Name);
		}
	}
}
