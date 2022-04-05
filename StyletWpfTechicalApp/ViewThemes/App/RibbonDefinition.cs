using MaterialDesignThemes.Wpf;
using Stylet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StyletWpfApp.ViewResources.Helpers
{
    public delegate void ActionHandler(IWindowManager window);
    public class RibbonDefinition
    {
        public static Dictionary<string, RibbonDefinition>? StaticRibbonDictionary = new();

        public ActionHandler? Action { get; set; } = null;
        public PackIconKind? Icon { get; set; } = null;
        public string? Header { get; set; } = null;
        public string? ToolTip { get; set; } = null;
        public bool IsSeperator { get; set; } = false;
        public List<RibbonDefinition>? SubItems { get; set; } = null;

        public RibbonDefinition(string? header, ActionHandler? action = null, PackIconKind? icon = null, string? toolTip = null, params RibbonDefinition[] subItems)
        {
            Header = header;
            Icon = icon;
            ToolTip = toolTip ?? header;
            Action = action;

            SubItems = subItems.ToList();

            if (SubItems.Count == 0)
                SubItems = null;
        }

        public RibbonDefinition()
        {
            IsSeperator = true;
        }
    }
}
