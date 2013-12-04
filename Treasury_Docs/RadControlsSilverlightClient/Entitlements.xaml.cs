using System;
using System.Windows.Controls;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.DataServices;
using System.Linq;

namespace RadControlsSilverlightClient
{
    public partial class Entitlements : UserControl
    {
        public string current_username = string.Empty;
        public string[] editUsers;

        public Entitlements()
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
            if (e.EditAction == Telerik.Windows.Controls.Data.DataForm.EditAction.Cancel || e.EditAction == Telerik.Windows.Controls.Data.DataForm.EditAction.Commit)
            {
                if ((sender as RadDataForm).CurrentItem != null)
                {
                    if (((sender as RadDataForm).CurrentItem as TDocs.Entitlements).entitlementID_pk == null || ((sender as RadDataForm).CurrentItem as TDocs.Entitlements).description == null)
                        e.Cancel = true;
                }
            }
            if (!e.Cancel)
            {
                if (((sender as RadDataForm).CurrentItem as TDocs.Entitlements).createddate == null)
                {
                    ((sender as RadDataForm).CurrentItem as TDocs.Entitlements).createddate = DateTime.Now;
                    ((sender as RadDataForm).CurrentItem as TDocs.Entitlements).createdby = @current_username;
                }
                ((sender as RadDataForm).CurrentItem as TDocs.Entitlements).modifieddate = DateTime.Now;
                ((sender as RadDataForm).CurrentItem as TDocs.Entitlements).modifiedby = @current_username;
                dataServiceDataSource.SubmitChanges();
            }
        }

        private void myRadDataForm_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if (!editUsers.Contains(current_username.Replace("IHESS\\", string.Empty)))
                myRadDataForm.CommandButtonsVisibility = Telerik.Windows.Controls.Data.DataForm.DataFormCommandButtonsVisibility.None;
        }
    }
}
