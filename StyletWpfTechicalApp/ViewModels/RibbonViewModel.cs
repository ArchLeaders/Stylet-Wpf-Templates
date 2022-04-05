using Stylet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StyletWpfApp.ViewModels
{
    public class RibbonViewModel : Screen
    {
        private Dictionary<string, Visibility> _category = new();
        public Dictionary<string, Visibility> Category
        {
            get => _category;
            set => SetAndNotify(ref _category, value);
        }
    }
}
