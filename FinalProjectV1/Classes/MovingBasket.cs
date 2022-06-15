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
    class MovingBasket: MovingObject//המחלקה יורשת ממחלקה של עצמים שזזים
    {
        public Rect FirstRim { get; set; }// הצד השאלי לל הטבעת 
        public Rect SecondRim { get; set; }//הצד הימני של הטבעת
        /// <summary>
        /// פעולה הבונה סל זז
        /// </summary>
        /// <param name="placeX">מיקום על ציר הX</param>
        /// <param name="placeY">מיקום על הציר הY</param>
        /// <param name="arena">זירת המשחק</param>
        /// <param name="width">רוחב העצם</param>
        /// <param name="height">גובה העצם</param>
        public MovingBasket(double placeX,double placeY,Canvas arena,double width,double height) : base(placeX, placeY, arena, width, height)
        {
            this.image.Source = new BitmapImage(new Uri("ms-appx:///Assets/Hoop.png"));
            this.speedX = 5;
            this.speedY = 0;
            this.dispatcherTimer.Tick += DispatcherTimer_Tick;
            this.FirstRim = new Rect(this.PlaceX + 20, this.PlaceY + 38, 15, 36);
            this.SecondRim = new Rect(this.PlaceX + 210, this.PlaceY + 38, 15, 36);
        }
        /// <summary>
        /// טיימר כדי להניע את הסל
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DispatcherTimer_Tick(object sender, object e)
        {
            if (this.PlaceX <= 0)
                this.speedX *= -1;
            if (this.PlaceX + this.image.ActualWidth >= this.arena.ActualWidth)
                this.speedX *= -1;
            this.PlaceX += this.speedX;
            Canvas.SetTop(this.image, this.PlaceY);
            Canvas.SetLeft(this.image, this.PlaceX);
            this.FirstRim = new Rect(this.PlaceX + 20, this.PlaceY + 38, 15, 36);
            this.SecondRim = new Rect(this.PlaceX + 210, this.PlaceY + 38, 15, 36);

        }
        

    }
}
