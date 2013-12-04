using System;

using Telerik.Windows.Controls;
using Telerik.Windows.Media.Imaging;
using Telerik.Windows.Documents.Model;
using Telerik.Windows.Documents.FormatProviders.Pdf;
using Telerik.Windows.Documents.FormatProviders.Html;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Windows;
using System.Data.Services.Client;
using System.Windows.Printing;


namespace RadControlsSilverlightClient
{
    public partial class BADSSheet : UserControl
    {
        List<TDocs.Contacts> _contacts = new List<TDocs.Contacts>();
        Dictionary<int, List<string>> allEntitlements = new Dictionary<int, List<string>>();
        Dictionary<int, List<string>> allSigners = new Dictionary<int, List<string>>();
        Dictionary<int, String> entitlementGroups = new Dictionary<int, string>();
        TDocs.AccountsView accountInfo;

        int numberOfPagesPrinted = 0;

        double printLength = 0;
        double printWidth = 0;
        private const double PRINT_MARGIN = 48;
        List<UIElement> extraPages = new List<UIElement>();
        List<RulesView> allRules = new List<RulesView>();
        BADSSheet printSheet;
        public BADSSheet()
        {
            InitializeComponent();
        }

        public BADSSheet(TDocs.BADSInfo badsInfo)
        {
            InitializeComponent();

            getRulesData(badsInfo.accountNumber);
            bankLabel.Content = badsInfo.name;
            entityLabel.Content = badsInfo.entityName;
            accountTypeLabel.Content = badsInfo.description;
            streetLabel.Content = badsInfo.street1;
            string cityLabelText = "";
            if (badsInfo.city != null && badsInfo.city != "")
                cityLabelText += badsInfo.city;
            if (badsInfo.state != null && badsInfo.state != "")
                cityLabelText += ", " + badsInfo.state;
            if (badsInfo.zip != null && badsInfo.state != "")
                cityLabelText += " " + badsInfo.zip.ToString();
            if (badsInfo.street2 == null || badsInfo.street2 == "")
                secondStreetLabel.Content = cityLabelText;
            else
            {
                secondStreetLabel.Content = badsInfo.street2;
                cityLabel.Content = cityLabelText;
            }
            phoneLabel.Content = "Phone Number: " + badsInfo.phone;
            faxLabel.Content = "Fax Number: " + badsInfo.fax;
            emailLabel.Content = "Email Address: " + badsInfo.email;

            contactNameLabel.Content = badsInfo.firstname + " " + badsInfo.lastname;
            currencyLabel.Content = badsInfo.currencyCode;
            accountNumberLabel.Content = badsInfo.accountNumber;
            accountTitleLabel.Content = badsInfo.accountName;
            accountOpenedLabel.Content = badsInfo.opened_date;
            accountClosedLabel.Content = badsInfo.closed_date;
            changeDateLabel.Content = badsInfo.modifieddate;
            taxIdLabel.Content = badsInfo.taxID;
            accountStatementLabel.Content = badsInfo.acstatement;
            zbaLabel.Content = badsInfo.zba;
            notesLabel.Text = badsInfo.notes;
        }

        public BADSSheet(TDocs.AccountsView acctInfo)
        {
            InitializeComponent();
            accountInfo = acctInfo;
            getContactData(acctInfo.bankID_pk);
            getRulesData(acctInfo.accountNumber);
            populateAccountInfo();
        }

        public BADSSheet(TDocs.AccountsView info, List<TDocs.Contacts> contacts, Dictionary<int, List<string>> entitlements,
            Dictionary<int, String> groups, Dictionary<int, List<string>> signers, Size printableArea, Thickness margins)
        {
            InitializeComponent();
            accountInfo = info;
            _contacts = contacts;
            allEntitlements = entitlements;
            allSigners = signers;
            entitlementGroups = groups;
            populateAccountInfo();
            populateContactView();
            populateRulesView();
            printLength = printableArea.Height;
            printWidth = printableArea.Width;
            PrintButton.Visibility = System.Windows.Visibility.Collapsed;
            Scroller.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
            setupPrintingPages();
        }

        private void populateAccountInfo()
        {
            dateLabel.Content = DateTime.Now.ToString();
            bankLabel.Content = accountInfo.bank;
            entityLabel.Content = accountInfo.entity;
            accountTypeLabel.Content = accountInfo.accountType;
            currencyLabel.Content = accountInfo.currencyCode;
            accountNumberLabel.Content = accountInfo.accountNumber;
            accountTitleLabel.Content = accountInfo.name;
            accountOpenedLabel.Content = accountInfo.opened_date;
            accountClosedLabel.Content = accountInfo.closed_date;
            changeDateLabel.Content = accountInfo.modifieddate;
            taxIdLabel.Content = accountInfo.taxID;
            accountStatementLabel.Content = accountInfo.acstatement;
            zbaLabel.Content = accountInfo.zba;
            notesLabel.Text = accountInfo.notes;
        }
        private void getContactData(int bankId)
        {
            MyTreasuryDocsEntities entities = new MyTreasuryDocsEntities();
            string storedProcedureCall = entities.BaseUri.ToString() + "/GetPrimaryContactsByBankId?bankId=" + bankId.ToString();
            entities.BeginExecute<TDocs.Contacts>(new Uri(storedProcedureCall), FinishedGettingContactData, entities);
        }

        private void getRulesData(string accountNumber)
        {
            MyTreasuryDocsEntities entities = new MyTreasuryDocsEntities();

            string storedProcedureCall = entities.BaseUri.ToString() + "/GetEntitlementsByAccountNumber?accountNumber='" + accountNumber + "'";
            entities.BeginExecute<TDocs.AccountSignerEntitlementsView>(new Uri(storedProcedureCall), FinishedGettingRulesData, entities);
        }

        public void FinishedGettingContactData(IAsyncResult result)
        {
            MyTreasuryDocsEntities entities = (MyTreasuryDocsEntities)result.AsyncState;
            QueryOperationResponse<TDocs.Contacts> response = (QueryOperationResponse<TDocs.Contacts>)entities.EndExecute<TDocs.Contacts>(result);
            if (response != null)
            {
                Dispatcher.BeginInvoke(() =>
                    {
                        foreach (TDocs.Contacts info in response)
                            _contacts.Add(info);
                        populateContactView();
                    });
            }
        }

        private void populateContactView()
        {
            foreach (TDocs.Contacts info in _contacts)
            {
                streetLabel.Content = info.street1;
                string cityLabelText = "";
                if (info.city != null && info.city != "")
                    cityLabelText += info.city;
                if (info.state != null && info.state != "")
                    cityLabelText += ", " + info.state;
                if (info.zip != null && info.state != "")
                    cityLabelText += " " + info.zip.ToString();
                if (info.street2 == null || info.street2 == "")
                    secondStreetLabel.Content = cityLabelText;
                else
                {
                    secondStreetLabel.Content = info.street2;
                    cityLabel.Content = cityLabelText;
                }
                phoneLabel.Content = "Phone Number: " + info.phone;
                faxLabel.Content = "Fax Number: " + info.fax;
                emailLabel.Content = "Email Address: " + info.email;
                contactNameLabel.Content = info.firstname + " " + info.lastname;
            }
        }

        public void FinishedGettingRulesData(IAsyncResult result)
        {
            MyTreasuryDocsEntities entities = (MyTreasuryDocsEntities)result.AsyncState;
            QueryOperationResponse<TDocs.AccountSignerEntitlementsView> response =
                (QueryOperationResponse<TDocs.AccountSignerEntitlementsView>)entities.EndExecute<TDocs.AccountSignerEntitlementsView>(result);

            List<int> entitlementsAdded = new List<int>();

            foreach (TDocs.AccountSignerEntitlementsView entitlement in response)
            {
                string name = entitlement.lastname + ", " + entitlement.firstname + " " + entitlement.middlename + " (" + entitlement.citizenshipCode + ")";
                if (allEntitlements.ContainsKey(entitlement.entitlementID_fk))
                {
                    List<string> currentsigners = allSigners[entitlement.entitlementID_fk];
                    currentsigners.Add(name);
                    allSigners[entitlement.entitlementID_fk] = currentsigners;
                }
                else
                {
                    List<string> entitlementList = new List<string>();
                    entitlementList.Add(entitlement.description);
                    allEntitlements.Add(entitlement.entitlementID_fk, entitlementList);
                    List<string> signers = new List<string>();
                    signers.Add(name);
                    allSigners.Add(entitlement.entitlementID_fk, signers);
                    entitlementGroups.Add(entitlement.entitlementID_fk, entitlement.entitlementGroup);
                }
            }

            foreach (int entitlement in allEntitlements.Keys)
            {
                List<string> entitlements = allEntitlements[entitlement];
                List<string> signers = allSigners[entitlement];
                int count = entitlements.Count;
            }

            Dispatcher.BeginInvoke(() =>
                    {
                        populateRulesView();
                    });
        }

        private void populateRulesView()
        {
            foreach (int entitlement in allEntitlements.Keys)
            {
                int amountOfRows = PrintLayout.RowDefinitions.Count;
                List<string> entitlements = allEntitlements[entitlement];
                List<string> signers = allSigners[entitlement];
                RulesView layout = new RulesView(signers, entitlements, entitlementGroups[entitlement]);
                PrintLayout.RowDefinitions.Add(new RowDefinition());
                allRules.Add(layout);
                PrintLayout.Children.Add(layout);
                Grid.SetRow(layout, amountOfRows);
                Grid.SetColumn(layout, 0);
            }
        }

        #region Print Support
        //known issues with this algorithm (unlikely to occur but good idea to fix)
        //1. If any single "Rule View" is greater than 1 page it will be clipped
        //2. If the account data section is greater than 1 page it will be clipped
        private void setupPrintingPages()
        {
            LayoutRoot.Children.Remove(PrintLayout);

            foreach (RulesView r in allRules)
                PrintLayout.Children.Remove(r);

            PrintLayout.Children.Remove(HeaderLayout);
            PrintLayout.Children.Remove(datalayout);
            PrintLayout.Children.Remove(SignatureTitleLayout);

            Canvas currentPage = new Canvas();
            currentPage.Height = printLength;
            currentPage.Width = printWidth;
            double currentPageHeight = PRINT_MARGIN;
            double bufferArea = 10;

            //add all the elements to the page
            Size printSize = new Size(printWidth, double.PositiveInfinity);

            HeaderLayout.Measure(printSize);
            datalayout.Measure(printSize);
            SignatureTitleLayout.Measure(printSize);

            Canvas.SetTop(HeaderLayout, PRINT_MARGIN);
            Canvas.SetLeft(HeaderLayout, PRINT_MARGIN + 30);
            currentPageHeight += HeaderLayout.DesiredSize.Height;
            currentPage.Children.Add(HeaderLayout);

            Canvas.SetTop(datalayout, currentPageHeight + bufferArea);
            Canvas.SetLeft(datalayout, PRINT_MARGIN);
            currentPageHeight += datalayout.DesiredSize.Height;
            currentPage.Children.Add(datalayout);

            Canvas.SetTop(SignatureTitleLayout, currentPageHeight + bufferArea);
            Canvas.SetLeft(SignatureTitleLayout, PRINT_MARGIN);
            currentPageHeight += SignatureTitleLayout.DesiredSize.Height;
            currentPage.Children.Add(SignatureTitleLayout);
            //Canvas.SetTop(

            for (int i = 0; i < allRules.Count; i++)
            {
                RulesView currentRule = allRules[i];
                currentRule.Measure(printSize);

                Canvas.SetTop(currentRule, currentPageHeight + bufferArea);
                Canvas.SetLeft(currentRule, PRINT_MARGIN);
                currentPage.Children.Add(currentRule);
                currentPageHeight += currentRule.DesiredSize.Height + bufferArea;

                //setup a new page
                if (currentPageHeight + PRINT_MARGIN > printLength)
                {
                    currentPageHeight = PRINT_MARGIN + bufferArea;
                    currentPage.Children.Remove(currentRule);
                    extraPages.Add(currentPage);
                    currentRule.Measure(printSize);
                    currentPage = new Canvas();
                    currentPage.Height = printLength;
                    currentPage.Width = printWidth;
                    Canvas.SetTop(currentRule, currentPageHeight);
                    Canvas.SetLeft(currentRule, PRINT_MARGIN);
                    currentPage.Children.Add(currentRule);
                    currentPageHeight += currentRule.DesiredSize.Height;
                }
            }
            //add last page to be printed
            extraPages.Add(currentPage);
        }

        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {
            PrintDocument pd = new PrintDocument();
            pd.PrintPage += new EventHandler<PrintPageEventArgs>(pd_PrintPage);
            pd.Print("Bank Account Data Sheet");
        }

        void pd_PrintPage(object sender, PrintPageEventArgs e)
        {
            if (printSheet == null)
                printSheet = new BADSSheet(accountInfo, _contacts, allEntitlements, entitlementGroups, allSigners, e.PrintableArea, e.PageMargins);

            e.PageVisual = printSheet.NextPrintPage();
            bool hasMorePages = printSheet.hasMorePagesToPrint();
            e.HasMorePages = hasMorePages;

            if (!hasMorePages)
                printSheet = null;
        }

        public UIElement NextPrintPage()
        {
            UIElement currentPage = extraPages[numberOfPagesPrinted];
            numberOfPagesPrinted++;
            return currentPage;
        }

        public bool hasMorePagesToPrint()
        {
            bool isFinished = false;
            if (numberOfPagesPrinted < extraPages.Count)
                isFinished = true;

            return isFinished;
        }

        public void resetPrintedPageCounter()
        {
            numberOfPagesPrinted = 0;
        }

        #endregion
    }
}
