using System;
using System.Windows.Controls;
using Telerik.Windows.Controls;
using Telerik.Windows.Data;

namespace RadControlsSilverlightClient
{
    public partial class AccountDataSheet : UserControl
    {
        public AccountDataSheet()
        {
            try
            {
                InitializeComponent();
                AccountsView.IsSynchronizedWithCurrentItem = false;
                AccountsView.SelectionChanged +=new EventHandler<SelectionChangeEventArgs>(AccountsView_SelectionChanged);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void AccountsView_SelectionChanged(object sender, SelectionChangeEventArgs e)
        {
            RadWindow window = new RadWindow();
            TDocs.BADSInfo selectedItem = AccountsView.SelectedItem as TDocs.BADSInfo;
            FilterDescriptor fd = new FilterDescriptor("accountNumber", FilterOperator.IsEqualTo, selectedItem.accountNumber);
            entitlementsDataSource.FilterDescriptors.Add(fd);
            
            window.Content = new BADSSheet(selectedItem);
            window.Header = "Account Data Sheet";
            window.Height = 620;
            window.WindowStartupLocation = Telerik.Windows.Controls.WindowStartupLocation.CenterScreen;
            window.ShowDialog();
            entitlementsDataSource.FilterDescriptors.Remove(fd);
        }
    }
}
