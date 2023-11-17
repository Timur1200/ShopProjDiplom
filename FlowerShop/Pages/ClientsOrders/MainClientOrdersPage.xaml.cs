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

namespace FlowerShop.Pages.ClientsOrders
{
    /// <summary>
    /// Логика взаимодействия для MainClientOrdersPage.xaml
    /// </summary>
    public partial class MainClientOrdersPage : Page
    {
        public MainClientOrdersPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DGrid.ItemsSource = FlowerShopEntities.GetContext().ClientOrder.Where(q=>q.ClientId== Nav.Client.Id).ToList();
        }

        private void AddClick(object sender, RoutedEventArgs e)
        {
            ClientOrder clientOrder = new ClientOrder
            {
                Date = DateTime.Now,
                ClientId = Nav.Client.Id,
                Sum= 0,
                Status = 0,

            };
            FlowerShopEntities.GetContext().ClientOrder.Add(clientOrder);
            FlowerShopEntities.GetContext().SaveChanges();
            Page_Loaded(null, null);

        }

        private void DelClick(object sender, RoutedEventArgs e)
        {

            if (!MyService.CheckDataGrid(DGrid))
            {
                MessageBox.Show("Нужно выбрать запись!");
                return;
            }
            ClientOrder clientOrder = DGrid.SelectedItem as ClientOrder;
            if (clientOrder.Status == 2)
            {
                MessageBox.Show("Данную запись удалить нельзя!");
                return;
            }
            FlowerShopEntities.GetContext().ClientOrder.Remove(clientOrder);
            FlowerShopEntities.GetContext().SaveChanges();
            Page_Loaded(null, null);
        }

        private void NextClick(object sender, RoutedEventArgs e)
        {
             if (MyService.CheckDataGrid(DGrid))
            {
                ClientOrder clientOrder = DGrid.SelectedItem as ClientOrder;
                if (clientOrder.Status != 0)
                {
                    MessageBox.Show("Заказ уже сформирован!");
                    return;
                }
                Nav.Go(new AddClientOrderPage(clientOrder));
            }
        }
    }
}
