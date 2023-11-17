using FlowerShop.Model;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для AddEditFlowerPage.xaml
    /// </summary>
    public partial class AddEditFlowerPage : Page
    {
        public AddEditFlowerPage(Flower flower)
        {
            InitializeComponent();
            _listProvider = FlowerShopEntities.GetContext().Provider.ToList();
            ProviderComboBox.ItemsSource = _listProvider;
            if (flower == null)
            {
                _flower = new Flower();
            }
            else
            {
                _flower = flower;
            }
            DataContext = _flower;
            LoadImg();
        }
        private Flower _flower;
        private List<Provider> _listProvider;
        private string FileName { get; set; }
        private void ImgClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                FileName = openFileDialog.FileName;
                MessageBox.Show("Изображение получено!");
                byte[] bData = File.ReadAllBytes(FileName);
                _flower.Img = bData;
                LoadImg();
            }
        }

        private void SaveClick(object sender, RoutedEventArgs e)
        {
            if (FileName != null)
            {
                byte[] bData = File.ReadAllBytes(FileName);
                _flower.Img = bData;
            }

            if (_flower.Id == 0) FlowerShopEntities.GetContext().Flower.Add(_flower);
            FlowerShopEntities.GetContext().SaveChanges();
            MessageBox.Show("Информация сохранена!");
            Nav.Back();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int p;
            if (int.TryParse(TextBoxPrice.Text,out p))
            {
                double pp = p;
                pp = pp * 1.1;
                TextBoxMarkup.Text = $"{pp}";
                _flower.Markup = (double)pp;
            }
            
        }

        private void PreviewTextInputHandler(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !int.TryParse(e.Text, out _);
        }

        private void LoadImg()
        {
            if (_flower.Img != null)
            {
                Img.Source = _flower.Img1;
            }
        }
    }
}
