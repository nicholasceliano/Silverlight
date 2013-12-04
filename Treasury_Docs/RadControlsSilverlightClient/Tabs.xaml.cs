using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Browser;

namespace RadControlsSilverlightClient
{
    public partial class Tabs : UserControl
    {
        public Tabs(string username, string[] editUsers)
        {
            InitializeComponent();
            BanksControl.current_username = username;
            ContactsControl.current_username = username;
            AccountsControl.current_username = username;
            AcctTypesControl.current_username = username;
            AuditAcctsControl.current_username = username;
            CurrenciesControl.current_username = username;
            SignersControl.current_username = username;
            EntitlementsControl.current_username = username;
            DivisionsControl.current_username = username;
            CitizenshipsControl.current_username = username;
            AccountsSignersControl.current_username = username;
            FBARControl.current_username = username;
            Reports.current_username = username;
            EntitiesControl.current_username = username;

            AccountsControl.editUsers = editUsers;
            AcctTypesControl.editUsers = editUsers;
            CurrenciesControl.editUsers = editUsers;
            SignersControl.editUsers = editUsers;
            EntitlementsControl.editUsers = editUsers;
            EntitiesControl.editUsers = editUsers;
            BanksControl.editUsers = editUsers;
            ContactsControl.editUsers = editUsers;
            DivisionsControl.editUsers = editUsers;
            CitizenshipsControl.editUsers = editUsers;
            AccountsSignersControl.editUsers = editUsers;

            #region GetQueryString

            if (HtmlPage.Document.QueryString.ContainsKey("TabID"))
            {
                string tabID = HtmlPage.Document.QueryString["TabID"];
                if (tabID.Equals("1"))
                    TABAccountsControl.IsSelected = true;
                else if (tabID.Equals("2"))
                    TABAcctTypesControl.IsSelected = true;
                else if (tabID.Equals("3"))
                    TABAuditAcctsControl.IsSelected = true;
                else if (tabID.Equals("4"))
                    TABCurrenciesControl.IsSelected = true;
                else if (tabID.Equals("5"))
                    TABSignersControl.IsSelected = true;
                else if (tabID.Equals("6"))
                    TABEntitlementsControl.IsSelected = true;
                else if (tabID.Equals("7"))
                    TABEntitiesControl.IsSelected = true;
                else if (tabID.Equals("8"))
                    TABBanksControl.IsSelected = true;
                else if (tabID.Equals("9"))
                    TABContactsControl.IsSelected = true;
                    else if (tabID.Equals("10"))
                    TABDivisionsControl.IsSelected = true;
                    else if (tabID.Equals("11"))
                    TABCitizenshipsControl.IsSelected = true;
                else if (tabID.Equals("12"))
                    TABAccountsSignersControl.IsSelected = true;
                else
                    TABAccountsControl.IsSelected = true;
            }

            #endregion
        }

        private void RadTabItem_GotFocus(object sender, RoutedEventArgs e)
        {
        }
    }
}
