using System;
using System.Windows.Controls;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.DataServices;
using System.Linq;

namespace RadControlsSilverlightClient
{
    public partial class Citizenships : UserControl
    {
        public string current_username = string.Empty;
        public string[] editUsers;

        public Citizenships()
        {
            InitializeComponent();
            dataServiceDataSource.SubmittedChanges += new EventHandler<DataServiceSubmittedChangesEventArgs>(dataServiceDataSource_SubmittedChanges);
        }

        void dataServiceDataSource_SubmittedChanges(object sender, DataServiceSubmittedChangesEventArgs e)
        {
            if (e.HasError)
                e.MarkErrorAsHandled();

            dataServiceDataSource.Load();
        }

        private void myRadDataForm_EditEnding(object sender, Telerik.Windows.Controls.Data.DataForm.EditEndingEventArgs e)
        {

            if (e.EditAction == Telerik.Windows.Controls.Data.DataForm.EditAction.Cancel ||
                e.EditAction == Telerik.Windows.Controls.Data.DataForm.EditAction.Commit)
            {
                if ((sender as RadDataForm).CurrentItem != null)
                {
                    if (((sender as RadDataForm).CurrentItem as TDocs.Citizenships).citizenshipCode == null || ((sender as RadDataForm).CurrentItem as TDocs.Citizenships).citizenshipName == null)
                        e.Cancel = true;
                }
            }
            if (!e.Cancel)
            {
                if (((sender as RadDataForm).CurrentItem as TDocs.Citizenships).createddate == null)
                {
                    ((sender as RadDataForm).CurrentItem as TDocs.Citizenships).createddate = DateTime.Now;
                    ((sender as RadDataForm).CurrentItem as TDocs.Citizenships).createdby = @current_username;
                }
                ((sender as RadDataForm).CurrentItem as TDocs.Citizenships).modifieddate = DateTime.Now;
                ((sender as RadDataForm).CurrentItem as TDocs.Citizenships).modifiedby = @current_username;
                dataServiceDataSource.SubmitChanges();
            }
        }

        private void RadGridView1_SelectionChanged(object sender, Telerik.Windows.Controls.SelectionChangeEventArgs e)
        {
        }

        private void myRadDataForm_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if (!editUsers.Contains(current_username.Replace("IHESS\\", string.Empty)))
                myRadDataForm.CommandButtonsVisibility = Telerik.Windows.Controls.Data.DataForm.DataFormCommandButtonsVisibility.None;
        }
    }
}
