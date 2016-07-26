using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace CZHSoftWindows.Model
{
    public class SaveSettingData
    {
        /// <summary>
        /// 程序集名称
        /// </summary>
        public string assemblyName;
        /// <summary>
        /// 类名
        /// </summary>
        public string className;
        /// <summary>
        /// 字段名
        /// </summary>
        public string fieldName;
        /// <summary>
        /// dic 字段名称
        /// </summary>
        public string keyName;
        /// <summary>
        /// Parms,Postdata,Cookie,
        /// </summary>
        public string controlType;

        public ObservableCollection<SettingData> settingList;
    }


    public class SettingData : INotifyPropertyChanged
    {
        public int Id { get; set; }

        public string KeyName { get; set; }
        public string ParmKeyName { get; set; }

        public string CookieName { get; set; }
        public string CookieValue { get; set; }
        public string ParmCookieValue { get; set; }
        public string CookiePath { get; set; }
        public string CookieDomain { get; set; }
        
        public string QueryType { get; set; }
        public string QueryBegin { get; set; }
        public string QueryEnd { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }

    public enum QueryType
    {
        处理,
        采集
    }
}
