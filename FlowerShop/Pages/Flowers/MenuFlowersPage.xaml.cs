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
    /// Логика взаимодействия для MenuFlowersPage.xaml
    /// </summary>
    public partial class MenuFlowersPage : Page
    {
        public MenuFlowersPage()
        {
            InitializeComponent();
            TabItemFrame1.Navigate(new MainFlowerPage());
            TabItemFrame2.Navigate(new ListFlowerPage());
            //TabItemFrame3.Navigate(new StoryStoragePage());
        }
    }
}
