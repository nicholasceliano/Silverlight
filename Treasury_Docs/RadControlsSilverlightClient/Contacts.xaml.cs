using System;
using System.Windows.Controls;
using System.ComponentModel;
using Telerik.Windows.Data;
using Telerik.Windows.Controls.DataServices;
using Telerik.Windows.Controls;
using System.Linq;

namespace RadControlsSilverlightClient
{
    public partial class Contacts : UserControl
    {
       public string current_username = string.Empty;
       public string[] editUsers;
       public int originalPageSize = 0;
       public int originalPageIndex = 0;

        public Contacts()
        {
            InitializeComponent();
            SortDescriptor banksSD = new SortDescriptor();
            banksSD.Member = "name";
            banksSD.SortDirection = ListSortDirection.Ascending;
            this.BanksdataServiceDataSource.SortDescriptors.Add(banksSD);
            
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
                    if (((sender as RadDataForm).CurrentItem as TDocs.Contacts).contactID_pk == null)// || ((sender as RadDataForm).CurrentItem as TDocs.Contacts).lastname == null)
                        e.Cancel = true;
                }
            }
            if (!e.Cancel)
            {
                if (((sender as RadDataForm).CurrentItem as TDocs.Contacts).createddate == null)
                {
                    ((sender as RadDataForm).CurrentItem as TDocs.Contacts).createddate = DateTime.Now;
                    ((sender as RadDataForm).CurrentItem as TDocs.Contacts).createdby = @current_username;
                }
                ((sender as RadDataForm).CurrentItem as TDocs.Contacts).modifieddate = DateTime.Now;
                ((sender as RadDataForm).CurrentItem as TDocs.Contacts).modifiedby = @current_username;
                dataServiceDataSource.SubmitChanges();
            }
        }

        private void radDataPager1_PageIndexChanged(object sender, PageIndexChangedEventArgs e)
        {
            originalPageIndex = e.NewPageIndex;
        }

        private void myRadDataForm_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if (!editUsers.Contains(current_username.Replace("IHESS\\", string.Empty)))
                myRadDataForm.CommandButtonsVisibility = Telerik.Windows.Controls.Data.DataForm.DataFormCommandButtonsVisibility.None;
        }
    }
}
