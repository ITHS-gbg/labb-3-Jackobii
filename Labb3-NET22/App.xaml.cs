using Labb3_NET22.Managers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Labb3_NET22.ViewModels;

namespace Labb3_NET22
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly NavigationManager _navigationManager;

        public App()
        {
            _navigationManager = new NavigationManager();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _navigationManager.CurrentViewModel = new MainMenuViewModel(_navigationManager);
            var mainWindow = new MainWindow { DataContext = new MainWindowViewModel(_navigationManager) };
            mainWindow.Show();
            base.OnStartup(e);
        }
    }
}
