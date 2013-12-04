using System;
using System.Windows.Controls;
using Telerik.Windows.Data;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.DataServices;
using System.Linq;

namespace RadControlsSilverlightClient
{
    public partial class Banks : UserControl
    {
        private FilterDescriptor BankContactsDetailsFilter;
        private FilterDescriptor BankAccountsDetailsFilter;
        public string current_username = string.Empty;
        public string[] editUsers;

        public Banks()
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
                    if (((sender as RadDataForm).CurrentItem as TDocs.Banks).name == null)//|| ((sender as RadDataForm).CurrentItem as TDocs.Banks).abanum == null)
                        e.Cancel = true;
                }
            }
            if (!e.Cancel)
            {
                if (((sender as RadDataForm).CurrentItem as TDocs.Banks).createddate == null)
                {
                    ((sender as RadDataForm).CurrentItem as TDocs.Banks).createddate = DateTime.Now;
                    ((sender as RadDataForm).CurrentItem as TDocs.Banks).createdby = @current_username;
                }
                ((sender as RadDataForm).CurrentItem as TDocs.Banks).modifieddate = DateTime.Now;
                ((sender as RadDataForm).CurrentItem as TDocs.Banks).modifiedby = @current_username;
                
                dataServiceDataSource.SubmitChanges();
            }
        }

        private void RadGridView1_SelectionChanged(object sender, Telerik.Windows.Controls.SelectionChangeEventArgs e)
        {
            ContactsdataServiceDataSource.FilterDescriptors.Remove(BankContactsDetailsFilter);
            AccountsdataServiceDataSource.FilterDescriptors.Remove(BankAccountsDetailsFilter);
            RadGridView Grid = (RadGridView)sender;
            RadControlsSilverlightClient.TDocs.Banks b = (RadControlsSilverlightClient.TDocs.Banks)Grid.SelectedItem;
            if (b != null)
            {
                BankContactsDetailsFilter = new FilterDescriptor("bankID_fk", FilterOperator.IsEqualTo, b.bankID_pk);
                ContactsdataServiceDataSource.FilterDescriptors.Add(BankContactsDetailsFilter);
                BankAccountsDetailsFilter = new FilterDescriptor("bankID_fk", FilterOperator.IsEqualTo, b.bankID_pk);
                AccountsdataServiceDataSource.FilterDescriptors.Add(BankAccountsDetailsFilter);
            }
        }

        private void myRadDataForm_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if (!editUsers.Contains(current_username.Replace("IHESS\\", string.Empty)))
                myRadDataForm.CommandButtonsVisibility = Telerik.Windows.Controls.Data.DataForm.DataFormCommandButtonsVisibility.None;
        }
    }
}
