using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Browser;

namespace RadControlsSilverlightClient
{
    public partial class Reports : UserControl
    {
        public string current_username = string.Empty;

        public Reports()
        {
            InitializeComponent();

            rptActiveSigners.current_username = current_username;
            rptEPMonthlyAccountActivity.current_username = current_username;
            rptFBARSummary.current_username = current_username;
        }
    }
}
