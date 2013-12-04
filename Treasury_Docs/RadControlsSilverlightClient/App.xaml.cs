using System;
using System.Windows;
using System.Collections.Generic;

namespace RadControlsSilverlightClient
{
    public partial class App : Application
    {
        private string UserName { get; set; }
        private string[] EditUsers { get; set; }
        private string[] ReadOnlyUsers { get; set; }

        public App()
        {
            this.Startup += this.Application_Startup;
            this.Exit += this.Application_Exit;
            this.UnhandledException += this.Application_UnhandledException;

            InitializeComponent();
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            string userName = e.InitParams["Params"].Split('|')[0];
            string editUsers = e.InitParams["Params"].Split('|')[1];

            this.UserName = userName.Length > 0 ? userName : "UNKNOWN";
            this.EditUsers = editUsers.Split(';');

            this.RootVisual = new Tabs(this.UserName, this.EditUsers);
        }

        private void Application_Exit(object sender, EventArgs e)
        {
        }

        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            if (!System.Diagnostics.Debugger.IsAttached)
            {
            }
            e.Handled = true;
        }

        private void ReportErrorToDOM(ApplicationUnhandledExceptionEventArgs e)
        {
            try
            {
                string errorMsg =  e.ExceptionObject.InnerException.ToString()  ;
                errorMsg = errorMsg.Replace('"', '\'').Replace("\r\n", @"\n");

                System.Windows.Browser.HtmlPage.Window.Eval("throw new Error(\"Unhandled Error in Silverlight Application " + errorMsg + "\");");
            }
            catch { }
        }
    }
}
