using System;
using System.Windows.Controls;
using System.Collections.Generic;

namespace RadControlsSilverlightClient
{
    public partial class RulesView : UserControl
    {
        public RulesView(List<string> signers, List<string> entitlements, string entitlementGroup)
        {
            InitializeComponent();
            int i = 0;
            signatoriesLeft.Text = "";
            signatoriesRight.Text = "";
            rulesTextBlock.Text = "";
            group.Text = "";
            if (!entitlementGroup.ToLower().StartsWith("group"))
                group.Text = "Group ";
            group.Text += entitlementGroup.TrimEnd() + ":";
            foreach (string signer in signers)
            {
                if (i % 2 == 0)
                    signatoriesLeft.Text += signer + "\n";
                else
                    signatoriesRight.Text += signer + "\n";
                i++;
            }
            
            foreach (string entitlement in entitlements)
            {
                rulesTextBlock.Text += String.Format("Rule: \n{0}\n", entitlement);
            }
        }
    }
}
