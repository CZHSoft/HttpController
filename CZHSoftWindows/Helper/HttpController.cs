using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNet4.Utilities;
using System.Net;
using CZHSoftWindows.Model;
using CZHSoft.Common;
using DotNet.Utilities;

namespace CZHSoftWindows.Helper
{
    public class HttpController
    {
        private HttpHelper helper;
        private HttpItem item;
        private HttpResult result;
        private CookieCollection cookieCollection;

        public HttpController()
        {
            helper = new HttpHelper();
            item = new HttpItem();
            result = new HttpResult();

            cookieCollection = new CookieCollection();
        }

        public bool HttpAction(FlowData flow, ref Dictionary<string,object> dic)
        {

            item = new HttpItem();

            item.URL = flow.isUrlParmFlag?ParamsSet(flow.url,ParmsGet(flow.urlParms,dic)):flow.url;

            if (!string.IsNullOrWhiteSpace(flow.referer))
            {
                item.Referer = flow.isRefererParmFlag ? ParamsSet(flow.referer, ParmsGet(flow.refererParms, dic)) : flow.referer;
            }

            item.Method = flow.method;

            if(!string.IsNullOrWhiteSpace(flow.accept))
            {
                item.Accept = flow.accept;
            }    
            if(!string.IsNullOrWhiteSpace(flow.userAgent))
            {
                item.UserAgent = flow.userAgent;
            }   
            if(!string.IsNullOrWhiteSpace(flow.host))
            {
                item.Host = flow.host;
            }  
            if(flow.isKeepAlive)
            {
                item.KeepAlive = true;
            }  
            if(!string.IsNullOrWhiteSpace(flow.contentType))
            {
                item.ContentType = flow.contentType;
            }

            if (flow.isNewCookie)
            {
                cookieCollection.Add(CookieGet(flow.cookieParms, dic));
            }

            if(flow.isNeedCookie)
            {
                item.CookieCollection = cookieCollection;
            }
            if (flow.isAcceptLanguage)
            {
                item.Header.Add("Accept-Language", flow.acceptLanguage);
            }
            if (flow.isAcceptEncoding)
            {
                item.Header.Add("Accept-Encoding", flow.acceptEncoding);
            }
            if (flow.isPostdataParmFlag)
            {
                item.Postdata = PostdataGet(flow.postdataParms, dic);
            }

            item.ResultCookieType = ResultCookieType.CookieCollection;

            result = helper.GetHtml(item);

            if (flow.resultType == "HTML")
            {
                string html = result.Html;

                if (flow.isSaveCookie)
                {
                    if (result.CookieCollection != null)
                    {
                        cookieCollection.Add(result.CookieCollection);
                    }
                }

                if (!string.IsNullOrWhiteSpace(flow.resultDeal))
                {
                     return ResultGet(html, flow.resultDeal, ref dic);
                }

            }

            return true;
        }

        private object[] ParmsGet(string data, Dictionary<string, object> dic)
        {
            SaveSettingData parmsData = JsonHelper.parse<SaveSettingData>(data);

            List<object> args = new List<object>();

            switch (parmsData.controlType)
            {
                case "Parms":
                    foreach (SettingData settingdata in parmsData.settingList)
                    {
                        if (settingdata.KeyName == "javaDataNow")
                        {
                            args.Add(DateTimeHelper.DateTimeConvert2JavaTicks(DateTime.Now).ToString());

                            continue;
                        }

                        if (!dic.Keys.Contains(settingdata.KeyName))
                        {
                            return null;
                        }

                        args.Add(dic[settingdata.KeyName]);
                    }

                    break;
            }

            return args.ToArray();
        }

        private string ParamsSet(string data,params object[] args)
        {
            return string.Format(data, args);
        }

        private string PostdataGet(string data,Dictionary<string, object> dic)
        {
            SaveSettingData parmsData = JsonHelper.parse<SaveSettingData>(data);

            foreach (SettingData settingdata in parmsData.settingList)
            {
                if (!dic.Keys.Contains(settingdata.KeyName)|| !dic.Keys.Contains(settingdata.ParmKeyName))
                {
                    return null;
                }

                if (dic[parmsData.keyName] is Dictionary<string, string>)
                {
                    (dic[parmsData.keyName] as Dictionary<string, string>)[settingdata.ParmKeyName] = (string)dic[settingdata.ParmKeyName];
                }
                else
                {
                    return null;
                }
            }

            if (dic[parmsData.keyName] is Dictionary<string, string>)
            {
                return HttpCookieHelper.GetPostParam(dic[parmsData.keyName] as Dictionary<string, string>);
            }
            else
            {
                return null;
            }

        }

        private CookieCollection CookieGet(string data, Dictionary<string, object> dic)
        {
            SaveSettingData parmsData = JsonHelper.parse<SaveSettingData>(data);

            CookieCollection cookies = new CookieCollection();

            foreach (SettingData settingdata in parmsData.settingList)
            {
                Cookie c = new Cookie();

                c.Name = settingdata.CookieName;

                if (!string.IsNullOrWhiteSpace(settingdata.ParmCookieValue))
                {
                    List<object> args = new List<object>();

                    string[] parms = settingdata.ParmCookieValue.Split(';');

                    foreach (string p in parms)
                    {
                        args.Add(dic[p]);
                    }

                    c.Value = string.Format(settingdata.CookieValue, args.ToArray());
                }
                else
                {
                    c.Value = settingdata.CookieValue;
                }

                c.Path = settingdata.CookiePath;

                c.Domain = settingdata.CookieDomain;

                cookies.Add(c);
            }

            return cookies;
        }

        private bool ResultGet(string html,string data, ref Dictionary<string, object> dic)
        {
            SaveSettingData parmsData = JsonHelper.parse<SaveSettingData>(data);

            foreach (SettingData settingdata in parmsData.settingList)
            {
                if (settingdata.QueryType == "get")
                {
                    string res = HttpCookieHelper.GetHtmlSubString(html, settingdata.QueryBegin, settingdata.QueryEnd);
                    if (!string.IsNullOrWhiteSpace(res))
                    {
                        if (dic.Keys.Contains(settingdata.KeyName))
                        {
                            dic[settingdata.KeyName] = res;

                        }
                        else
                        {
                            dic.Add(settingdata.KeyName, res);
                        }
                    }
                    else
                    {
                        return false;
                    }
                }


                if (settingdata.QueryType == "true")
                {
                    if (html.IndexOf(settingdata.QueryBegin) == -1)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

    }
}
