using System;
using System.IO;
using System.Web;
using System.Web.Hosting;

namespace aashrayhospital.Helper
{
    public static class GlobalHelper
    {
        static GlobalHelper()
        {
            APP_ROOT_PATH = HostingEnvironment.ApplicationPhysicalPath ?? AppDomain.CurrentDomain.BaseDirectory;
        }

        /// <summary>
        /// </summary>
        public static string WebSiteUrl
        {
            get
            {
                return HttpContext.Current == null
                    ? string.Empty
                    : string.Format("{0}{1}{2}{3}/",
                        HttpContext.Current.Request.Url.Scheme,
                        Uri.SchemeDelimiter,
                        HttpContext.Current.Request.Url.Host,
                        (HttpContext.Current.Request.Url.IsDefaultPort
                            ? string.Empty
                            : string.Format(":{0}", HttpContext.Current.Request.Url.Port)));
            }
        }

        public static string APP_ROOT_PATH { get; set; }

        public static string AssemblyVersion(Type type)
        {
            return string.Format("{0}", type.Assembly.GetName().Version);
        }

        public static string ToLocalPath(string path)
        {
            try
            {
                return HostingEnvironment.MapPath(path);
            }
            catch (Exception ex)
            {
                ex.Log(typeof (GlobalHelper));
                string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                int binIndex = baseDirectory.IndexOf("\\bin\\", StringComparison.Ordinal);
                if (binIndex >= 0)
                    baseDirectory = baseDirectory.Substring(0, binIndex);
                else if (baseDirectory.EndsWith("\\bin"))
                    baseDirectory = baseDirectory.Substring(0, baseDirectory.Length - 4);

                path = path.Replace("~/", "").TrimStart('/').Replace('/', '\\');
                return Path.Combine(baseDirectory, path);
            }
        }
    }
}