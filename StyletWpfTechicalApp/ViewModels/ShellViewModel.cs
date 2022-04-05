#pragma warning disable CS0108
#pragma warning disable CS8612
#pragma warning disable CS8618

using MaterialDesignThemes.Wpf;
using Stylet;
using StyletWpfApp.ViewModels;
using StyletWpfApp.ViewResources.Helpers;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace StyletWpfApp.ViewModels
{
    public class ShellViewModel : Screen, INotifyPropertyChanged
    {
        /// 
        /// App parameters
        /// 
        public static int MinHeight { get; set; } = 600;
        public static int MinWidth { get; set; } = 1000;
        public static bool CanResize { get; set; } = true;
        public string HelpLink { get; set; } = "https://github.com/ArchLeaders";
        public string Title { get; set; } = "$projectname$";

        ///
        /// Ribbon Actions
        ///
        #region Ribbon Actions

        ///
        /// Home Ribbon Actions
        ///
        #region Home Ribbon Actions

        public void ClickRibbonTab(string name)
        {
            // Show ribbon {name}
            Ribbon = Helpers.DrawRibbon(WindowManager, name);
        }

        public void OpenFile(IWindowManager windowManager)
        {

        }

        public void SaveFile(IWindowManager windowManager)
        {

        }

        public void SaveFileAs(IWindowManager windowManager)
        {

        }

        public void Close(IWindowManager windowManager)
        {

        }

        #endregion

        ///
        /// Edit Ribbon Actions
        ///
        #region Edit Ribbon Actions



        #endregion

        #endregion

        ///
        /// Properties
        ///
        #region Properties

        #endregion

        ///
        /// Bindings
        ///
        #region Bindings

        private UIElement? _ribbon = null;
        public UIElement? Ribbon
        {
            get => _ribbon;
            set => SetAndNotify(ref _ribbon, value);
        }

        private StackPanel? _tabs = null;
        public StackPanel? Tabs
        {
            get => _tabs;
            set
            {
                // Tabs connot be SetAndNotify (System.ExecutionEngineException) when _tab is not null
                // Setting it as null first is added as a work-around for this behavior
                _tabs = null;
                SetAndNotify(ref _tabs, value); 
            }
        }

        #endregion

        ///
        /// DataContext
        ///
        #region DataContext

        // Views
        public SettingsViewModel? SettingsViewModel { get; set; } = null;

        // App
        public bool CanFullscreen { get; set; } = CanResize;
        public ResizeMode ResizeMode { get; set; } = CanResize ? ResizeMode.CanResize : ResizeMode.CanMinimize;
        public WindowStyle WindowStyle { get; set; } = CanResize ? WindowStyle.None : WindowStyle.SingleBorderWindow;

        public void Help()
        {
            Process proc = new();

            proc.StartInfo.FileName = "explorer.exe";
            proc.StartInfo.Arguments = HelpLink;

            proc.Start();
        }

        private IWindowManager WindowManager { get; set; }
        public ShellViewModel(IWindowManager windowManager)
        {
            WindowManager = windowManager;
            SettingsViewModel = new(windowManager);

            RibbonDefinition.StaticRibbonDictionary = SetRibbonDefaults();

            List<string> tabs = new();
            foreach (var key in RibbonDefinition.StaticRibbonDictionary.Keys)
                tabs.Add(key);

            Tabs = Helpers.DrawTabs(ClickRibbonTab, tabs.ToArray());
            Ribbon = Helpers.DrawRibbon(WindowManager, "Home");

            Dictionary<string, RibbonDefinition> SetRibbonDefaults()
            {
                Dictionary<string, RibbonDefinition> ribbon = new();

                // Home

                RibbonDefinition home = new();

                home.SubItems = new() {
                    new RibbonDefinition("Open", OpenFile, PackIconKind.FileReplace, "Open a txt file"),
                    new RibbonDefinition("New", OpenFile, PackIconKind.File, "Create a new txt file"),
                    new RibbonDefinition(),
                    new RibbonDefinition("Save", SaveFile, PackIconKind.ContentSave, "Save the open file"),
                    new RibbonDefinition("Save As", SaveFileAs, PackIconKind.ContentSaveMove, "Save the open file"),
                    new RibbonDefinition(),
                    new RibbonDefinition(null, Close, PackIconKind.Close, "Close the application"),
                };

                // Edit

                RibbonDefinition edit = new();

                edit.SubItems = new()
                {
                    new RibbonDefinition("Open", OpenFile, PackIconKind.FileReplace, "Open a txt file"),
                    new RibbonDefinition("New", OpenFile, PackIconKind.File, "Create a new txt file"),
                    new RibbonDefinition(),
                    new RibbonDefinition("Save", SaveFile, PackIconKind.ContentSave, "Save the open file"),
                    new RibbonDefinition("Save As", SaveFileAs, PackIconKind.ContentSaveMove, "Save the open file"),
                    new RibbonDefinition(),
                    new RibbonDefinition(null, Close, PackIconKind.Close, "Close the application"),
                };

                // View
                // Tools

                ribbon.Add("Home", home);
                ribbon.Add("Edit", edit);

                return ribbon;
            }
        }

        #endregion
    }
}
