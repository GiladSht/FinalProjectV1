using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;
using Windows.UI.Input;
using System.Diagnostics;
using Windows.UI.Xaml.Shapes;
using Windows.UI.Xaml.Media;
using Windows.UI;
using DataBaseProject.Models;

namespace FinalProjectV1.Classes
{
    class Manager//מנהל המשחק
    {
        private Random rnd = new Random();
        private Ball ball;//עצם הישמש ככדור
        private MovingBasket movingBasket;//הסל הזז במשחק
        private int score = 0;//כמות הנקודות 
        public event EventHandler ShowScore;//אירוע המראה ניקוד
        public event EventHandler Restart;//אירוע המאתחל משחק
        private Canvas arena;//זירת המשחק
        private ConstBasket ConstBasket;//עצם של סל קבוע
        private int BallMaxWidthValue;//מיקום מקסימלי של הכדור בציר האופקי 
        private int BasketMaxWidthValue;//מיקום מקסימלי של הסל בציר האופקי 
        private int BallMaxHeightValue;//מיקום מקסימלי של הכדור בציר האנכי
        private int BasketMaxHeightValue;//מיקום מקסימלי של סל בציר האנכי
        private User user;//משתמש המשחק
        /// <summary>
        /// הפעולה יוצרת את מנהל המשחק
        /// </summary>
        /// <param name="arena">זירת המשחק</param>
        /// <param name="user">משתמש המשחק</param>
        public Manager(Canvas arena,User user)
        {
            this.user = user;

            this.arena = arena;
            this.score = 0;
             BallMaxWidthValue = (int)Math.Abs(arena.ActualWidth - ((double)arena.ActualWidth / 20));
            BasketMaxWidthValue = (int)Math.Abs(arena.ActualWidth - ((double)arena.ActualWidth / 5));
            BallMaxHeightValue = (int)Math.Abs(arena.ActualHeight - ((double)arena.ActualHeight / 10));
            BasketMaxHeightValue = (int)Math.Abs(arena.ActualHeight - ((double)arena.ActualHeight / 5));
            CreateBasket();
            this.ball.Scored += Ball_Scored;
            this.ball.Missed += Ball_Missed;
            this.user = user;
        }

        /// <summary>
        /// האירוע קורה שמחטיאים את הזריקה והוא מוסיף כדף לפי הניקוד
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ball_Missed(object sender, EventArgs e)
        {
            this.ball.Missed -= Ball_Missed;
            this.ball.Scored -= Ball_Scored;

            user.Money += ((int)sender * score);
            if(Restart!=null)
            Restart(user, null);
        }
        /// <summary>
        /// האירוע קורה כאשר נקלע סל
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ball_Scored(object sender, EventArgs e)
        {
            this.score++;
            CreateBasket();
            ShowScore(this.score, null);
        }
        /// <summary>
        /// יצירת הסל במיקום שונה ומחיקת הקודם אם יש
        /// </summary>
        private void CreateBasket()
        {   
            BasketMaxWidthValue = (int)Math.Abs(arena.ActualWidth - ((double)arena.ActualWidth / 5));
            BasketMaxHeightValue = (int)Math.Abs(arena.ActualHeight - ((double)arena.ActualHeight / 5));

            if (score <5)
            {
                if (ConstBasket != null)
                {
                    this.arena.Children.Remove(ball.GetImage());
                    this.arena.Children.Remove(ConstBasket.GetImage());
                }
                ConstBasket = new ConstBasket((double)rnd.Next(0, BasketMaxWidthValue), rnd.Next(0, BasketMaxHeightValue), arena,
               250, 100);
                ConstBasket.FirstRim = new Rect(ConstBasket.PlaceX + 20, ConstBasket.PlaceY + 38,10,36);
                ConstBasket.SecondRim = new Rect(ConstBasket.PlaceX + 215, ConstBasket.PlaceY + 38, 10, 36);
                movingBasket = null;
                this.ball = new Ball(this.user, this.score,ConstBasket, movingBasket, (double)rnd.Next(0, BallMaxWidthValue), rnd.Next(0, BallMaxHeightValue), arena,
                40, 40);
                this.ball.Scored += Ball_Scored;
                this.ball.Missed += Ball_Missed;
            }
            else
            {
                if (ConstBasket!=null)
                    this.arena.Children.Remove(ConstBasket.GetImage());
                if(movingBasket!=null)
                {
                    this.arena.Children.Remove(ball.GetImage());
                    this.arena.Children.Remove(movingBasket.GetImage());
                }
                this.arena.Children.Remove(ball.GetImage());
                this.movingBasket = new MovingBasket((double)rnd.Next(0, BasketMaxWidthValue), rnd.Next(0, BasketMaxHeightValue), arena,
                250, 100);
                movingBasket.FirstRim = new Rect(movingBasket.PlaceX + 20, movingBasket.PlaceY + 38, 15, 36);
                movingBasket.SecondRim = new Rect(movingBasket.PlaceX + 210, movingBasket.PlaceY + 38, 15, 36);
                ConstBasket = null;
                this.ball = new Ball(this.user,this.score,ConstBasket, movingBasket, (double)rnd.Next(0, BallMaxWidthValue), rnd.Next(0, BallMaxHeightValue), arena,
                40, 40);
                this.ball.Scored += Ball_Scored;
                this.ball.Missed += Ball_Missed;

            }

        }
        /// <summary>
        /// הפעולה מעבירה לכדור את מיקום השיא על מנת שהכדור ייזרק
        /// </summary>
        /// <param name="position">מיקום נקודת השיא במהלך הזריקה</param>
        internal void Throw(PointerPoint position)
        {
            ball.ThrowBall(position);
        }
        
    }
}
