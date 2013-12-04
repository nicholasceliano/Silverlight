using System;
using System.IO;
using System.Windows;
using System.Linq;
using System.Windows.Controls;
using Telerik.Windows.Controls;

namespace RadControlsSilverlightClient
{
    public partial class rptActiveSigners : UserControl
    {
        public string current_username = string.Empty;

        public rptActiveSigners()
        {
            InitializeComponent();
            exportButton.Click += new RoutedEventHandler(exportButton_Click);
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
    }
}
