using System;
using System.Windows.Controls;
using Telerik.Windows.Data;
using Telerik.Windows.Controls.DataServices;
using Telerik.Windows.Controls;
using System.Linq;

namespace RadControlsSilverlightClient
{
    public partial class Entities : UserControl
    {
        private FilterDescriptor AccountsDetailsFilter;
        public string current_username = string.Empty;
        public string[] editUsers;

        public Entities()
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
                    if (((sender as RadDataForm).CurrentItem as TDocs.Entities).entityID_pk == null || ((sender as RadDataForm).CurrentItem as TDocs.Entities).name == null)
                        e.Cancel = true;
                }
            }
            if (!e.Cancel)
            {
                if (((sender as RadDataForm).CurrentItem as TDocs.Entities).createddate == null)
                {
                    ((sender as RadDataForm).CurrentItem as TDocs.Entities).createddate = DateTime.Now;
                    ((sender as RadDataForm).CurrentItem as TDocs.Entities).createdby = @current_username;
                }
                ((sender as RadDataForm).CurrentItem as TDocs.Entities).modifieddate = DateTime.Now;
                ((sender as RadDataForm).CurrentItem as TDocs.Entities).modifiedby = @current_username;
                dataServiceDataSource.SubmitChanges();
            }
        }

        private void RadGridView1_SelectionChanged(object sender, Telerik.Windows.Controls.SelectionChangeEventArgs e)
        {
            AccountsdataServiceDataSource.FilterDescriptors.Remove(AccountsDetailsFilter);
            RadGridView Grid = (RadGridView)sender;
            RadControlsSilverlightClient.TDocs.Entities ent = (RadControlsSilverlightClient.TDocs.Entities)Grid.SelectedItem;
            if (ent != null)
            {
                AccountsDetailsFilter = new FilterDescriptor("entityID_fk", FilterOperator.IsEqualTo, ent.entityID_pk);
                AccountsdataServiceDataSource.FilterDescriptors.Add(AccountsDetailsFilter);
            }
        }

        private void myRadDataForm_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if (!editUsers.Contains(current_username.Replace("IHESS\\", string.Empty)))
                myRadDataForm.CommandButtonsVisibility = Telerik.Windows.Controls.Data.DataForm.DataFormCommandButtonsVisibility.None;
        }
    }
}
