using FlowerShop.Model;
using FlowerShop.Pages.Flowers;
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
    /// Логика взаимодействия для AddClientOrderPage.xaml
    /// </summary>
    public partial class AddClientOrderPage : Page
    {
        public AddClientOrderPage(ClientOrder clientOrder)
        {
            InitializeComponent();
            _clientOrder= clientOrder;
            DataContext = _clientOrder;
          
        }
        private ClientOrder _clientOrder;
        private List<ListClientOrder> _listFlower;
        private void AddFlowerClick(object sender, RoutedEventArgs e)
        {
            Nav.Go(new ListFlowerPage(_clientOrder));
        }

        private void DelFlowerClick(object sender, RoutedEventArgs e)
        {
            if (FlowerListBox.SelectedItem == null)
            {
                MessageBox.Show("Нужно выбрать поле!");
                return;
            }
            ListClientOrder item = FlowerListBox.SelectedItem as ListClientOrder;
            FlowerShopEntities.GetContext().ListClientOrder.Remove(item);
            FlowerShopEntities.GetContext().SaveChanges();
            PageLoaded(null, null);
        }

        private void SaveClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(_clientOrder.Address)){
                
                MessageBox.Show("Поле адрес не заполнено!");
                return;
            }
            if (_listFlower.Count == 0)
            {
                MessageBox.Show("Для оформления заказа нужно выбрать хотя бы один товар!");
                return;
            }
            _clientOrder.Status = 1;
            FlowerShopEntities.GetContext().SaveChanges();
            MessageBox.Show("Заказ оформлен!");
            Nav.Back();
        }

        private void PageLoaded(object sender, RoutedEventArgs e)
        {
            _listFlower = FlowerShopEntities.GetContext().ListClientOrder.Where(q=>q.ClientOrderId==_clientOrder.Id).ToList();
            FlowerListBox.ItemsSource = _listFlower;
            if (_listFlower.Count != 0)
            {
                double? sum = 0;
                foreach (var item in _listFlower)
                {
                    sum = sum + item.Sum;
                }
                _clientOrder.Sum = sum;
                SumTextBlock.Text = $"Итоговая сумма: " + sum.ToString() + " руб.";
            }
            else
            {
                _clientOrder.Sum = 0;
                SumTextBlock.Text = string.Empty;
            }
        }
    }
}
