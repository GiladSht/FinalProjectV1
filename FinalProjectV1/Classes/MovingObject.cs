using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace FinalProjectV1.Classes
{
    class MovingObject : BaseClass //המחלקה יורשת ממחלקת הבסיס BaseClass
    {
        protected DispatcherTimer dispatcherTimer;//טיימר בשביל תנועת הגוף
        protected double speedX, speedY;//מהירות בציר הX ובציר הY
        /// <summary>
        /// פעולה הבונה עצם זז
        /// </summary>
        /// <param name="placeX">מיקום בציר הX</param>
        /// <param name="placeY">מיקום בציר הY</param>
        /// <param name="arena">זירת המשחק</param>
        /// <param name="width">רוחב העצם</param>
        /// <param name="height">גובה העצם</param>
        public MovingObject(double placeX,double placeY,Canvas arena,double width,double height) : base(placeX, placeY, arena, width, height)
        {
            this.speedY = 0;
            this.speedX = 0;
            this.dispatcherTimer = new DispatcherTimer();
            this.dispatcherTimer.Interval = TimeSpan.FromMilliseconds(1);
            this.dispatcherTimer.Start();
            
        }

        
        
    }
}
