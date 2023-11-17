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
using System.Windows.Shapes;

namespace FlowerShop
{
    /// <summary>
    /// Логика взаимодействия для RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
            _client = new Client();
            DataContext = _client;
        }
        Client _client;
        private void LoginClick(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }

        private void RegisterClick(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrEmpty(_client.Phone) || string.IsNullOrEmpty(_client.Fio) || string.IsNullOrEmpty(PassBox1.Password))
            {
                errors.AppendLine("Все поля необходимы для заполнения");
            }
            if (PassBox1.Password != PassBox2.Password)
            {
                errors.AppendLine("Пароли не совпадают");
            }
            int i = FlowerShopEntities.GetContext().Client.Where(q => q.Phone == _client.Phone).Count();
            if (i != 0 || _client.Phone == "+7(111) 111-1111")
            {
                errors.AppendLine("Номер телефона занят");
            }
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString(),"Внимание!");
                return;
            }
            _client.Password = PassBox1.Password;
            FlowerShopEntities.GetContext().Client.Add(_client);
            FlowerShopEntities.GetContext().SaveChanges();
            MessageBox.Show("Регистрация прошла успешно!");
            LoginClick(null, null);
            this.Close();
        }
    }
}
