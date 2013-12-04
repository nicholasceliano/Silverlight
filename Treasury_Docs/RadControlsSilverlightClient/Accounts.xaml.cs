using System;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;
using System.Threading;
using System.IO;
using Telerik.Windows.Data;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;
using Telerik.Windows.Controls.DataServices;
using System.Linq;

namespace RadControlsSilverlightClient
{
    public partial class Accounts : UserControl
    {
        private FilterDescriptor AccountDetailsFilter;
        private FilterDescriptor SignersFilter;
        public string current_username = string.Empty;
        public string[] editUsers;
        public string accountName = string.Empty;
        public string accountNumber = string.Empty;
        public int divisionID;
        public int bankID;
        public int typeID;
        public int entityID;
        public int currencyID;
        public bool activeID;
        public string acStatement = string.Empty;
        public string zba = string.Empty;
        public DateTime defaultdate = new DateTime(0001, 01, 01);
        public DateTime closed_date = new DateTime(0001, 01, 01);
        public DateTime open_date = new DateTime(0001, 01, 01);

        private TDocs.Accounts currentEditedAccount = null;
        public Accounts()
        {
            InitializeComponent();

            SortDescriptor banksSD = new SortDescriptor();
            SortDescriptor typesSD = new SortDescriptor();
            SortDescriptor entitiesSD = new SortDescriptor();
            SortDescriptor currenciesSD = new SortDescriptor();
            SortDescriptor divisionsSD = new SortDescriptor();

            banksSD.Member = "name";
            banksSD.SortDirection = ListSortDirection.Ascending;
            this.BanksdataServiceDataSource.SortDescriptors.Add(banksSD);

            typesSD.Member = "description";
            typesSD.SortDirection = ListSortDirection.Ascending;
            this.TypesdataServiceDataSource.SortDescriptors.Add(typesSD);

            entitiesSD.Member = "name";
            entitiesSD.SortDirection = ListSortDirection.Ascending;
            this.EntitiesdataServiceDataSource.SortDescriptors.Add(entitiesSD);

            currenciesSD.Member = "currencyCode";
            currenciesSD.SortDirection = ListSortDirection.Ascending;
            this.CurrenciesdataServiceDataSource.SortDescriptors.Add(currenciesSD);

            divisionsSD.Member = "divisionCode";
            divisionsSD.SortDirection = ListSortDirection.Ascending;
            this.DivisionsdataServiceDataSource.SortDescriptors.Add(divisionsSD);

            dataServiceDataSource.SubmittedChanges += new EventHandler<DataServiceSubmittedChangesEventArgs>(dataServiceDataSource_SubmittedChanges);
            dataServiceDataSource.LoadedData += new EventHandler<LoadedDataEventArgs>(dataServiceDataSource_LoadedData);
            dataServiceDataSource.LoadingData += new EventHandler<LoadingDataEventArgs>(dataServiceDataSource_LoadingData);
        }

        void dataServiceDataSource_LoadingData(object sender, LoadingDataEventArgs e)
        {
        }

        void dataServiceDataSource_LoadedData(object sender, LoadedDataEventArgs e)
        {
            AccountsdataServiceDataSource.Load();
            Perform(() => updateGrid(),1000);
        }


        void Perform(Action myMethod, int delayMilliseconds)
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += (s, e) => Thread.Sleep(delayMilliseconds);
            worker.RunWorkerCompleted += (s, e) => myMethod.Invoke();
            worker.RunWorkerAsync();
        }

        void updateGrid()
        {
            Dispatcher.BeginInvoke(() =>
                {
                    AccountsdataServiceDataSource.Load();
                });
        }
        void dataServiceDataSource_SubmittedChanges(object sender, DataServiceSubmittedChangesEventArgs e)
        {

            if (e.HasError)
            {
                RadWindow win = new RadWindow();
                win.Content = "error " + e.Error.ToString();
                win.ShowDialog();
            }
            
            AccountsdataServiceDataSource.Load();
        }

        private void myRadDataForm_EditEnding(object sender, Telerik.Windows.Controls.Data.DataForm.EditEndingEventArgs e)
        {
            if (e.EditAction == Telerik.Windows.Controls.Data.DataForm.EditAction.Cancel || e.EditAction == Telerik.Windows.Controls.Data.DataForm.EditAction.Commit)
            {
                if ((sender as RadDataForm).CurrentItem != null)
                {
                    if (((sender as RadDataForm).CurrentItem as TDocs.Accounts).accountID_pk == null || ((sender as RadDataForm).CurrentItem as TDocs.Accounts).name == null)
                        e.Cancel = true;
                }
            }
            if (!e.Cancel)
            {
                if ((sender as RadDataForm).CurrentItem != null)
                {
                    if (((sender as RadDataForm).CurrentItem as TDocs.Accounts).createddate == null)
                    {
                        ((sender as RadDataForm).CurrentItem as TDocs.Accounts).createddate = DateTime.Now;
                        ((sender as RadDataForm).CurrentItem as TDocs.Accounts).createdby = @current_username;
                    }
                    ((sender as RadDataForm).CurrentItem as TDocs.Accounts).modifieddate = DateTime.Now;
                    ((sender as RadDataForm).CurrentItem as TDocs.Accounts).modifiedby = @current_username;
                    TDocs.Accounts view = (TDocs.Accounts)(sender as RadDataForm).CurrentItem;
                    
                    if (view.closed_date != null)
                        view.active = false;
                    else
                    {
                        if (!view.active)
                        {
                            RadWindow.Alert("You must enter a closed date when the account is set to closed");
                            return;
                        }

                    }

                    TDocs.TreasuryDocsEntities4 entities = new MyTreasuryDocsEntities();
                    entities.AttachTo("Accounts", (sender as RadDataForm).CurrentItem);
                    entities.UpdateObject((sender as RadDataForm).CurrentItem);
                    entities.BeginSaveChanges(AccountsUpdated, entities);
                }
                else
                {
                    //NEC - 7/19/2013
                    foreach (TDocs.AccountsView t in AccountsdataServiceDataSource.DataView)
                    {
                        if (accountNumber == t.accountNumber)
                        {
                            RadWindow.Alert("Account Number " + t.accountNumber + " is a duplicate.");
                            return;
                        }
                    }

                    TDocs.Accounts view = (TDocs.Accounts)dataServiceDataSource.DataView.AddNew();

                    view.name = accountName;
                    view.accountNumber = accountNumber;
                    view.divisionsID_fk = divisionID;
                    view.currencyID_fk = currencyID;
                    view.entityID_fk = entityID;
                    view.typeID_fk = typeID;
                    view.bankID_fk = bankID;
                    view.active = activeID;
                    view.acstatement = acStatement;
                    view.zba = zba;
                    if (open_date != defaultdate)
                        view.opened_date = open_date;
                    if (closed_date != defaultdate)
                        view.closed_date = closed_date;
                    view.createdby = @current_username;
                    view.createddate = DateTime.Now;
                    view.modifieddate = DateTime.Now;
                    view.modifiedby = @current_username;
                    (sender as RadDataForm).CurrentItem = view;
                    TDocs.TreasuryDocsEntities4 entities = new MyTreasuryDocsEntities();
                    entities.AddToAccounts(view);
                    entities.BeginSaveChanges(AccountsUpdated, entities);
                }
            }
        }

        private void AccountsUpdated(IAsyncResult result)
        {
            TDocs.TreasuryDocsEntities4 entities = (TDocs.TreasuryDocsEntities4)result.AsyncState;
            entities.EndSaveChanges(result);
            Dispatcher.BeginInvoke(() =>
               {
                   AccountsdataServiceDataSource.Load();
               });
        }

        private void RadGridView1_SelectionChanged(object sender, Telerik.Windows.Controls.SelectionChangeEventArgs e)
        {
            dataServiceDataSource.FilterDescriptors.Remove(AccountDetailsFilter);
            SignersdataServiceDataSource.FilterDescriptors.Remove(SignersFilter);

            RadGridView Grid = (RadGridView)sender;

            RadControlsSilverlightClient.TDocs.AccountsView a = (RadControlsSilverlightClient.TDocs.AccountsView)Grid.SelectedItem;
            if (a != null)
            {
                AccountDetailsFilter = new FilterDescriptor("accountID_pk", FilterOperator.IsEqualTo, a.accountID);
                dataServiceDataSource.FilterDescriptors.Add(AccountDetailsFilter);

                SignersFilter = new FilterDescriptor("accountID_pk", FilterOperator.IsEqualTo, a.accountID);
                SignersdataServiceDataSource.FilterDescriptors.Add(SignersFilter);

                myRadDataForm.CurrentItem = dataServiceDataSource.DataView.CurrentItem;
            }
        }

        private void exportButton_Click(object sender, RoutedEventArgs e)
        {
            string extension = "xls";
            SaveFileDialog dialog = new SaveFileDialog()
            {
                DefaultExt = extension,
                Filter = String.Format("{1} files (*.{0})|*.{0}|All files (*.*)|*.*", extension, "Excel"),
                FilterIndex = 1
            };
            if (dialog.ShowDialog() == true)
            {
                using (Stream stream = dialog.OpenFile())
                {
                    RadGridView1.Export(stream,
                     new GridViewExportOptions()
                     {
                         //Format = ExportFormat.Html,
                         Format = ExportFormat.ExcelML,
                         ShowColumnHeaders = true,
                         ShowColumnFooters = true,
                         ShowGroupFooters = false,
                     });
                }
            }
        }

        #region Setting_Vaues

        private void accountNew_TextChanged(object sender, TextChangedEventArgs e)
        {
            accountName = (sender as TextBox).Text;
        }

        private void accountNumberNew_TextChanged(object sender, TextChangedEventArgs e)
        {
            accountNumber = (sender as TextBox).Text;
        }

        private void DivisionNew_SelectionChanged(object sender, Telerik.Windows.Controls.SelectionChangedEventArgs e)
        {
            divisionID = (int)(sender as RadComboBox).SelectedValue;
        }

        private void BankNew_SelectionChanged(object sender, Telerik.Windows.Controls.SelectionChangedEventArgs e)
        {
            bankID = (int)(sender as RadComboBox).SelectedValue;
        }

        private void TypeNew_SelectionChanged(object sender, Telerik.Windows.Controls.SelectionChangedEventArgs e)
        {
            typeID = (int)(sender as RadComboBox).SelectedValue;
        }

        private void EntityNew_SelectionChanged(object sender, Telerik.Windows.Controls.SelectionChangedEventArgs e)
        {
            entityID = (int)(sender as RadComboBox).SelectedValue;
        }

        private void CurrencyNew_SelectionChanged(object sender, Telerik.Windows.Controls.SelectionChangedEventArgs e)
        {
            currencyID = (int)(sender as RadComboBox).SelectedValue;
        }

        private void ActiveNew_SelectionChanged(object sender, Telerik.Windows.Controls.SelectionChangedEventArgs e)
        {
            activeID = (bool)(sender as RadComboBox).SelectedValue;
        }

        private void acStatementNew_TextChanged(object sender, TextChangedEventArgs e)
        {
            acStatement = (sender as TextBox).Text;
        }

        private void zbaNew_TextChanged(object sender, TextChangedEventArgs e)
        {
            zba = (sender as TextBox).Text;
        }

        private void closedNew_SelectionChanged(object sender, Telerik.Windows.Controls.SelectionChangedEventArgs e)
        {
            closed_date = (DateTime)(sender as RadDatePicker).SelectedValue;
        }

        private void openNew_SelectionChanged(object sender, Telerik.Windows.Controls.SelectionChangedEventArgs e)
        {
            open_date = (DateTime)(sender as RadDatePicker).SelectedValue;
        }

        #endregion

        private void DataSheetButton_Click(object sender, RoutedEventArgs e)
        {
            RadButton button = sender as RadButton;
            GridViewRow row = button.ParentOfType<GridViewRow>();
            TDocs.AccountsView selectedAccount = (TDocs.AccountsView)row.Item;
            BADSSheet infoSheet = new BADSSheet(selectedAccount);

            RadWindow window = new RadWindow();
            window.Content = infoSheet;
            window.Header = "Account Data Sheet";
            window.Height = 620;
            window.WindowStartupLocation = Telerik.Windows.Controls.WindowStartupLocation.CenterScreen;
            window.ShowDialog();
        }

        private void myRadDataForm_ValidatingItem(object sender, CancelEventArgs e)
        {
            TDocs.Accounts currentAccount = (TDocs.Accounts)myRadDataForm.CurrentItem;
            if (currentAccount.active)
            {
                if (currentAccount.closed_date != null)
                    RadWindow.Alert("Account must be in closed state to have a closed date");
            }
            else
            {
                if (currentAccount.closed_date == null)
                    RadWindow.Alert("An account in closed state must have a closed date");
            }
        }

        private void myRadDataForm_BeginningEdit(object sender, CancelEventArgs e)
        {
            dataServiceDataSource.DataView.EditItem(myRadDataForm.CurrentItem);
        }

        private void myRadDataForm_Loaded(object sender, RoutedEventArgs e)
        {
            if(!editUsers.Contains(current_username.Replace("IHESS\\", string.Empty)))
                myRadDataForm.CommandButtonsVisibility = Telerik.Windows.Controls.Data.DataForm.DataFormCommandButtonsVisibility.None;
        }
    }
}
