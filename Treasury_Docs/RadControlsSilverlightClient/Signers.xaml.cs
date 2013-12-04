using System;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;
using Telerik.Windows.Data;
using Telerik.Windows.Controls.DataServices;
using Telerik.Windows.Controls;
using RadControlsSilverlightClient.TDocs;
using System.Linq;

namespace RadControlsSilverlightClient
{
    public partial class Signers : UserControl
    {
        public string current_username = string.Empty;
        public string[] editUsers;
        private FilterDescriptor DetailsFilter;
        public string firstName = string.Empty;
        public string lastName = string.Empty;
        public string middleName = string.Empty;
        public string title = string.Empty;
        public int citizenshipID;
        public bool activeID;

        public Signers()
        {
            InitializeComponent();

            SortDescriptor citizenshipsSD = new SortDescriptor();

            citizenshipsSD.Member = "citizenshipName";
            citizenshipsSD.SortDirection = ListSortDirection.Ascending;
            this.CitizenshipsdataServiceDataSource.SortDescriptors.Add(citizenshipsSD);

            dataServiceDataSource.SubmittedChanges += new EventHandler<DataServiceSubmittedChangesEventArgs>(dataServiceDataSource_SubmittedChanges);

        }

        void dataServiceDataSource_SubmittedChanges(object sender, DataServiceSubmittedChangesEventArgs e)
        {
            if (e.HasError)
                e.MarkErrorAsHandled();
            
            SignersViewdataServiceDataSource.Load();
        }

        private void RadGridView1_SelectionChanged(object sender, SelectionChangeEventArgs e)
        {
            dataServiceDataSource.FilterDescriptors.Remove(DetailsFilter);
            RadGridView Grid = (RadGridView)sender;

            RadControlsSilverlightClient.TDocs.SignersView a = (RadControlsSilverlightClient.TDocs.SignersView)Grid.SelectedItem;
            if (a != null)
            {
                DetailsFilter = new FilterDescriptor("signerID_pk", FilterOperator.IsEqualTo, a.signerID_pk);
                dataServiceDataSource.FilterDescriptors.Add(DetailsFilter);
                signersDataForm.CurrentItem = dataServiceDataSource.DataView.CurrentItem;
            }
        }

        private void RadGridView1_Loaded(object sender, RoutedEventArgs e)
        {
            dataServiceDataSource.Load();
        }

        #region Setting_Vaues

        private void firstNameNew_TextChanged(object sender, TextChangedEventArgs e)
        {
            firstName = (sender as TextBox).Text;
        }

        private void lastNameNew_TextChanged(object sender, TextChangedEventArgs e)
        {
            lastName = (sender as TextBox).Text;
        }

        private void middleNameNew_TextChanged(object sender, TextChangedEventArgs e)
        {
            middleName = (sender as TextBox).Text;
        }

        private void titleNew_TextChanged(object sender, TextChangedEventArgs e)
        {
            title = (sender as TextBox).Text;
        }

        private void CitizenshipNew_SelectionChanged(object sender, Telerik.Windows.Controls.SelectionChangedEventArgs e)
        {
            citizenshipID = (int)(sender as RadComboBox).SelectedValue;
        }

        private void activeNew_Checked(object sender, RoutedEventArgs e)
        {
            activeID = true;
        }

        private void activeNew_Unchecked(object sender, RoutedEventArgs e)
        {
            activeID = false;
        }

        #endregion

        private void savedChanges(IAsyncResult result)
        {
            TDocs.TreasuryDocsEntities4 entities = (TDocs.TreasuryDocsEntities4)result.AsyncState;

            System.Diagnostics.Debug.WriteLine("saved changes");

            Dispatcher.BeginInvoke(() =>
            {
                SignersViewdataServiceDataSource.Load();
            });
        }

        private void signersDataForm_EditEnding(object sender, Telerik.Windows.Controls.Data.DataForm.EditEndingEventArgs e)
        {
            if (e.EditAction == Telerik.Windows.Controls.Data.DataForm.EditAction.Cancel || 
                e.EditAction == Telerik.Windows.Controls.Data.DataForm.EditAction.Commit)
            {
                if ((sender as RadDataForm).CurrentItem != null)
                {
                    if (((sender as RadDataForm).CurrentItem as TDocs.Signers).signerID_pk == null || ((sender as RadDataForm).CurrentItem as TDocs.Signers).firstname == null)
                        e.Cancel = true;
                }
            }
            if (!e.Cancel)
            {
                if ((sender as RadDataForm).CurrentItem != null)
                {
                    if (((sender as RadDataForm).CurrentItem as TDocs.Signers).createddate == null)
                    {
                        ((sender as RadDataForm).CurrentItem as TDocs.Signers).createddate = DateTime.Now;
                        ((sender as RadDataForm).CurrentItem as TDocs.Signers).createdby = @current_username;
                    }
                    ((sender as RadDataForm).CurrentItem as TDocs.Signers).modifieddate = DateTime.Now;
                    ((sender as RadDataForm).CurrentItem as TDocs.Signers).modifiedby = @current_username;
                    if (activeID == false)
                        ((sender as RadDataForm).CurrentItem as TDocs.Signers).deactivation_date = DateTime.Now;

                    TDocs.TreasuryDocsEntities4 entities = new MyTreasuryDocsEntities();
                    entities.AttachTo("Signers", (sender as RadDataForm).CurrentItem);
                    entities.UpdateObject((sender as RadDataForm).CurrentItem);
                    entities.BeginSaveChanges(savedChanges, entities);
                }
                else
                {
                    TDocs.Signers view = new TDocs.Signers();
                    try
                    {
                        view.firstname = firstName;
                        view.lastname = lastName;
                        view.middlename = middleName;
                        view.title = title;
                        view.citizenshipID_fk = citizenshipID;
                        view.active = activeID;
                        view.createdby = @current_username;
                        view.createddate = DateTime.Now;
                        view.modifieddate = DateTime.Now;
                        view.modifiedby = @current_username;
                        (sender as RadDataForm).CurrentItem = view;

                        TDocs.TreasuryDocsEntities4 entities = new MyTreasuryDocsEntities();
                        entities.AddToSigners(view);
                        entities.BeginSaveChanges(savedChanges, entities);
                    }
                    catch (Exception ex)
                    {
                        RadWindow.Alert("Error " + ex.Message);
                    }
                }
            }
        }

        private void signersDataForm_Loaded(object sender, RoutedEventArgs e)
        {
            if (!editUsers.Contains(current_username.Replace("IHESS\\", string.Empty)))
                signersDataForm.CommandButtonsVisibility = Telerik.Windows.Controls.Data.DataForm.DataFormCommandButtonsVisibility.None;
        }
    }
}
