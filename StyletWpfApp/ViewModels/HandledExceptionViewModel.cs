using Stylet;
using StyletWpfApp.ViewResources.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Formatting;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace StyletWpfApp.ViewModels
{
    public class HandledExceptionViewModel : Screen
    {
        public async Task Report()
        {
            await ReportError.Markdown(this);
        }

        public async Task Copy()
        {
            await ReportError.HtmlView(this);
        }

        private string _title = "Handled Exception";
        public string Title
        {
            get => _title;
            set => SetAndNotify(ref _title, value);
        }

        private string _message = "No details were provided.";
        public string Message
        {
            get => _message;
            set => SetAndNotify(ref _message, value);
        }

        private TextBlock _stack = new();
        public TextBlock Stack
        {
            get => _stack;
            set => SetAndNotify(ref _stack, value);
        }

        private bool _isReportable;
        public bool IsReportable
        {
            get => _isReportable;
            set => SetAndNotify(ref _isReportable, value);
        }

        private string _user = "Anonymous";
        public string User
        {
            get => _user;
            set => SetAndNotify(ref _user, value);
        }

        public string StackText { get; set; } = "";

        public HandledExceptionViewModel(string title, string message, string stack, bool isReportable = true)
        {
            Title = title;
            Message = message;
            StackText = stack;
            Stack = stack.ToTextBlock();
            IsReportable = isReportable;
        }
    }
}
