using System;
using System.Windows.Controls;

namespace RadControlsSilverlightClient
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void myRadDataForm_EditEnding(object sender, Telerik.Windows.Controls.Data.DataForm.EditEndingEventArgs e)
        {
            dataServiceDataSource.SubmitChanges();
        }
    }
}
