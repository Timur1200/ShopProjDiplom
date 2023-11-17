using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace FlowerShop.Model
{
    public partial class Flower
    {
         public BitmapSource Img1
        {
            get
            {
                if (Img != null) try { return (BitmapSource)new ImageSourceConverter().ConvertFrom(Img); }
                    catch { return null; }
                return null;
            }
        }
    }
}
