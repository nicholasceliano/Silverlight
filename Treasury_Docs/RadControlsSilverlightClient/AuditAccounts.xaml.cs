using System;
using System.Windows.Controls;

namespace RadControlsSilverlightClient
{
    public partial class AuditAccounts : UserControl
    {
        public string current_username = string.Empty;
        public AuditAccounts()
        {
            InitializeComponent();
        }
        public AuditAccounts(string username)
        {
            InitializeComponent();
            current_username = username;
        }
    }
}
