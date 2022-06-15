using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Input;
using Windows.UI.Xaml;
using Windows.Storage;
using DataBaseProject.Models;

namespace FinalProjectV1.Classes
{
    class Ball:MovingObject//המחלקה יורשת מהמחלקה של העצמים הזזים
    {
        int increase = 1;//תכונה בשביל הוספת הכסף
        private User user;//המשתמש שמשחק 
        private int CurrentScore;//כמות הנקודות הנוכחית
        protected double acceleration; //תאוצת הגוף
        public event EventHandler Scored;//האירוע שיתרחש כשהכדור יכנס לסל
        public event EventHandler Missed; //האירוע שיתרחש כשהכדור יצא מגבולות המסך כלומר השחקן החטיא
        private MovingBasket MovingBasket;//עצם של סל נע
        private ConstBasket ConstBasket;//עצם של סל קבוע
        /// <summary>
        /// הפעולה בונה כדור בהתאם לתכונות
        /// </summary>
        /// <param name="user">משתמש</param>
        /// <param name="CurrentScore">כמות הנקודות שנוכחית</param>
        /// <param name="constBasket">סל קבוע</param>
        /// <param name="MovingBasket">סל נע</param>
        /// <param name="placeX">מיקום בציר הX</param>
        /// <param name="placeY">מיקום בציר הY</param>
        /// <param name="arena">זירת המשחק</param>
        /// <param name="width">רוחב הכדור</param>
        /// <param name="height">גובה הכדור</param>
        public Ball(User user, int CurrentScore,ConstBasket constBasket,MovingBasket MovingBasket,double placeX, double placeY, Canvas arena,
            double width, double height) : base(placeX, placeY, arena, width, height)
        { 
            this.user = user;
            this.ConstBasket = constBasket;
            this.MovingBasket = MovingBasket;
            this.image.Source = GetSkin();
            this.speedX = 0;
            this.speedY = 0;
            this.dispatcherTimer.Tick += DispatcherTimer_Tick;
            this.CurrentScore = CurrentScore;
        }
        /// <summary>
        /// פעולה זו מחזירה את הדמות של הכדור
        /// </summary>
        /// <returns>תמונה המתאימה לבחירת השחקן</returns>
        private BitmapImage GetSkin()
        {
            if(this.user.CurrentCharacter == 2)
            {
                this.image.Width -= 5;
                this.image.Height -= 5;
                return new BitmapImage(new Uri("ms-appx:///Assets/BallsTypes/BlueBall.png"));
                
            }
            if (this.user.CurrentCharacter == 3)
            {

                increase = 2;
                return new BitmapImage(new Uri("ms-appx:///Assets/BallsTypes/GreenBall.png"));
                
            }
            if(this.user.CurrentCharacter == 4)
            {
                increase = 2;
                this.image.Width -= 5;
                this.image.Height -= 5;
                return new BitmapImage(new Uri("ms-appx:///Assets/BallsTypes/Soccer.png"));
            }
            return new BitmapImage(new Uri("ms-appx:///Assets/BallsTypes/OriginalBall.png"));
        }
        /// <summary>
        /// טיימר זה מתאים לתנועת הכדור
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DispatcherTimer_Tick(object sender, object e)
        {
            Rect ballRect = base.GetRectangle();
            Rect leftCrushRect;
            Rect rightCrushRect;
            if (MovingBasket != null)
                {
                    
                        leftCrushRect = RectHelper.Intersect(ballRect, MovingBasket.FirstRim);
                        rightCrushRect = RectHelper.Intersect(ballRect, MovingBasket.SecondRim);
                     if (leftCrushRect.Height > 0 || leftCrushRect.Width > 0 ||
                      rightCrushRect.Height > 0 || rightCrushRect.Width > 0)
                     {
                          this.speedY *= -1;
                     }   


                    if (this.PlaceX > MovingBasket.FirstRim.Right && this.PlaceX < MovingBasket.SecondRim.Left && this.PlaceY > MovingBasket.FirstRim.Top && this.PlaceY < MovingBasket.FirstRim.Bottom)
                    {
                        PlaySound("swishSound.mp3");
                        acceleration = 0;
                        speedX = 0;
                        speedY = 0;
                        PlaceY = 100;
                        PlaceX = 100;
                        if (Scored != null)
                            Scored(null, null);
                    CurrentScore++;
                    }
                    if (!(this.PlaceX <= 0 || this.PlaceX >= this.arena.ActualWidth - this.image.Width || this.PlaceY >= this.arena.ActualHeight - this.image.Height))
                    {
                        this.PlaceY += this.speedY;
                        this.PlaceX += this.speedX;
                        Canvas.SetTop(this.image, this.PlaceY);
                        Canvas.SetLeft(this.image, this.PlaceX);
                        this.speedY += this.acceleration;
                    }
                    else
                    {
                    if (Missed != null)
                        Missed(increase, null);
                   
                    }

                }


            
            if(ConstBasket!=null)
            {
                if (ConstBasket.FirstRim != null && ConstBasket.SecondRim != null)
                {
                    leftCrushRect = RectHelper.Intersect(ballRect, ConstBasket.FirstRim);
                    rightCrushRect = RectHelper.Intersect(ballRect, ConstBasket.SecondRim);
                    if (leftCrushRect.Height > 0 || leftCrushRect.Width > 0 ||
                        rightCrushRect.Height > 0 || rightCrushRect.Width > 0)
                    {
                        this.speedY *= -1;
                    }
                }

                if (this.PlaceX > ConstBasket.FirstRim.Right && this.PlaceX < ConstBasket.SecondRim.Left && this.PlaceY > ConstBasket.FirstRim.Top-20 && this.PlaceY < ConstBasket.FirstRim.Bottom+20)
                {
                    PlaySound("swishSound.mp3");
                    acceleration = 0;
                    speedX = 0;
                    speedY = 0;
                    PlaceY = 100;
                    PlaceX = 100;
                    CurrentScore++;
                    if(Scored!=null)
                    Scored(null,null);
                }
                else
                if (!(this.PlaceX <= 0 || this.PlaceX >= this.arena.ActualWidth - this.image.Width || this.PlaceY >= this.arena.ActualHeight - this.image.Height))
                {
                    this.PlaceY += this.speedY;
                    this.PlaceX += this.speedX;
                    Canvas.SetTop(this.image, this.PlaceY);
                    Canvas.SetLeft(this.image, this.PlaceX);
                     this.speedY += this.acceleration;
                }
                else
                {
                    if (Missed != null)
                        Missed(increase, null);
                    
                }
            }
           

            
        }
        /// <summary>
        /// הפעולה מקבלת את הנקודה המקסימלית של הכדור ןמחשבת את המהירות לפי זריקה משופעת
        /// </summary>
        /// <param name="maxPoint">הנקודה המקסימלית שאליה יגיע הכדור</param>
        public void ThrowBall(PointerPoint maxPoint)
        {
            
            
            this.acceleration = 10;
            double targetX = maxPoint.Position.X;
            double targetY = maxPoint.Position.Y;
            double CurrentX = Canvas.GetLeft(this.image);
            double CurrentY = Canvas.GetTop(this.image);

            this.speedY = (targetY-500-CurrentY) / 10;

            this.speedX = ( targetX-CurrentX ) / 10;
            
        }
        /// <summary>
        /// פעולה זו משמיעה מוזיקה כאשר נקלע סל
        /// </summary>
        /// <param name="FilePath">מיקום קובץ ההשמעה</param>
        private async void PlaySound(string FilePath)
        {
            MediaElement PlayMusic = new MediaElement();
            StorageFolder Folder = Windows.ApplicationModel.Package.Current.InstalledLocation;
            Folder = await Folder.GetFolderAsync("Assets");
            StorageFile sf = await Folder.GetFileAsync(FilePath);
            PlayMusic.SetSource(await sf.OpenAsync(FileAccessMode.Read), sf.ContentType);
            PlayMusic.Play();

        }
        

    }
}
