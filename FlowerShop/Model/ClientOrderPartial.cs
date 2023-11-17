using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop.Model
{
    public partial class ClientOrder
    {
        public string StatusText
        {
            get
            {
                return ((StatusEnum)Status).ToString();
            }
        }
    }
}
