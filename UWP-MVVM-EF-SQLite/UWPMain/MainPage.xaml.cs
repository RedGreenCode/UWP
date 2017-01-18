using Windows.Foundation;
using Windows.UI.ViewManagement;

using UWPMain.ViewModels;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UWPMain
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class MainPage
	{
		public MainPage()
		{
			InitializeComponent();
			ApplicationView.PreferredLaunchViewSize = new Size(700, 300);
			ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;

			Task = new TaskViewModel();
		}

		public TaskViewModel Task { get; set; }
	}
}
