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
using System.Windows.Shapes;

namespace CZHSoftWindows
{
    /// <summary>
    /// Welcome.xaml 的交互逻辑
    /// </summary>
    public partial class Welcome : Window
    {
        public Welcome()
        {
            InitializeComponent();

            System.Data.Entity.Database.SetInitializer(new Model.DBInitData());
        }

        private void btnFlowSetting_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            FlowSetting setting = new FlowSetting();
            setting.ShowDialog();
            this.Show();
        }

        private void btnDicSetting_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            DicSetting setting = new DicSetting();
            setting.ShowDialog();
            this.Show();
        }

        private void btnRun_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainWindow main = new MainWindow();
            main.ShowDialog();
            this.Show();
        }

       
    }
}
