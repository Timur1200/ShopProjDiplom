using FlowerShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FlowerShop.Pages.Clients
{
    /// <summary>
    /// Логика взаимодействия для MainClientPage.xaml
    /// </summary>
    public partial class MainClientPage : Page
    {
        public MainClientPage()
        {
            InitializeComponent();
        }
        List<Client> _clientList;
        private void PageLoaded(object sender, RoutedEventArgs e)
        {
            _clientList = FlowerShopEntities.GetContext().Client.ToList();
            DGrid.ItemsSource = _clientList;
        }
        private void AddClick(object sender, RoutedEventArgs e)
        {
            Nav.Go(new AddEditClientPage(null));
        }

        private void EditClick(object sender, RoutedEventArgs e)
        {
            if (MyService.CheckDataGrid(DGrid))
            {
                Nav.Go(new AddEditClientPage(DGrid.SelectedItem as Client));
            }
        }

        private void DelClick(object sender, RoutedEventArgs e)
        {
            if (MyService.CheckDataGrid(DGrid))
            {
                if (MessageBox.Show("Вы действительно хотите удалить эту запись?","Внимание!",MessageBoxButton.YesNo,MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    Client client = DGrid.SelectedItem as Client;
                    FlowerShopEntities.GetContext().Client.Remove(client);
                    FlowerShopEntities.GetContext().SaveChanges();
                    PageLoaded(null, null);
                }
            }
        }
    }
}
