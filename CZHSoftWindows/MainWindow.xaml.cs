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
using CZHSoft.Common;
using CZHSoftWindows.Model;
using CZHSoftWindows.Helper;
using CZHSoftLib.Common;

namespace CZHSoftWindows
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {

        private SettingEntities entity;

        private Dictionary<string, object> dicData;

        private List<FlowData> flows;
        private List<DicData> dics;

        private HttpController httpController;

        public MainWindow()
        {
            InitializeComponent();

            InitData();

            //TestFlow();
        }

        private void InitData()
        {
            entity = new SettingEntities();

            dicData = new Dictionary<string, object>();
            dicData.Add("javaData", 
                DateTimeHelper.DateTimeConvert2JavaTicks(DateTime.Now).ToString().Substring(0, 10));
            dicData.Add("userName","");
            dicData.Add("password", "");


            flows = entity.Flows.ToList();
            dics = entity.Dics.ToList();

            foreach (DicData data in dics)
            {
                Dictionary<string,string> temp = ExcelHelper.ConvertExcelToDic(data.dicPath);
                dicData.Add(data.dicName,temp);
            }

            httpController = new HttpController();
        }

        private void TestFlow()
        {
            foreach (FlowData data in flows)
            {
                if (!httpController.HttpAction(data, ref dicData))
                {
                    MessageBox.Show(string.Format("流程:[{0}]操作失败",data.remark));
                    return;
                }
            }

            MessageBox.Show("登录成功");
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            dicData["userName"] = tbUserName.Text;
            dicData["password"] = tbPassword.Text;

            TestFlow();
        }

    }
}
