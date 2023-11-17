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

namespace FlowerShop.Pages.Flowers
{
    /// <summary>
    /// Логика взаимодействия для ListFlowerPage.xaml
    /// </summary>
    public partial class ListFlowerPage : Page
    {
        public ListFlowerPage()
        {
            InitializeComponent();
            _b = false;
        }
        public ListFlowerPage(ClientOrder clientOrder)
        {
            InitializeComponent();
            _clientOrder = clientOrder;
            _b = true;
        }
        private bool _b;
        public ClientOrder _clientOrder;
        private List<Flower> _listFlower;
        public Flower _flower;
        private void LViewMat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_b)
            {
                if (LViewMat.SelectedItem == null)
                {
                    return;
                }
                _flower = LViewMat.SelectedItem as Flower;
                string result = Microsoft.VisualBasic.Interaction.InputBox("Введите количество:", "Выбор");
                int count = 0;
                if (int.TryParse(result,out count))
                {
                    double? c = (double)count;
                    int cc = (int)c;
                    double? sum = c * _flower.Markup;
                    if (cc > _flower.Count)
                    {
                        MessageBox.Show("Не хватает цветов");
                        return;
                    }
                    ListClientOrder listClientOrder = new ListClientOrder {
                    ClientOrderId = _clientOrder.Id,
                    FlowerId = _flower.Id,
                    Count = count,
                    Sum = sum,
                    };

                    FlowerShopEntities.GetContext().ListClientOrder.Add(listClientOrder);
                    FlowerShopEntities.GetContext().SaveChanges();
                    
                }
                else
                {
                    MessageBox.Show("Введено некорректное число");
                }
                Nav.Back();
            }
           
        }

        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TBoxSearch.Text.Length == 0)
            {
                LViewMat.ItemsSource = _listFlower;
                return;
            }
            LViewMat.ItemsSource = _listFlower.Where(q => q.Name.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();
        }

        private void PageLoaded(object sender, RoutedEventArgs e)
        {
            _listFlower = FlowerShopEntities.GetContext().Flower.ToList();
            LViewMat.ItemsSource = _listFlower;
        }
    }
}
