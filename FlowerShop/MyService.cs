using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace FlowerShop
{
    public class MyService
    {
        public static bool CheckDataGrid(DataGrid dataGrid)
        {
            if (dataGrid.SelectedItem == null)
            {
                MessageBox.Show("Нужно выбрать запись!");
                return false;
            }
            return true;
        }
    }
}
