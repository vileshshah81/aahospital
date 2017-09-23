using System.IO;

namespace aashrayhospital.com
{
    using Helper;

    public static class LogConfig
    {
        /// <summary>
        /// </summary>
        public static void Register()
        {
            // log4net file info.
            FileInfo log4NetFileInfo = new FileInfo(Path.Combine(GlobalHelper.APP_ROOT_PATH, "log4net.config"));

            // log4net initializing configuration.
            log4net.Config.XmlConfigurator.Configure(log4NetFileInfo);
        }
    }
}