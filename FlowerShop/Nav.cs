using FlowerShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace FlowerShop
{
    internal class Nav
    {
        public static Frame MainFrame { get; set; }
        public static void Back()
        {
            if (MainFrame.CanGoBack) { MainFrame.NavigationService.GoBack(); }
        }
        public static void Go(Page p)
        {
            MainFrame.Navigate(p);
        }
        public static Client Client { get; set; }
    }
}
