using System;
using System.Windows.Controls;

namespace RadControlsSilverlightClient
{
    public partial class AuditAccountsSignersEntitlements : UserControl
    {
        string current_username = string.Empty;

        public AuditAccountsSignersEntitlements()
        {
            InitializeComponent();
        }
        public AuditAccountsSignersEntitlements(string username)
        {
            InitializeComponent();
            current_username = username;
        }
    }
}
