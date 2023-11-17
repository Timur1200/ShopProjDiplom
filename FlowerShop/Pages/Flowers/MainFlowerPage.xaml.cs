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
    /// Логика взаимодействия для MainFlowerPage.xaml
    /// </summary>
    public partial class MainFlowerPage : Page
    {
        public MainFlowerPage()
        {
            InitializeComponent();
        }

        private void AddClick(object sender, RoutedEventArgs e)
        {
            Nav.Go(new AddEditFlowerPage(null));
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DGrid.ItemsSource = FlowerShopEntities.GetContext().Flower.ToList();
        }

        private void EditClick(object sender, RoutedEventArgs e)
        {
            if (MyService.CheckDataGrid(DGrid))
            {
                Nav.Go(new AddEditFlowerPage(DGrid.SelectedItem as Flower));
            }
        }

        private void DelClick(object sender, RoutedEventArgs e)
        {
            if (MyService.CheckDataGrid(DGrid))
            {
                if (MessageBox.Show("Вы действительно хотите удалить данную запись?","Внимание!",MessageBoxButton.YesNo,MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    Flower flower = DGrid.SelectedItem as Flower;
                    FlowerShopEntities.GetContext().Flower.Remove(flower);
                    FlowerShopEntities.GetContext().SaveChanges();
                    Page_Loaded(null, null);
                }
            }
        }
    }
}
