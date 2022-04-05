using MaterialDesignThemes.Wpf;
using Stylet;
using StyletWpfApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace StyletWpfApp.ViewResources.Helpers
{
    public class Helpers
    {
        public delegate void ClickTab(string name);
        public static StackPanel DrawTabs(ClickTab click, params string[] tabs)
        {
            // Create the initial stack panel
            bool isFirst = true;
            StackPanel stack = new() { Orientation = Orientation.Horizontal };

            // Add tab items
            foreach (var tab in tabs)
            {
                RadioButton tabButton = new()
                {
                    Style = (Style)Application.Current.Resources["TabButton"],
                    IsChecked = isFirst,
                    Content = tab
                };

                tabButton.Checked += (s, e) => click(tab);

                stack.Children.Add(tabButton);

                isFirst = false;
            }

            return stack;
        }

        public static UIElement DrawRibbon(IWindowManager window, string ribbon)
        {
            PaletteHelper helper = new();
            ITheme theme = helper.GetTheme();
            SolidColorBrush foreground = new(theme.Body);

            // Look for the provided ribbon
            if (!RibbonDefinition.StaticRibbonDictionary.ContainsKey(ribbon))
            {
                return new TextBlock()
                {
                    Text = $"The ribbon key '{ribbon}' was not found in StaticRibbonDictionary.",
                    Foreground = new SolidColorBrush(theme.ValidationError),
                    Focusable = false,
                    Margin = new(0)
                };
            }

            // Create the initial menu stack
            Menu menu = new()
            {
                IsTabStop = false
            };

            if (RibbonDefinition.StaticRibbonDictionary[ribbon].SubItems != null)
            {
                foreach (var item in RibbonDefinition.StaticRibbonDictionary[ribbon].SubItems)
                {
                    if (!item.IsSeperator)
                    {
                        MenuItem menuItem = new()
                        {
                            Header = item.Header,
                            Icon = item.Icon != null ? item.Icon.Value.ToString() : "Abacus",
                            ToolTip = item.ToolTip
                        };

                        if (item.SubItems != null)
                        {
                            foreach (var subItem in item.SubItems)
                            {
                                menuItem.Items.Add(DrawMenu(subItem));
                            }
                        }
                        else if (item.Action != null)
                        {
                            menuItem.Click += (s, e) => item.Action(window);
                        }


                        menu.Items.Add(menuItem);
                    }
                    else
                    {
                        menu.Items.Add(new Separator() { IsEnabled = false });
                    }
                }
            }

            return menu;

            UIElement DrawMenu(RibbonDefinition item)
            {
                if (!item.IsSeperator)
                {
                    MenuItem menuItem = new()
                    {
                        Header = item.Header,
                        Icon = item.Icon != null ? item.Icon.Value.ToString() : "Abacus",
                        ToolTip = item.ToolTip
                    };

                    if (item.SubItems != null)
                    {
                        foreach (var subItem in item.SubItems)
                        {
                            menuItem.Items.Add(DrawMenu(subItem));
                        }
                    }
                    else if (item.Action != null)
                    {
                        menuItem.Click += (s, e) => item.Action(window);
                    }

                    return menuItem;
                }
                else
                {
                    return new Separator();
                }
            }
        }
    }
}
