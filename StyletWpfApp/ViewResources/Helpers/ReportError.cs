using StyletWpfApp.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Operations;
using System.Text;
using System.Threading.Tasks;

namespace StyletWpfApp.ViewResources.Helpers
{
    public class ReportError
    {
        public static async Task Discord(HandledExceptionViewModel ex)
        {
            throw new NotImplementedException();
        }

        public static async Task GitHub(UnhandledExceptionViewModel ex)
        {
            throw new NotImplementedException();
        }

        public static async Task Markdown(HandledExceptionViewModel ex)
        {
            throw new NotImplementedException();
        }

        public static async Task Markdown(UnhandledExceptionViewModel ex)
        {
            throw new NotImplementedException();
        }

        public static string Markdown(string title, string message, string stack, string user)
        {
            return new(
                $"# {title}\n" +
                $"> {user}\n\n" +
                $"{message}\n\n" +
                $"```\n{stack}\n```"
            );
        }

        public static async Task HtmlView(HandledExceptionViewModel ex)
        {
            string appdata = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string user = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

            string htmlFile = $"{appdata}\\Temp\\{new Random().Next(1000, 9999)}-{new Random().Next(1000, 9999)} - {ex.Message} - index.htm";
            string fullReport = HtmlView(ex.Title, ex.Message, ex.StackText, ex.User)
                .Replace(user, "C:\\Users\\admin");

            await File.WriteAllTextAsync(htmlFile, fullReport);
            await Execute.Explorer($"\"{htmlFile}\"");
        }

        public static async Task HtmlView(UnhandledExceptionViewModel ex)
        {
            string appdata = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string user = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

            string htmlFile = $"{appdata}\\Temp\\{new Random().Next(1000, 9999)}-{new Random().Next(1000, 9999)} - {ex.Message} - index.htm";
            string fullReport = HtmlView(ex.Title, ex.MessageText, ex.Stack, "Anonymous")
                .Replace(user, "C:\\Users\\admin");

            await File.WriteAllTextAsync(htmlFile, fullReport);
            await Execute.Explorer($"\"{htmlFile}\"");
        }

        public static string HtmlView(string title, string message, string stack, string user)
        {
            // - - Make an actuall class to create this in future - -

            #region HTML Meta-data/CSS

            string css = new(
                "<style>" +
                "html {" +
                "    background: #121212;" +
                "    color: #e1e1e1;" +
                "    font-family: 'Ubuntu';" +
                "    padding: 15px;" +
                "}" +
                "hr {" +
                "    border-color: #7160E8;" +
                "    margin-left: 30px;" +
                "    margin-right: 30px;" +
                "}" +
                "h1 {" +
                "    padding-top: 15px;" +
                "    padding-bottom: 0px;" +
                "    font-size: 40px;" +
                "    padding-left: 30px;" +
                "    margin-bottom: 0px;" +
                "}" +
                "#contact {" +
                "    padding-top: 15px;" +
                "    padding-bottom: 0px;" +
                "    font-size: 20px;" +
                "    padding-left: 30px;" +
                "}" +
                "h3 {" +
                "    padding-left: 50px;" +
                "}" +
                "p {" +
                "    padding-left: 60px;" +
                "    font-size: 26px;" +
                "    font-weight: lighter;" +
                "}" +
                "span {" +
                "    font-style: italic;" +
                "    color: #797979;" +
                "}" +
                "code {" +
                "display: inline-block;" +
                "    background: #353535;" +
                "    margin-left: 60px;" +
                "    padding: 5px;" +
                "    padding-left: 10px;" +
                "    padding-right: 10px;" +
                "    border-radius: 5px;" +
                "    line-height: 25px;" +
                "    font-family: 'Ubuntu Mono', monospace;;" +
                "    font-size: 14px;" +
                "    font-weight: bold;" +
                "}" +
                "</style>"
            );

            string htmlHeader = new(
                $"<!DOCTYPE html>" +
                $"<html lang=\"en\">" +
                $"<head>" +
                $"\t<meta charset=\"UTF-8\">" +
                $"\t<meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\">" +
                $"\t<meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">" +
                $"\t<link rel=\"preconnect\" href=\"https://fonts.googleapis.com\">\n" +
                $"\t<link rel=\"preconnect\" href=\"https://fonts.gstatic.com\" crossorigin>\n" +
                $"\t<link href=\"https://fonts.googleapis.com/css2?family=Ubuntu+Mono&family=Ubuntu:wght@300&display=swap\" rel=\"stylesheet\">\n" +
                $"\t<title>{title}</title>{css}" +
                $"</head>" +
                $"<body>"
            );

            string htmlFooter = new(
                $"</body>" +
                $"</html>"
            );

            #endregion

            string body = new(
                $"<span>*The contents of this page will be uploaded to a public or private GitHub repository</span><br>" +
                $"<h1>{title}</h1>" +
                $"<label id=\"contact\">{user}<label><hr>" +
                $"<p>{message}</p>" +
                $"<code>{stack}</code>"
            );

            return new(
                $"{htmlHeader}{body}{htmlFooter}"
            );
        }
    }
}
