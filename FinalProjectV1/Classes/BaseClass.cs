using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace FinalProjectV1.Classes
{
    /// <summary>
    /// מחלקת הבסיס ששאר המחלקות מתבססות עליה
    /// </summary>
    class BaseClass
    {
        public double PlaceX { get; set; }//מיקום הגור בציר ביחס לציר אופקי
        public double PlaceY { get; set; }//מיקום הציר ביחס לציר אנכי 
        protected Image image;// תמונה
        protected Canvas arena;// סביבת המשחק
        /// <summary>
        /// הפעולה בונה עצם יסוד
        /// </summary>
        /// <param name="placeX">מיקום הגור בציר ביחס לציר אופקי</param>
        /// <param name="placeY">מיקום הגור בציר ביחס לציר אנכי</param>
        /// <param name="arena">סביבת המשחק</param>
        /// <param name="width">רוחב בגוף</param>
        /// <param name="height">גובה הגוף</param>
        
        public BaseClass(double placeX,double placeY,Canvas arena,
            double width,double height)
        {
            this.PlaceX = placeX;
            this.PlaceY = placeY;
            this.image = new Image();
            this.image.Width = width;
            this.image.Height = height;
            this.arena = arena;
            Canvas.SetLeft(this.image, this.PlaceX);
            Canvas.SetTop(this.image, this.PlaceY);
            this.arena.Children.Add(this.image);
            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns>המלבן מסביב לגוף</returns>
        public Rect GetRectangle()
        {
            return new Rect(this.PlaceX, this.PlaceY, this.image.Width, this.image.Height);
        }
        /// <summary>
        /// הפעולה מחזירה את התמונה של העצם
        /// </summary>
        /// <returns>התמונה של העצם</returns>
        public Image GetImage()
        {
            return this.image;
        }
        
    }
}
