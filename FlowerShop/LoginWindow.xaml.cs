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
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            
        }
        private void LoginClick(object sender, RoutedEventArgs e)
        {
            var users = FlowerShopEntities.GetContext().Client.ToList();
            foreach(var user in users)
            {
                if (user.Phone == LoginTextBox.Text && user.Password == PassBox.Password)
                {
                    MainWindow mainWindow = new MainWindow(false);
                    mainWindow.Show();
                    this.Close();
                    Nav.Client = user;
                    return;
                }
            }
            if (LoginTextBox.Text == "+7(111) 111-1111" && PassBox.Password == "1")
            {
                MainWindow mainWindow = new MainWindow(true);
                mainWindow.Show();
                this.Close();
                return;
            }
            MessageBox.Show("Пользователь не найден!");
        }
        private void RegClick(object sender, RoutedEventArgs e)
        {
            RegisterWindow registerWindow = new RegisterWindow();
            registerWindow.Show();
            this.Hide();
        }
    }
}
