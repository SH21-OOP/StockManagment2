using Models;
using System.Windows;
using ViewModel;

namespace StockManagment
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private StockManagmentViewModel ViewModel;
        public MainWindow()
        {
            InitializeComponent();
            ViewModel = new StockManagmentViewModel();
            this.DataContext = ViewModel;
        }

        // Осуждаю:
        private void ProductGroups_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var control = (System.Windows.Controls.ListView)sender;
            if (control.SelectedItems.Count > 0)
            {
                var item = control.SelectedItems[0];
                ViewModel.SelectedGroup = (ComercialProductGroupDTO)item;
            }
        }

        private void Products_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var control = (System.Windows.Controls.ListView)sender;
            if (control.SelectedItems.Count > 0)
            {
                var item = control.SelectedItems[0];
                ViewModel.SelectedProduct = (ProductDTO)item;
            }
        }

        private void TextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }
    }
}
