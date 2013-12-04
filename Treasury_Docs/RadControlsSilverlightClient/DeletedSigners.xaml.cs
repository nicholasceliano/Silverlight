using System;
using System.Windows.Controls;

namespace RadControlsSilverlightClient
{
    public partial class DeletedSigners : UserControl
    {
        public DeletedSigners()
        {
            try
            {
                InitializeComponent();
                DeletedSignersDataContext.Load();
                DeletedSignersGridView.ItemsSource = DeletedSignersDataContext.DataView;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
