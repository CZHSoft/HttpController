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
using System.Data;
using CZHSoftWindows.Model;
using System.Windows.Controls.Primitives;

namespace CZHSoftWindows
{
    /// <summary>
    /// FlowSetting.xaml 的交互逻辑
    /// </summary>
    public partial class FlowSetting : Window
    {
        private SettingEntities entity;

        private ControlAction actionFlag = ControlAction.Cancel;

        public FlowSetting()
        {
            InitializeComponent();

            InitView();

            InitData();
        }

        private void InitView()
        {
            this.gridInfo.IsEnabled = false;
        }

        private void InitData()
        {
            
            //DataTable dataTable = new DataTable("MyDataTable");

            //dataTable.Columns.Add("ID", typeof(string));
            //dataTable.Columns.Add("序号", typeof(string));
            //dataTable.Columns.Add("描述", typeof(string));
            //for (int id = 1; id <= 20; id++)
            //    dataTable.Rows.Add(id.ToString(), "123", "细节");

            //lvFlow.DataContext = dataTable.DefaultView;

            entity = new SettingEntities();

            lvFlow.Items.SortDescriptions.Add(
                new System.ComponentModel.SortDescription(
                    "flowPos", System.ComponentModel.ListSortDirection.Ascending));

            lvFlow.ItemsSource = entity.Flows.ToList();

            //entity.Flows.Add(new FlowData() { flowId = 1, flowPos = 1 });
            //entity.SaveChanges();

            //foreach (FlowData flow in entity.Flows)
            //{
            //    Console.WriteLine("");
            //}

        }

        private bool SaveDataView(ref FlowData data,ControlAction action)
        {
            if (string.IsNullOrWhiteSpace(tbUrl.Text))
            {
                return false;
            }
            //if (string.IsNullOrWhiteSpace(tbReferer.Text))
            //{
            //    return false;
            //}
            //if (string.IsNullOrWhiteSpace(tbAccept.Text))
            //{
            //    return false;
            //}
            //if (string.IsNullOrWhiteSpace(tbContentType.Text))
            //{
            //    return false;
            //}
            //if (string.IsNullOrWhiteSpace(tbHost.Text))
            //{
            //    return false;
            //}
            //if (string.IsNullOrWhiteSpace(tbUserAgent.Text))
            //{
            //    return false;
            //}

            data.url = tbUrl.Text;
            data.urlParms = tbUrlParm.Text;
            data.isUrlParmFlag = cbUrlParms.IsChecked.Value;

            data.referer = tbReferer.Text;
            data.refererParms = tbRefererParm.Text;
            data.isRefererParmFlag = cbReferer.IsChecked.Value;

            data.isPostdataParmFlag = cbPostdata.IsChecked.Value;
            data.postdataParms = tbPostdata.Text;
            
            data.flowId = 1;
            if (action == ControlAction.Add)
            {
                data.flowPos = entity.Flows.Count() + 1;
            }

            data.host = tbHost.Text;
            data.accept = tbAccept.Text;
            data.contentType = tbContentType.Text;
            data.userAgent = tbUserAgent.Text;
            if (rbGET.IsChecked.Value)
            {
                data.method = "GET";
            }
            else
            {
                data.method = "POST";
            }

            data.isNeedCookie = cbNeedCookie.IsChecked.Value;
            data.isSaveCookie = cbSaveCookie.IsChecked.Value;
            data.isNewCookie = cbNewCookie.IsChecked.Value;
            data.cookieParms = tbNewCookieParm.Text;

            data.isKeepAlive = cbKeepAlive.IsChecked.Value;

            data.isAcceptLanguage = cbAcceptLan.IsChecked.Value;
            data.acceptLanguage = tbAcceptLan.Text;

            data.isAcceptEncoding = cbAcceptEnc.IsChecked.Value;
            data.acceptEncoding = tbAccpetEnc.Text;

            data.isNeedCer = cbCer.IsChecked.Value;
            data.cerPath = tbCerPath.Text;

            data.resultType = cmbType.SelectedIndex == 0 ? "HTML" : "BYTES";
            data.resultDeal = tbSaveInfo.Text;

            data.remark = tbRemark.Text;
            
            return true;
        }
       

        /// <summary>
        /// 界面控制
        /// </summary>
        /// <param name="control"></param>
        private void SetControl(ControlAction control)
        {
            switch (control)
            {
                case ControlAction.Add:
                    gridInfo.IsEnabled = true;
                    lvFlow.IsEnabled = false;
                    SetDataView(new FlowData());
                    break;
                case ControlAction.Mod:
                    gridInfo.IsEnabled = true;
                    lvFlow.IsEnabled = false;
                    break;
                case ControlAction.Cancel:
                    gridInfo.IsEnabled = false;
                    lvFlow.IsEnabled = true;
                    lvFlow_SelectionChanged(null, null);
                    break;
            }
        }

        /// <summary>
        /// 判断操作合法性
        /// </summary>
        /// <param name="control"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        private bool ActionControl(ControlAction control,ControlAction action)
        {
            if (control == ControlAction.Cancel)
            {
                return true;
            }
            else
            {
                switch (control)
                {
                    case ControlAction.Add:
                    case ControlAction.Clean:
                    case ControlAction.Del:
                    case ControlAction.Mod:
                        if (action != ControlAction.Cancel && action != ControlAction.Save)
                        {
                            return false;
                        }
                        break;
                    case ControlAction.Save:
                        if (action != ControlAction.Add && action != ControlAction.Mod)
                        {
                            return false;
                        }
                        break;
                }

                return true;
            }
        }

        /// <summary>
        /// 操作动作
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        private bool DoAction(ControlAction action)
        {
            switch (action)
            {
                case ControlAction.Add:
                    FlowData tempData = new FlowData();
                    SaveDataView(ref tempData,action);
                    entity.Flows.Add(tempData);
                    if (entity.SaveChanges() != 1)
                    {
                        return false;
                    }
                    break;
                case ControlAction.Mod:
                    FlowData selectData = lvFlow.SelectedItem as FlowData;
                    if (selectData != null)
                    {
                        SaveDataView(ref selectData,action);
                        if (entity.SaveChanges() != 1)
                        {
                            return false;
                        }
                    }
                    break;
                case ControlAction.Del:
                    break;
                case ControlAction.Clean:
                    break;

            }

            return true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;

            switch (btn.Content as string)
            {
                case "添加":
                    if (ActionControl(actionFlag, ControlAction.Add))
                    {
                        actionFlag = ControlAction.Add;
                    }
                    break;
                case "修改":
                    if (ActionControl(actionFlag, ControlAction.Mod))
                    {
                        actionFlag = ControlAction.Mod;
                    }
                    break;
                case "删除":
                    if (ActionControl(actionFlag, ControlAction.Del))
                    {
                        if (DoAction(ControlAction.Del))
                        {
                            actionFlag = ControlAction.Cancel;
                        }
                    }
                    break;
                case "清空":
                    if (ActionControl(actionFlag, ControlAction.Clean))
                    {
                        if (DoAction(ControlAction.Clean))
                        {
                            actionFlag = ControlAction.Cancel;
                        }
                    }
                    break;
                case "保存":
                    if (ActionControl(actionFlag, ControlAction.Save))
                    {
                        if (DoAction(actionFlag))
                        {
                            actionFlag = ControlAction.Cancel;
                        }
                    }
                    break;
                case "取消":
                    if (ActionControl(actionFlag, ControlAction.Cancel))
                    {
                        actionFlag = ControlAction.Cancel;
                    }
                    break;
                case "退出":
                    this.Close();
                    return;
            }

            SetControl(actionFlag);
        }

        #region lvFlow

        private void SetDataView(FlowData data)
        {
            if (data == null)
            {
                return;
            }

            this.tbUrl.Text = data.url;
            this.cbUrlParms.IsChecked = data.isUrlParmFlag;
            this.tbUrlParm.Text = data.urlParms;

            this.tbReferer.Text = data.referer;
            this.cbReferer.IsChecked = data.isRefererParmFlag;
            this.tbRefererParm.Text = data.refererParms;

            this.cbPostdata.IsChecked = data.isPostdataParmFlag;
            this.tbPostdata.Text = data.postdataParms;

            switch (data.method)
            {
                case "POST":
                    rbPOST.IsChecked = true;
                    break;
                default:
                    rbGET.IsChecked = true;
                    break;
            }

            this.tbAccept.Text = data.accept;
            this.tbUserAgent.Text = data.userAgent;
            this.tbHost.Text = data.host;
            this.tbContentType.Text = data.contentType;

            this.cbNeedCookie.IsChecked = data.isNeedCookie;
            this.cbSaveCookie.IsChecked = data.isSaveCookie;
            this.cbNewCookie.IsChecked = data.isNewCookie;
            this.tbNewCookieParm.Text = data.cookieParms;

            this.cbKeepAlive.IsChecked = data.isKeepAlive;

            this.cbAcceptLan.IsChecked = data.isAcceptLanguage;
            this.tbAcceptLan.Text = data.acceptLanguage;

            this.cbAcceptEnc.IsChecked = data.isAcceptEncoding;
            this.tbAccpetEnc.Text = data.acceptEncoding;

            this.cbCer.IsChecked = data.isNeedCer;
            this.tbCerPath.Text = data.cerPath;

            this.cmbType.SelectedIndex = data.resultType == "BYTES" ? 1 : 0;
            this.tbSaveInfo.Text = data.resultDeal;

            this.tbRemark.Text = data.remark;
        }

        private void lvFlow_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FlowData data = lvFlow.SelectedItem as FlowData;

            if (data != null)
            {
                SetDataView(data);
            }
        }

        private void lvFlow_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(System.Collections.IList)))
            {
                System.Collections.IList peopleList = e.Data.GetData(typeof(System.Collections.IList)) as System.Collections.IList;
               
                //index为放置时鼠标下元素项的索引  
                int index = GetCurrentIndex(new GetPositionDelegate(e.GetPosition));

                if (index > -1)
                {
                    FlowData data = lvFlow.SelectedItem as FlowData;

                    if (data != null)
                    {
                        int tempPos = data.flowPos;

                        if (tempPos > index + 1)
                        {
                            foreach (FlowData d in entity.Flows.Where(o => o.flowPos > index + 1 && o.flowPos < tempPos))
                            {
                                d.flowPos++;
                            }
                        }
                        else
                        {
                            foreach (FlowData d in entity.Flows.Where(o => o.flowPos >= tempPos && o.flowPos < index + 1))
                            {
                                d.flowPos--;
                            }
                        }

                        data.flowPos = index+1;

                        entity.SaveChanges();
                    }

                    lvFlow.ItemsSource = entity.Flows.ToList();
                }
            }  
        }

        private void lvFlow_MouseMove(object sender, MouseEventArgs e)
        {
            ListView listview = sender as ListView;
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                System.Collections.IList list = listview.SelectedItems as System.Collections.IList;
                DataObject data = new DataObject(typeof(System.Collections.IList), list);
                if (list.Count > 0)
                {
                    DragDrop.DoDragDrop(listview, data, DragDropEffects.Move);
                }
            }  
        }

        private int GetCurrentIndex(GetPositionDelegate getPosition)
        {
            int index = -1;
            for (int i = 0; i < lvFlow.Items.Count; ++i)
            {
                ListViewItem item = GetListViewItem(i);
                if (item != null && this.IsMouseOverTarget(item, getPosition))
                {
                    index = i;
                    break;
                }
            }
            return index;
        }

        private bool IsMouseOverTarget(Visual target, GetPositionDelegate getPosition)
        {
            Rect bounds = VisualTreeHelper.GetDescendantBounds(target);
            Point mousePos = getPosition((IInputElement)target);
            return bounds.Contains(mousePos);
        }

        delegate Point GetPositionDelegate(IInputElement element);

        ListViewItem GetListViewItem(int index)
        {
            if (lvFlow.ItemContainerGenerator.Status != GeneratorStatus.ContainersGenerated)
                return null;
            return lvFlow.ItemContainerGenerator.ContainerFromIndex(index) as ListViewItem;
        }
        
        #endregion

        //private void checkBox_Checked(object sender, RoutedEventArgs e)
        //{
        //   CheckBox cb = sender as CheckBox;
            
        //    switch (cb.Name)
        //    {
        //        case "cbUrlParms":
        //            if (string.IsNullOrWhiteSpace(tbUrlParm.Text))
        //            {
        //                FlowElementSetting s = new FlowElementSetting(0, this);

        //                if (s.ShowDialog() == true)
        //                {
        //                    tbUrlParm.Text = this.Tag.ToString();
        //                }
        //                else
        //                {
        //                    tbUrlParm.Text = "";
        //                    cbUrlParms.IsChecked = false;
        //                }
        //            }
        //            break;
        //        case "cbReferer":
        //            if (string.IsNullOrWhiteSpace(tbRefererParm.Text))
        //            {
        //                FlowElementSetting s = new FlowElementSetting(0, this);

        //                if (s.ShowDialog() == true)
        //                {
        //                    tbRefererParm.Text = this.Tag.ToString();
        //                }
        //                else
        //                {
        //                    tbRefererParm.Text = "";
        //                    cbReferer.IsChecked = false;
                            
        //                }
        //            }
        //            break;
        //        case "cbPostdata":
        //            if (string.IsNullOrWhiteSpace(tbPostdata.Text))
        //            {
        //                FlowElementSetting s = new FlowElementSetting(1, this);

        //                if (s.ShowDialog() == true)
        //                {
        //                    tbPostdata.Text = this.Tag.ToString();
        //                }
        //                else
        //                {
        //                    tbRefererParm.Text = "";
        //                    cbReferer.IsChecked = false;

        //                }
        //            }
        //            break;
        //    }

            
        //}

        //private void checkBox_Unchecked(object sender, RoutedEventArgs e)
        //{
        //    CheckBox cb = sender as CheckBox;

        //    switch (cb.Name)
        //    {
        //        case "cbUrlParms":
        //            if (!string.IsNullOrWhiteSpace(tbUrlParm.Text))
        //            {
        //                if (MessageBox.Show("是否要取消URL参数并删除对应的参数值?", "警告", MessageBoxButton.YesNo) == MessageBoxResult.No)
        //                {
        //                    (sender as CheckBox).IsChecked = true;
        //                }
        //                else
        //                {
        //                    tbUrlParm.Text = "";
        //                }
        //            }
        //            break;
        //        case "cbReferer":
        //            if (!string.IsNullOrWhiteSpace(tbRefererParm.Text))
        //            {
        //                if (MessageBox.Show("是否要取消Referer参数并删除对应的参数值?", "警告", MessageBoxButton.YesNo) == MessageBoxResult.No)
        //                {
        //                    (sender as CheckBox).IsChecked = true;
        //                }
        //                else
        //                {
        //                    tbRefererParm.Text = "";
        //                }
        //            }
        //            break;
        //        case "cbPostdata":
        //            if (!string.IsNullOrWhiteSpace(tbPostdata.Text))
        //            {
        //                if (MessageBox.Show("是否要取消Postdata参数并删除对应的参数值?", "警告", MessageBoxButton.YesNo) == MessageBoxResult.No)
        //                {
        //                    (sender as CheckBox).IsChecked = true;
        //                }
        //                else
        //                {
        //                    tbPostdata.Text = "";
        //                }
        //            }
        //            break;
        //        case "cbNewCookie":
        //            if (!string.IsNullOrWhiteSpace(tbNewCookieParm.Text))
        //            {
        //                if (MessageBox.Show("是否要取消Cookie参数并删除对应的参数值?", "警告", MessageBoxButton.YesNo) == MessageBoxResult.No)
        //                {
        //                    (sender as CheckBox).IsChecked = true;
        //                }
        //                else
        //                {
        //                    tbNewCookieParm.Text = "";
        //                }
        //            }
        //            break;

        //    }

            
        //}

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            CheckBox cb = sender as CheckBox;

            if (cb.IsChecked.Value)
            {
                switch (cb.Name)
                {
                    case "cbUrlParms":
                        if (string.IsNullOrWhiteSpace(tbUrlParm.Text))
                        {
                            FlowElementSetting s = new FlowElementSetting(0, this);

                            if (s.ShowDialog() == true)
                            {
                                tbUrlParm.Text = this.Tag.ToString();
                                cbUrlParms.IsChecked = true;
                            }
                            else
                            {
                                tbUrlParm.Text = "";
                                cbUrlParms.IsChecked = false;
                            }
                        }
                        break;
                    case "cbReferer":
                        if (string.IsNullOrWhiteSpace(tbRefererParm.Text))
                        {
                            FlowElementSetting s = new FlowElementSetting(0, this);

                            if (s.ShowDialog() == true)
                            {
                                tbRefererParm.Text = this.Tag.ToString();
                                cbReferer.IsChecked = true;
                            }
                            else
                            {
                                tbRefererParm.Text = "";
                                cbReferer.IsChecked = false;

                            }
                        }
                        break;
                    case "cbPostdata":
                        if (string.IsNullOrWhiteSpace(tbPostdata.Text))
                        {
                            FlowElementSetting s = new FlowElementSetting(1, this);

                            if (s.ShowDialog() == true)
                            {
                                tbPostdata.Text = this.Tag.ToString();
                                cbPostdata.IsChecked = true;
                            }
                            else
                            {
                                tbPostdata.Text = "";
                                cbPostdata.IsChecked = false;

                            }
                        }
                        break;
                    case "cbNewCookie":
                        if (string.IsNullOrWhiteSpace(tbNewCookieParm.Text))
                        {
                            FlowElementSetting s = new FlowElementSetting(2, this);

                            if (s.ShowDialog() == true)
                            {
                                tbNewCookieParm.Text = this.Tag.ToString();
                                cbNewCookie.IsChecked = true;
                            }
                            else
                            {
                                tbNewCookieParm.Text = "";
                                cbNewCookie.IsChecked = false;

                            }
                        }
                        break;
                }

            }
            else
            {
                switch (cb.Name)
                {
                    case "cbUrlParms":
                        if (!string.IsNullOrWhiteSpace(tbUrlParm.Text))
                        {
                            FlowElementSetting s = new FlowElementSetting(0,tbUrlParm.Text ,this);

                            bool? flag = s.ShowDialog();

                            if (flag == true)
                            {
                                if (this.Tag == null)
                                {
                                    tbUrlParm.Text = "";
                                    cbUrlParms.IsChecked = false;
                                }
                                else
                                {
                                    tbUrlParm.Text = this.Tag.ToString();
                                    cbUrlParms.IsChecked = true;
                                }
                            }
                            else if (flag == false)
                            {
                                cbUrlParms.IsChecked = true;
                            }
                        }
                        break;
                    case "cbReferer":
                        if (!string.IsNullOrWhiteSpace(tbRefererParm.Text))
                        {

                            FlowElementSetting s = new FlowElementSetting(0, tbRefererParm.Text, this);

                            bool? flag = s.ShowDialog();

                            if (flag == true)
                            {
                                if (this.Tag == null)
                                {
                                    tbRefererParm.Text = "";
                                    cbReferer.IsChecked = false;
                                }
                                else
                                {
                                    tbRefererParm.Text = this.Tag.ToString();
                                    cbReferer.IsChecked = true;
                                }
                            }
                            else if (flag == false)
                            {
                                cbReferer.IsChecked = true;
                            }
                        }
                        break;
                    case "cbPostdata":
                        if (!string.IsNullOrWhiteSpace(tbPostdata.Text))
                        {


                            FlowElementSetting s = new FlowElementSetting(1, tbPostdata.Text, this);
                            
                            bool? flag = s.ShowDialog();

                            if (flag == true)
                            {
                                if (this.Tag == null)
                                {
                                    tbPostdata.Text = "";
                                    cbPostdata.IsChecked = false;
                                }
                                else
                                {
                                    tbPostdata.Text = this.Tag.ToString();
                                    cbPostdata.IsChecked = true;
                                }
                            }
                            else if (flag == false)
                            {
                                cbPostdata.IsChecked = true;
                            }

                        }
                        break;
                    case "cbNewCookie":
                        if (!string.IsNullOrWhiteSpace(tbNewCookieParm.Text))
                        {
                            FlowElementSetting s = new FlowElementSetting(2, tbNewCookieParm.Text, this);

                            bool? flag = s.ShowDialog();

                            if (flag == true)
                            {
                                if (this.Tag == null)
                                {
                                    tbNewCookieParm.Text = "";
                                    cbNewCookie.IsChecked = false;
                                }
                                else
                                {
                                    tbNewCookieParm.Text = this.Tag.ToString();
                                    cbNewCookie.IsChecked = true;
                                }
                            }
                            else if (flag == false)
                            {
                                cbNewCookie.IsChecked = true;
                            }
                        }
                        break;
                }

            }
        }

        private void btnSaveInfo_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbSaveInfo.Text))
            {
                FlowElementSetting s = new FlowElementSetting(3, this);

                if (s.ShowDialog() == true)
                {
                    tbSaveInfo.Text = this.Tag.ToString();
                }
            }
            else
            {
                FlowElementSetting s = new FlowElementSetting(3, tbSaveInfo.Text, this);

                if (s.ShowDialog() == true)
                {
                    tbSaveInfo.Text = this.Tag.ToString();
                }
            }

            
        }

    }

    public enum ControlAction
    {
        Add,
        Mod,
        Del,
        Clean,
        Save,
        Cancel
    }
}
