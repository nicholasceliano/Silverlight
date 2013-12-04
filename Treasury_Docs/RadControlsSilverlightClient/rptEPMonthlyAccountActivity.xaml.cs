using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Controls;
using Telerik.Windows.Data;

namespace RadControlsSilverlightClient
{
    public partial class rptEPMonthlyAccountActivity : UserControl
    {
        public string current_username = string.Empty;

        public rptEPMonthlyAccountActivity()
        {
            InitializeComponent();
            exportButton.Click += new RoutedEventHandler(exportButton_Click);
            LoadCalendar();
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
                         Format = ExportFormat.ExcelML,
                         ShowColumnHeaders = true,
                         ShowColumnFooters = true,
                         ShowGroupFooters = false,
                     });
                }
            }
        }

        private void LoadCalendar()
        {
            this.calReportDate.Culture = new System.Globalization.CultureInfo("en-US");
            this.calReportDate.Culture.DateTimeFormat.ShortDatePattern = "MMM yyyy";
            calReportDate.SelectedDate = DateTime.Now.AddMonths(-1);
            calReportDate.DisplayDateEnd = DateTime.Now;
        }

        private void calReportDate_SelectionChanged(object sender, Telerik.Windows.Controls.SelectionChangedEventArgs e)
        {
            RadGridView1.FilterDescriptors.Clear();

            DateTime firstOfMonth = new DateTime(calReportDate.SelectedDate.Value.Year, calReportDate.SelectedDate.Value.Month, 1);
            DateTime lastOfMonth = firstOfMonth.AddMonths(1).AddDays(-1);

            CompositeFilterDescriptor openDesc = new CompositeFilterDescriptor();
            openDesc.LogicalOperator = FilterCompositionLogicalOperator.And;
            openDesc.FilterDescriptors.Add(new FilterDescriptor("OpenedDate", FilterOperator.IsGreaterThan, firstOfMonth));
            openDesc.FilterDescriptors.Add(new FilterDescriptor("OpenedDate", FilterOperator.IsLessThan, lastOfMonth));

            CompositeFilterDescriptor closedDesc = new CompositeFilterDescriptor();
            closedDesc.LogicalOperator = FilterCompositionLogicalOperator.And;
            closedDesc.FilterDescriptors.Add(new FilterDescriptor("ClosedDate", FilterOperator.IsGreaterThan, firstOfMonth));
            closedDesc.FilterDescriptors.Add(new FilterDescriptor("ClosedDate", FilterOperator.IsLessThan, lastOfMonth));

            CompositeFilterDescriptor bothDesc = new CompositeFilterDescriptor();
            bothDesc.LogicalOperator = FilterCompositionLogicalOperator.Or;
            bothDesc.FilterDescriptors.Add(openDesc);
            bothDesc.FilterDescriptors.Add(closedDesc);

            RadGridView1.FilterDescriptors.Add(bothDesc);
            RadGridView1.Rebind();
        }
    }
}
