using System;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;
using System.Threading;
using System.Data.Services.Client;
using RadControlsSilverlightClient.TDocs;
using Telerik.Windows.Data;
using Telerik.Windows.Controls.DataServices;
using Telerik.Windows.Controls;
using System.Linq;

namespace RadControlsSilverlightClient
{
    public partial class AccountsSigners : UserControl
    {
        private FilterDescriptor DetailsFilter;
        public string current_username = string.Empty;
        public string[] editUsers;
        public int accountID;
        public int signerID;
        public int entitlementID;
        public AccountsSigners()
        {
            InitializeComponent();

            SortDescriptor accountsSD = new SortDescriptor();
            SortDescriptor signersSD = new SortDescriptor();
            SortDescriptor entitlementsSD = new SortDescriptor();

            accountsSD.Member = "accountNumber";
            accountsSD.SortDirection = ListSortDirection.Ascending;
            this.AccountsdataServiceDataSource.SortDescriptors.Add(accountsSD);

            signersSD.Member = "name";
            signersSD.SortDirection = ListSortDirection.Ascending;
            this.SignersdataServiceDataSource.SortDescriptors.Add(signersSD);

            entitlementsSD.Member = "entitlementGroup";
            entitlementsSD.SortDirection = ListSortDirection.Ascending;
            this.EntitlementsdataServiceDataSource.SortDescriptors.Add(entitlementsSD);

            Accounts_Signers_EntitlementsDataSource.SubmittedChanges +=
                new EventHandler<DataServiceSubmittedChangesEventArgs>(Accounts_Signers_EntitlementsDataSource_SubmittedChanges);
            Accounts_Signers_EntitlementsDataSource.LoadedData += new EventHandler<LoadedDataEventArgs>(Accounts_Signers_EntitlementsDataSource_LoadedData);
            RadGridView1.ItemsSource = dataServiceDataSource.DataView;
            RadGridView1.IsSynchronizedWithCurrentItem = true;
        }

        void Accounts_Signers_EntitlementsDataSource_LoadedData(object sender, LoadedDataEventArgs e)
        {
            dataServiceDataSource.Load();
            RadGridView1.Rebind();
        }

        void Perform(Action myMethod, int delayMilliseconds)
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += (s, e) => Thread.Sleep(delayMilliseconds);
            worker.RunWorkerCompleted += (s, e) => myMethod.Invoke();
            worker.RunWorkerAsync();
        }

        void Accounts_Signers_EntitlementsDataSource_SubmittedChanges(object sender, DataServiceSubmittedChangesEventArgs e)
        {
            if (e.HasError)
                   e.MarkErrorAsHandled();
        }

        private void RadGridView1_SelectionChanged(object sender, Telerik.Windows.Controls.SelectionChangeEventArgs e)
        {
            if (!Accounts_Signers_EntitlementsDataSource.IsBusy && !dataServiceDataSource.IsBusy)
            {
                if (DetailsFilter != null)
                    Accounts_Signers_EntitlementsDataSource.FilterDescriptors.Remove(DetailsFilter);

                RadGridView Grid = (RadGridView)sender;
                RadControlsSilverlightClient.TDocs.AccountsSignersView a = (RadControlsSilverlightClient.TDocs.AccountsSignersView)Grid.SelectedItem;
                if (a != null)
                {
                    Accounts_Signers_Entitlements _a = null;
                    foreach (Accounts_Signers_Entitlements entitlement in Accounts_Signers_EntitlementsDataSource.DataView)
                    {
                        if (entitlement.account_signer_entitlementID_pk == a.account_signer_entitlementID_pk)
                            _a = entitlement;
                    }
                    accountSignersDataForm.CurrentItem = _a;
                }
            }
        }

        private void RadComboBox_SelectionChanged(object sender, Telerik.Windows.Controls.SelectionChangedEventArgs e)
        {
            object o = (sender as RadComboBox).SelectedValue;
            if (o != null)
                accountID = (int)o;
        }

        private void Signer_SelectionChanged(object sender, Telerik.Windows.Controls.SelectionChangedEventArgs e)
        {
            object o = (sender as RadComboBox).SelectedValue;
            if (o != null)
                signerID = (int)o;
        }

        private void Entitlement_SelectionChanged(object sender, Telerik.Windows.Controls.SelectionChangedEventArgs e)
        {
            object o = (sender as RadComboBox).SelectedValue;
            if (o != null)
                entitlementID =(int)o;
        }

        private void RadGridView1_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void accountSignersDataForm_EditEnding(object sender, Telerik.Windows.Controls.Data.DataForm.EditEndingEventArgs e)
        {
            if (e.EditAction == Telerik.Windows.Controls.Data.DataForm.EditAction.Cancel
                    || e.EditAction == Telerik.Windows.Controls.Data.DataForm.EditAction.Commit)
            {
                if ((sender as RadDataForm).CurrentItem != null)
                {
                    if (((sender as RadDataForm).CurrentItem as TDocs.Accounts_Signers_Entitlements).accountID_fk == null || ((sender as RadDataForm).CurrentItem as TDocs.Accounts_Signers_Entitlements).signerID_fk == null)
                        e.Cancel = true;
                }
            }
            if (!e.Cancel)
            {
                TDocs.Accounts_Signers_Entitlements currentItem = (TDocs.Accounts_Signers_Entitlements)(sender as RadDataForm).CurrentItem;

                if (currentItem != null && currentItem.account_signer_entitlementID_pk != 0)
                {
                    if (((sender as RadDataForm).CurrentItem as TDocs.Accounts_Signers_Entitlements).createddate == null)
                    {
                        ((sender as RadDataForm).CurrentItem as TDocs.Accounts_Signers_Entitlements).createddate = DateTime.Now;
                        ((sender as RadDataForm).CurrentItem as TDocs.Accounts_Signers_Entitlements).createdby = @current_username;
                    }
                    ((sender as RadDataForm).CurrentItem as TDocs.Accounts_Signers_Entitlements).modifieddate = DateTime.Now;
                    ((sender as RadDataForm).CurrentItem as TDocs.Accounts_Signers_Entitlements).modifiedby = @current_username;
                    TDocs.TreasuryDocsEntities4 entities = new MyTreasuryDocsEntities();
                    entities.AttachTo("Accounts_Signers_Entitlements", (sender as RadDataForm).CurrentItem);
                    entities.UpdateObject((sender as RadDataForm).CurrentItem);
                    entities.BeginSaveChanges(UpdatedEntitlement, entities);
                }
                else
                {
                    //add new
                    TDocs.Accounts_Signers_Entitlements view = (TDocs.Accounts_Signers_Entitlements) Accounts_Signers_EntitlementsDataSource.DataView.AddNew();
                    (view as TDocs.Accounts_Signers_Entitlements).accountID_fk = accountID;
                    (view as TDocs.Accounts_Signers_Entitlements).signerID_fk = signerID;
                    (view as TDocs.Accounts_Signers_Entitlements).entitlementID_fk = entitlementID;
                    (view as TDocs.Accounts_Signers_Entitlements).added = DateTime.Now;
                    (view as TDocs.Accounts_Signers_Entitlements).user_add = @current_username;
                    (view as TDocs.Accounts_Signers_Entitlements).createdby = @current_username;
                    (view as TDocs.Accounts_Signers_Entitlements).createddate = DateTime.Now;
                    (view as TDocs.Accounts_Signers_Entitlements).modifieddate = DateTime.Now;
                    (view as TDocs.Accounts_Signers_Entitlements).modifiedby = @current_username;
                    (sender as RadDataForm).CurrentItem = view;
                    TreasuryDocsEntities4 entities = new MyTreasuryDocsEntities();
                    try
                    {
                        entities.AddToAccounts_Signers_Entitlements(view);
                        entities.BeginSaveChanges(UpdatedEntitlement, entities);
                    }
                    catch { }
                }
            }
        }

        private void UpdatedEntitlement(IAsyncResult result)
        {
            Dispatcher.BeginInvoke(() =>
                {
                    TDocs.TreasuryDocsEntities4 entities = (TDocs.TreasuryDocsEntities4)result.AsyncState;

                    DataServiceResponse response = entities.EndSaveChanges(result);
                    dataServiceDataSource.Load();
                    RadGridView1.Rebind();
                    Accounts_Signers_EntitlementsDataSource.Load();
                });
        }

        private void accountSignersDataForm_DeletingItem(object sender, CancelEventArgs e)
        {
            if ((sender as RadDataForm).CurrentItem != null)
            {
                TDocs.Accounts_Signers_Entitlements currentAccount = (TDocs.Accounts_Signers_Entitlements)(sender as RadDataForm).CurrentItem;
                MyTreasuryDocsEntities context = new MyTreasuryDocsEntities();
                string storedProcCall = context.BaseUri.ToString() + "/RemoveAccountSignerEntitlement?accountSignerEntitlementId=" +
                    currentAccount.account_signer_entitlementID_pk.ToString();
                storedProcCall += "&username='" + @current_username + "'";
                context.BeginExecute<int>(new Uri(storedProcCall), FinishedDeletion, context);
            }
        }

        private void FinishedDeletion(IAsyncResult result)
        {
            dataServiceDataSource.Load();
        }

        private void accountSignersDataForm_Loaded(object sender, RoutedEventArgs e)
        {
            if (!editUsers.Contains(current_username.Replace("IHESS\\", string.Empty)))
                accountSignersDataForm.CommandButtonsVisibility = Telerik.Windows.Controls.Data.DataForm.DataFormCommandButtonsVisibility.None;
        }
    }
}
