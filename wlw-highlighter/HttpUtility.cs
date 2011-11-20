using System;
using System.Collections.Generic;
using System.Text;

namespace me.lizj.wlw_highlighter
{
    class HttpUtility
    {
        private static Dictionary<string, string> htmlCodes = new Dictionary<string, string>
        {
            {"&", "&amp;"},
            {"<", "&lt;"},
            {">", "&gt;"},
            {"\"", "&quot;"}
        };


        internal static string HtmlEncode(string p)
        {
            if (string.IsNullOrEmpty(p))
            {
                return p;
            }
            var sb = new StringBuilder(p);
            foreach (var item in htmlCodes)
            {
                sb = sb.Replace(item.Key, item.Value);
            }
            return sb.ToString();
        }

        internal static string HtmlDecode(string p)
        {
            if (string.IsNullOrEmpty(p))
            {
                return p;
            }
            var sb = new StringBuilder(p);
            foreach (var item in htmlCodes)
            {
                sb = sb.Replace(item.Value, item.Key);
            }
            return sb.ToString();
        }
    }
}
