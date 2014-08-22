using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Jace.PclDemoApp.Resources;

namespace Jace.PclDemoApp
{
	public partial class MainPage : PhoneApplicationPage
	{
		private MainPageViewModel _viewModel;
		// Constructor
		public MainPage()
		{
			InitializeComponent();
			this.DataContext = _viewModel = new MainPageViewModel();
		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			_viewModel.IsActive = true;

			base.OnNavigatedTo(e);
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			NavigationService.Navigate(new Uri("/VariablesPage.xaml", UriKind.Relative));
		}
	}
}