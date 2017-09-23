using System;
using System.Configuration;

namespace aashrayhospital.Helper
{
    public static class AppStart
    {
        public static void Config()
        {
            EmailHelper.EmailSmtpServer = ConfigurationManager.AppSettings["EmailSmtpServer"];
            EmailHelper.EmailSmtPport = !string.IsNullOrEmpty(ConfigurationManager.AppSettings["EmailSmtPport"])
                ? Convert.ToInt16(ConfigurationManager.AppSettings["EmailSmtPport"])
                : 25;
            EmailHelper.EmailIsSsl = !string.IsNullOrEmpty(ConfigurationManager.AppSettings["EmailIsSsl"]) && Convert.ToBoolean(ConfigurationManager.AppSettings["EmailIsSsl"]);
            EmailHelper.EmailFromDisplayName = ConfigurationManager.AppSettings["EmailFromDisplayName"];
            EmailHelper.EmailFromAddress = ConfigurationManager.AppSettings["EmailFromAddress"];
            EmailHelper.EmailFromPassword = ConfigurationManager.AppSettings["EmailFromPassword"];
            EmailHelper.EmailToDisplayName = ConfigurationManager.AppSettings["EmailToDisplayName"];
            
            LogHelper.IsDebugLogEnable = !string.IsNullOrEmpty(ConfigurationManager.AppSettings["IsDebugLogEnable"]) && Convert.ToBoolean(ConfigurationManager.AppSettings["IsDebugLogEnable"]);
        }
    }
}