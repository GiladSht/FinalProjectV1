using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace FinalProjectV1.Classes
{
    class ConstBasket :BaseClass //המחלקה יורת ממחלקת הבסיס BaseClass 
    {
        Random rnd = new Random();
        public Rect FirstRim { get; set; }//הצד השמאלי של הטבעת
        public Rect SecondRim { get; set; }//הצד הימני של הטבעת
        /// <summary>
        /// הפעולה בונה סל קבוע
        /// </summary>
        /// <param name="placeX">מיקום בציר הX</param>
        /// <param name="placeY">מיקום בציר הY</param>
        /// <param name="arena">זירת המשחק</param>
        /// <param name="width">רוחב העצם</param>
        /// <param name="height">גובה העצמ</param>
        public ConstBasket(double placeX,double placeY,Canvas arena,double width,double height) : base(placeX, placeY, arena, width, height)
        {
            this.image.Source = new BitmapImage(new Uri("ms-appx:///Assets/Hoop.png"));
            
        }
       
    }
}
