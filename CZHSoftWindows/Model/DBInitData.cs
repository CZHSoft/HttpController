using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CZHSoftWindows.Model
{
    public class DBInitData :System.Data.Entity.CreateDatabaseIfNotExists<SettingEntities>
    {
        protected override void Seed(SettingEntities context)
        {
            //new List<FlowData>
            //{
            //    new FlowData{ 
            //        flowId=1,flowPos=1,
            //        url="1",isUrlParmFlag=false,urlParms="1",
            //        referer ="1",isRefererParmFlag=false,refererParms="1",
            //        isPostdataParmFlag=false,postdataParms="1",
            //        method="1",
            //        accept="1",
            //        userAgent="1",
            //        host="1",
            //        contentType="1",
            //        remark = "这是测试数据..."
            //    },

            //    new FlowData{ 
            //        flowId=1,flowPos=2,
            //        url="1",isUrlParmFlag=false,urlParms="1",
            //        referer ="1",isRefererParmFlag=false,refererParms="1",
            //        isPostdataParmFlag=false,postdataParms="1",
            //        method="1",
            //        accept="1",
            //        userAgent="1",
            //        host="1",
            //        contentType="1",
            //        remark = "2这是测试数据..."
            //    },

            //    new FlowData{ 
            //        flowId=1,flowPos=3,
            //        url="1",isUrlParmFlag=false,urlParms="1",
            //        referer ="1",isRefererParmFlag=false,refererParms="1",
            //        isPostdataParmFlag=false,postdataParms="1",
            //        method="1",
            //        accept="1",
            //        userAgent="1",
            //        host="1",
            //        contentType="1",
            //        remark = "3这是测试数据..."
            //    },

            //    new FlowData{ 
            //        flowId=1,flowPos=4,
            //        url="1",isUrlParmFlag=false,urlParms="1",
            //        referer ="1",isRefererParmFlag=false,refererParms="1",
            //        isPostdataParmFlag=false,postdataParms="1",
            //        method="1",
            //        accept="1",
            //        userAgent="1",
            //        host="1",
            //        contentType="1",
            //        remark = "4这是测试数据..."
            //    },

            //}.ForEach(a => context.Flows.Add(a));

            //new List<DicData>
            //{
            //    new DicData{
            //        dicName="1",
            //        dicPath="1"
            //    },
            //    new DicData{
            //        dicName="2",
            //        dicPath="2"
            //    },
            //    new DicData{
            //        dicName="3",
            //        dicPath="3"
            //    },
            //}.ForEach(a => context.Dics.Add(a));


        }
    }
}
