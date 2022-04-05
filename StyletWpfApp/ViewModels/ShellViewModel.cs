#pragma warning disable CS0108
#pragma warning disable CS8612
#pragma warning disable CS8618

using Stylet;
using StyletWpfApp.ViewResources.Helpers;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;

namespace StyletWpfApp.ViewModels
{
    public class ShellViewModel : Screen
    {
        /// 
        /// App parameters
        /// 
        public static int MinHeight { get; set; } = 400;
        public static int MinWidth { get; set; } = 600;
        public static bool CanResize { get; set; } = true;
        public string HelpLink { get; set; } = "https://github.com/ArchLeaders";
        public string GitHubRepo { get; set; } = "stylet-wpf-template";
        public ulong ServerID { get; set; } = 0;
        public string Title { get; set; } = "$projectname$";

        ///
        /// Actions
        ///
        #region Actions

        #endregion

        ///
        /// Properties
        ///
        #region Properties

        private string _helloWorld = "Hello World!";
        public string HelloWorld
        {
            get => _helloWorld;
            set => SetAndNotify(ref _helloWorld, value);
        }

        #endregion

        ///
        /// Bindings
        ///
        #region Bindings

        private Visibility _handledExceptionViewVisibility = Visibility.Collapsed;
        public Visibility HandledExceptionViewVisibility
        {
            get => _handledExceptionViewVisibility;
            set => SetAndNotify(ref _handledExceptionViewVisibility, value);
        }

        #endregion

        ///
        /// DataContext
        ///
        #region DataContext

        // Views
        public SettingsViewModel? SettingsViewModel { get; set; } = null;
        
        private HandledExceptionViewModel? _handledExceptionViewModel = null;
        public HandledExceptionViewModel? HandledExceptionViewModel
        {
            get => _handledExceptionViewModel;
            set => SetAndNotify(ref _handledExceptionViewModel, value);
        }

        // App
        public bool CanFullscreen { get; set; } = CanResize;
        public ResizeMode ResizeMode { get; set; } = CanResize ? ResizeMode.CanResize : ResizeMode.CanMinimize;
        public WindowStyle WindowStyle { get; set; } = CanResize ? WindowStyle.None : WindowStyle.SingleBorderWindow;

        public void ThrowException(HandledExceptionViewModel ex)
        {
            WindowManager.Error(ex.Message, ex.StackText, ex.Title);
            HandledExceptionViewModel = ex;
            HandledExceptionViewVisibility = Visibility.Visible;
        }

        public void Help()
        {
            Process proc = new();

            proc.StartInfo.FileName = "explorer.exe";
            proc.StartInfo.Arguments = HelpLink;

            proc.Start();
        }

        public IWindowManager WindowManager { get; set; }
        public ShellViewModel(IWindowManager windowManager)
        {
            WindowManager = windowManager;
            SettingsViewModel = new(this);
        }

        #endregion
    }
}
