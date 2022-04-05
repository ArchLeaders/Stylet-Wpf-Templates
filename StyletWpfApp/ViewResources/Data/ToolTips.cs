using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StyletWpfApp.ViewResources.Data
{
    public class ToolTips
    {
        public static string ReportError
        {
            get => "Reporting an error will upload system information such as file paths to a private or public GitHub Repository.\n" +
                "Your username is hidden; however, any other names or information in file/folder paths will be uploaded.\n" +
                "Please regard this before proceeding.\n\n" +
                "Auto reporting will expire on 12/31/2022 or sooner to maintain security.";
        }
    }
}
