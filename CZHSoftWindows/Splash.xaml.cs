using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Windows.Media.Animation;

namespace CZHSoftWindows
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class Splash : Window
    {
        //定时器  
        private DispatcherTimer mDataTimer = null;

        public Splash()
        {
            InitializeComponent();

            //InitTimer();
        }

        private void LoadComplete(object sender, EventArgs e)
        {
            //MessageBox.Show("启动完成...");
            this.Hide();
            Welcome welcome = new Welcome();
            Application.Current.MainWindow = welcome;
            this.Close();
            welcome.ShowDialog();
            
        }

        private void InitTimer()
        {
            if (mDataTimer == null)
            {
                mDataTimer = new DispatcherTimer();
                mDataTimer.Tick += new EventHandler(DataTimer_Tick);
                mDataTimer.Interval = TimeSpan.FromMilliseconds(150);

                mDataTimer.Start();
            }
        }

        private void DataTimer_Tick(object sender, EventArgs e)
        {
            if (this.Opacity >= 1)
            {
                mDataTimer.Stop();
                //Application.Current.Shutdown();

                ////DoubleAnimation widthAnimation = new DoubleAnimation();
                ////widthAnimation.From = 300;
                ////widthAnimation.To = 600;
                ////widthAnimation.Duration = new Duration(TimeSpan.Parse("0:0:1"));
                //DoubleAnimation heigthAnimation = new DoubleAnimation();
                //heigthAnimation.From = 120;
                //heigthAnimation.To = 360;
                //heigthAnimation.Duration = new Duration(TimeSpan.Parse("0:0:5"));
                //this.BeginAnimation(MainWindow.HeightProperty, heigthAnimation);
                ////this.BeginAnimation(Window.TopProperty, b);

            }
            else
            {
                this.Opacity += 0.1;
            }
        }  


    }
}
