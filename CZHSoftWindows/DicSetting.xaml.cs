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
using CZHSoftWindows.Model;
using System.Windows.Controls.Primitives;

namespace CZHSoftWindows
{
    /// <summary>
    /// DicSetting.xaml 的交互逻辑
    /// </summary>
    public partial class DicSetting : Window
    {
        //public List<DicData> SettingList { get; set; }

        private SettingEntities entity;

        public DicSetting()
        {
            InitializeComponent();

            InitView();

        }

        private void InitView()
        {
            entity = new SettingEntities();

            //SettingList = new List<DicData>();

            //SettingList = entity.Dics.ToList();

            //dgSetting.Columns.Clear();

            //dgSetting.Columns.Add(new DataGridTextColumn() { Header = "序号", IsReadOnly = true, Binding = new Binding("Id") });
            //dgSetting.Columns.Add(new DataGridTextColumn() { Header = "定义名称", Binding = new Binding("dicName") });
            //dgSetting.Columns.Add(new DataGridTextColumn() { Header = "文档路径", Binding = new Binding("dicPath") });

            //this.DataContext = this;

            //dgSetting.SetBinding(ItemsControl.ItemsSourceProperty, new Binding() { Source = entity.Dics.ToList() });

            dgSetting.ItemsSource = entity.Dics.ToList();

            //dgSetting.Focus();

        }

        private void DataGrid_InitializingNewItem(object sender, InitializingNewItemEventArgs e)
        {
            //if (entity.Dics.ToList().Count==0)
            //{
            //    ((DicData)e.NewItem).Id = 1;
            //}
            //else
            //{
            //    ((DicData)e.NewItem).Id = entity.Dics.ToList().Max(p => p.Id) + 1;
            //}
            //((Student)e.NewItem).Selected = true;

            entity.Dics.Add(new DicData());
            entity.SaveChanges();

            dgSetting.ItemsSource = entity.Dics.ToList();
        }

        private void dgSetting_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter && dgSetting.CurrentColumn.Header.ToString() == "配置命名")
            {
                dgSetting.SelectedIndex++;
                dgSetting.BeginEdit();
            }
        }

        private void dgSetting_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter && dgSetting.CurrentColumn.Header.ToString() == "配置命名")
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
            entity.SaveChanges();
        }

    }
}
