using Stylet;
using StyletWpfApp.ViewResources.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StyletWpfApp.ViewModels
{
    public class SettingsViewModel : Screen
    {
        private IWindowManager WindowManager { get; set; }
        public void Throw()
        {
            try
            {
                File.ReadAllBytes("D:\\void.ini");
            }
            catch (Exception ex)
            {
                WindowManager.Show($"{ex.Message}", "Error", isOption: true);
                WindowManager.Error($"{ex.Message}", $"{ex.Source}.{ex.TargetSite}\n\n[Stack Trace]\n{ex.StackTrace}\n\n[Inner Exception]\n{ex.InnerException}");
            }
        }

        public SettingsViewModel(IWindowManager windowManager)
        {
            WindowManager = windowManager;
        }
    }
}
