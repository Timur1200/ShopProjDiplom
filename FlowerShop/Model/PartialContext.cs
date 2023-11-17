using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop.Model
{
    public partial class FlowerShopEntities 
    {
        private static FlowerShopEntities _context;
        public static FlowerShopEntities GetContext()
        {
            if (_context == null ) _context = new FlowerShopEntities();
            return _context;
        }
    }
}
