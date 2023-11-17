using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop.Model
{
    public partial class ListClientOrder
    {
        public string FullName
        {
            get
            {
                return $"{Flower.Name} x {Count}шт.";
            }
        }
    }
}
