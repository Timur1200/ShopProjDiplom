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
    /// Логика взаимодействия для AddEditClientPage.xaml
    /// </summary>
    public partial class AddEditClientPage : Page
    {
        public AddEditClientPage(Client client)
        {
            InitializeComponent();
            if (client == null)
            {
                _client = new Client();
            }
            else
            {
                _client = client;
            }
            DataContext = _client;  
        }
        private Client _client;

        private void SaveClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(_client.Fio) || string.IsNullOrEmpty(_client.Phone) || string.IsNullOrEmpty(_client.Password))
            {
                MessageBox.Show("Все поля необходимы для заполнения!");
                return;
            }

            if (_client.Id == 0)
            {
                FlowerShopEntities.GetContext().Client.Add(_client);
            }
            FlowerShopEntities.GetContext().SaveChanges();
            MessageBox.Show("Информация сохранена!");
            Nav.Back();
        }
    }
}
