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
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls.Primitives;
using CZHSoftWindows.Model;
using CZHSoft.Common;
using System.Collections;

namespace CZHSoftWindows
{
    /// <summary>
    /// FlowElementSetting.xaml 的交互逻辑
    /// </summary>
    public partial class FlowElementSetting : Window
    {
        public ObservableCollection<SettingData> SettingList { get; set; }

        public FlowElementSetting(int type, Window owner)
        {
            this.Owner = owner;

            InitializeComponent();

            InitView(type);

        }

        public FlowElementSetting(int type,string data ,Window owner)
        {
            this.Owner = owner;

            InitializeComponent();

            InitView(type,data);

        }


        private void InitView(int type)
        {
            SettingList = new ObservableCollection<SettingData>();

            dgSetting.Columns.Clear();

            switch (type)
            {
                case 0:
                    dgSetting.Columns.Add(new DataGridTextColumn() { Header = "序号", IsReadOnly = true, Binding = new Binding("Id") });
                    dgSetting.Columns.Add(new DataGridTextColumn() { Header = "字典变量", Binding = new Binding("KeyName") });
                    cmbType.SelectedIndex = 0;
                    break;
                case 1:
                    dgSetting.Columns.Add(new DataGridTextColumn() { Header = "序号", IsReadOnly = true, Binding = new Binding("Id") });
                    dgSetting.Columns.Add(new DataGridTextColumn() { Header = "字典变量", Binding = new Binding("KeyName") });
                    dgSetting.Columns.Add(new DataGridTextColumn() { Header = "参数变量", Binding = new Binding("ParmKeyName") });
                    cmbType.SelectedIndex = 1;
                    break;
                case 2:
                    dgSetting.Columns.Add(new DataGridTextColumn() { Header = "序号", IsReadOnly = true, Binding = new Binding("Id") });
                    dgSetting.Columns.Add(new DataGridTextColumn() { Header = "Cookie名称", Binding = new Binding("CookieName") });
                    dgSetting.Columns.Add(new DataGridTextColumn() { Header = "Cookie值", Binding = new Binding("CookieValue") });
                    dgSetting.Columns.Add(new DataGridTextColumn() { Header = "Cookie值参数", Binding = new Binding("ParmCookieValue") });
                    dgSetting.Columns.Add(new DataGridTextColumn() { Header = "Cookie地址", Binding = new Binding("CookiePath") });
                    dgSetting.Columns.Add(new DataGridTextColumn() { Header = "Cookie域", Binding = new Binding("CookieDomain") });
                    cmbType.SelectedIndex = 2;
                    break;
                case 3:
                    dgSetting.Columns.Add(new DataGridTextColumn() { Header = "序号", IsReadOnly = true, Binding = new Binding("Id") });
                    dgSetting.Columns.Add(new DataGridTextColumn() { Header = "字典变量", Binding = new Binding("KeyName") });
                    dgSetting.Columns.Add(new DataGridTextColumn() { Header = "结果类型", Binding = new Binding("QueryType") });
                    dgSetting.Columns.Add(new DataGridTextColumn() { Header = "条件开始位置", Binding = new Binding("QueryBegin") });
                    dgSetting.Columns.Add(new DataGridTextColumn() { Header = "条件结束位置", Binding = new Binding("QueryEnd") });
                    cmbType.SelectedIndex = 3;
                    break;
            }

            this.DataContext = this;

            dgSetting.Focus();
        }

        private void InitView(int type, string data)
        {
            InitView(type);

            SaveSettingData setting = JsonHelper.parse<SaveSettingData>(data);

            SettingList = setting.settingList;

            tbAssemblyName.Text = setting.assemblyName;
            tbClassName.Text = setting.className;
            tbFieldName.Text = setting.fieldName;
            tbKeyName.Text = setting.keyName;

            dgSetting.Items.Clear();
            dgSetting.ItemsSource = SettingList;

        }

        private void DataGrid_InitializingNewItem(object sender, InitializingNewItemEventArgs e)
        {
            if (SettingList == null || SettingList.Count == 0)
            {
                ((SettingData)e.NewItem).Id = 1;
            }
            else
            {
                ((SettingData)e.NewItem).Id = SettingList.Max(p => p.Id) + 1;
            }

        }

        private void dgSetting_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter && dgSetting.CurrentColumn.Header.ToString() == "字典变量")
            {
                dgSetting.SelectedIndex++;
                dgSetting.BeginEdit();
            }
        }

        private void dgSetting_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter && dgSetting.CurrentColumn.Header.ToString() == "字典变量")
            {
                string name = ((TextBox)e.OriginalSource).Text;
            }
        }

        private void dgSetting_Loaded(object sender, RoutedEventArgs e)
        {
            DataGridCell cell = GetCell(0, 1);
            if (cell != null)
            {
                dgSetting.SelectedIndex = 0;
                cell.Focus();
                dgSetting.BeginEdit();
            }
        }


        /// <summary>
        /// 根据行、列索引取的对应单元格对象
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public DataGridCell GetCell(int row, int column)
        {
            DataGridRow rowContainer = GetRow(row);

            if (rowContainer != null)
            {
                DataGridCellsPresenter presenter = GetVisualChild<DataGridCellsPresenter>(rowContainer);

                // try to get the cell but it may possibly be virtualized
                DataGridCell cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(column);
                if (cell == null)
                {
                    // now try to bring into view and retreive the cell
                    dgSetting.ScrollIntoView(rowContainer, dgSetting.Columns[column]);
                    cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(column);
                }
                return cell;
            }
            return null;
        }

        /// <summary>
        /// 根据行索引取的行对象
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public DataGridRow GetRow(int index)
        {
            DataGridRow row = (DataGridRow)dgSetting.ItemContainerGenerator.ContainerFromIndex(index);
            if (row == null)
            {
                // may be virtualized, bring into view and try again
                dgSetting.ScrollIntoView(dgSetting.Items[index]);
                row = (DataGridRow)dgSetting.ItemContainerGenerator.ContainerFromIndex(index);
            }
            return row;
        }

        /// <summary>
        /// 获取指定类型的子元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parent"></param>
        /// <returns></returns>
        public T GetVisualChild<T>(Visual parent) where T : Visual
        {
            T child = default(T);
            int numVisuals = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < numVisuals; i++)
            {
                Visual v = (Visual)VisualTreeHelper.GetChild(parent, i);
                child = v as T;
                if (child == null)
                {
                    child = GetVisualChild<T>(v);
                }
                if (child != null)
                {
                    break;
                }
            }
            return child;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            SaveSettingData setting = new SaveSettingData();
            setting.assemblyName = tbAssemblyName.Text;
            setting.className = tbClassName.Text;
            setting.fieldName = tbFieldName.Text;
            setting.controlType = cmbType.Text;
            setting.keyName = tbKeyName.Text;
            setting.settingList = SettingList;

            string data = JsonHelper.stringify(setting);

            this.Owner.Tag = data;

            //SaveSettingData setting2 = JsonHelper.parse<SaveSettingData>(data);

            this.DialogResult = true;

        }

        private void btnClean_Click(object sender, RoutedEventArgs e)
        {
            this.Owner.Tag = null;

            this.DialogResult = true;
        }
    }



    
}
