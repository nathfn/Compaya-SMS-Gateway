using System.Net;

namespace CompayaSmsGateway.Extensions
{
    internal static class WebResponseExtensions
    {
        public static int GetHttpStatusCode(this WebResponse r)
        {
            return (int) ((HttpWebResponse) r).StatusCode;
        }
    }
}
