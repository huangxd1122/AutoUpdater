using System;
using System.Collections.Generic;
using System.Windows;
using AutoUpdater.Utils;
using AutoUpdater.ViewModels;

namespace AutoUpdater
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application, ISingleInstanceApp
    {
        private const string AppId = "{7F280539-0814-4F9C-95BF-D2BB60023657}";

        [STAThread]
        static void Main(string[] args)
        {
            //0.9.0.0 1.0.0.0 https://github.com/WELL-E http://localhost:9090/UpdateFile.zip E:\PlatformPath 2b406701f8ad92922feb537fc789561a
            //args = new string[] { "1.0.0.3","1.0.0.4","https://github.com/WELL-E","http://192.168.0.46:8080/UpdateFile/testfile.zip",
            //    AppDomain.CurrentDomain.BaseDirectory,"59401e942d3ca1a9098b09f69afe5682"};
            if (args.Length != 6) return;
            if (SingleInstance<App>.InitializeAsFirstInstance(AppId))
            {
                var win = new MainWindow();
                var vm = new MainWindowViewModel(args, win.Close);
                win.DataContext = vm;

                var application = new App();
                application.InitializeComponent();
                application.Run(win);
                SingleInstance<App>.Cleanup();
            }
        }

        public bool SignalExternalCommandLineArgs(IList<string> args)
        {
            if (this.MainWindow.WindowState == WindowState.Minimized)
            {
                this.MainWindow.WindowState = WindowState.Normal;
            }
            this.MainWindow.Activate();

            return true;
        }
    }
}
