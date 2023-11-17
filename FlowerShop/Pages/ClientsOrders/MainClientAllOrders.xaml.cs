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
    /// Логика взаимодействия для MainClientAllOrders.xaml
    /// </summary>
    public partial class MainClientAllOrders : Page
    {
        public MainClientAllOrders()
        {
            InitializeComponent();
            FilterComboBox.Items.Add("Все заказы");
            FilterComboBox.Items.Add("В ожидании");
            FilterComboBox.Items.Add("Завершенные");
        }
        private List<ClientOrder> _clientOrder;
      

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            _clientOrder = FlowerShopEntities.GetContext().ClientOrder.Where(q=>q.Status != 0).ToList();
            DGrid.ItemsSource = _clientOrder;
        }

        private void FilterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = FilterComboBox.SelectedIndex;
            if (index == 0)
            {
                Page_Loaded(null, null);
            }
            else if (index == 1)
            {
                DGrid.ItemsSource = _clientOrder.Where(q=>q.Status == 1).ToList();
            }
            else if (index == 2)
            {
                DGrid.ItemsSource = _clientOrder.Where(q => q.Status == 2).ToList();
            }
        }

        private void DelClick(object sender, RoutedEventArgs e)
        {
            if (MyService.CheckDataGrid(DGrid))
            {
                if (MessageBox.Show("Вы действительно хотите удалить данную запись?", "Внимание!", MessageBoxButton.YesNo,MessageBoxImage.Question) == MessageBoxResult.Yes) 
                { 
                ClientOrder clientOrder = DGrid.SelectedItem as ClientOrder;
                FlowerShopEntities.GetContext().ClientOrder.Remove(clientOrder);
                    FlowerShopEntities.GetContext().SaveChanges();
                    Page_Loaded(null, null);
                }
            }
        }

        private void OKClick(object sender, RoutedEventArgs e)
        {
            ClientOrder clientOrder = DGrid.SelectedItem as ClientOrder;
            clientOrder.Status = 2;
            var listClientOrders = FlowerShopEntities.GetContext().ListClientOrder.Where(q=>q.ClientOrderId == clientOrder.Id).ToList();
            FlowerShopEntities.GetContext().SaveChanges();
            foreach (var item in listClientOrders)
            {
                var flower = item.Flower;
                flower.Count = flower.Count - item.Count;
                FlowerShopEntities.GetContext().SaveChanges();
            }
            Page_Loaded(null, null);
        }

        private void WordClick(object sender, RoutedEventArgs e)
        {
            if (MyService.CheckDataGrid(DGrid)) {
                
                ClientOrder clientOrder = DGrid.SelectedItem as ClientOrder;
                if (clientOrder.Status != 2)
                {
                    MessageBox.Show("Вывести на печать можно только завершенные заказы");
                    return;
                }
                StringBuilder sb = new StringBuilder();
                var items = FlowerShopEntities.GetContext().ListClientOrder.Where(q => q.ClientOrderId == clientOrder.Id).ToList();
                foreach(var item in items)
                {
                    sb.AppendLine($"{item.FullName} ={item.Sum}");
                }
                WordService wordService = new WordService("Word/chek.docx");
                wordService.ReplaceWordStub("(дата)", $"{clientOrder.Date.Value.ToString("d")}");
                wordService.ReplaceWordStub("(итог)", $"{clientOrder.Sum}");
                wordService.ReplaceWordStub("(итог)", $"{clientOrder.Sum}");
                wordService.ReplaceWordStub("(товары)", $"{sb}");
                wordService.ToWord();
            }

        }

        private void SearchDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SearchDatePicker.SelectedDate == null)
            {
                return;
            }
            DateTime dateTime = SearchDatePicker.SelectedDate.Value;
            var list = _clientOrder.Where(q => q.Date.Value.Year == dateTime.Year && q.Date.Value.Month == dateTime.Month && q.Date.Value.Day == dateTime.Date.Day).ToList();
            ResultTextBlock.Text = $"Сумма за {dateTime.ToString("d")} составляет {list.Sum(q=>q.Sum)} руб";
            DGrid.ItemsSource = list;
        }
    }
}
