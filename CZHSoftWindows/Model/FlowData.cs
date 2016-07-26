using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CZHSoftWindows.Model
{
    public class FlowData
    {
        public int Id { get; set; }

        public int flowId { get; set; }
        public int flowPos { get; set; }

        public string url { get; set; }
        public bool isUrlParmFlag { get; set; }
        public string urlParms { get; set; }

        public string referer { get; set; }
        public bool isRefererParmFlag { get; set; }
        public string refererParms { get; set; }

        public bool isPostdataParmFlag { get; set; }
        public string postdataParms { get; set; }

        public string method { get; set; }
        public string accept { get; set; }
        public string userAgent { get; set; }
        public string host { get; set; }
        public string contentType { get; set; }

        public bool isNeedCookie { get; set; }
        public bool isSaveCookie { get; set; }
        public bool isNewCookie { get; set; }
        public string cookieParms { get; set; }

        public bool isKeepAlive { get; set; }

        public bool isAcceptLanguage { get; set; }
        public string acceptLanguage { get; set; }

        public bool isAcceptEncoding { get; set; }
        public string acceptEncoding { get; set; }

        public bool isNeedCer { get; set; }
        public string cerPath { get; set; }

        public string resultType { get; set; }
        public string resultDeal { get; set; }

        public string remark { get; set; }
    }
}
