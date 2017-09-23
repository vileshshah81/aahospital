using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using log4net;

namespace aashrayhospital.Helper
{
    public static class LogHelper
    {
        public static bool IsDebugLogEnable { get; set; }

        #region Exception Extensions

        /// <summary>
        ///     Method Log error using Log4net
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="callingType"></param>
        public static void Log(this Exception ex, object callingType)
        {
            var log = LogManager.GetLogger(callingType.GetType());
            if (!(ex is NotImplementedException))
                log.Error(ex.Message, ex);
            else
                log.Error(ex.Message);
        }

        /// <summary>
        ///     Method Log error using Log4net
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="callingType"></param>
        /// <param name="actionName"></param>
        /// <param name="controllerName"></param>
        /// <param name="logString"></param>
        public static void Log(this Exception ex, object callingType, string actionName, string controllerName,
            params KeyValuePair<string, string>[] logString)
        {
            try
            {
                var log = LogManager.GetLogger(callingType.GetType());
                StringBuilder sb = new StringBuilder();
                sb.Append("Action:" + actionName + "\t");
                sb.Append("Controller:" + controllerName + "\t");

                if (logString != null)
                {
                    foreach (KeyValuePair<string, string> logStringPair in logString)
                    {
                        sb.Append(Convert.ToString(logStringPair.Key) + ":" + Convert.ToString(logStringPair.Value) +
                                  "\t");
                    }
                }
#if DEBUG
                Trace.Write(sb.ToString());
#endif
                log.Error(sb.ToString(), ex);
            }
            catch (Exception newEx)
            {
                Log(newEx.Message, callingType);
            }
        }

        #endregion

        #region Log Extensions

        /// <summary>
        ///     Method log info into DB using Log4net
        /// </summary>
        /// <param name="message"></param>
        /// <param name="callingType"></param>
        public static void Log(this string message, object callingType)
        {
#if DEBUG
            Trace.Write(message);
#endif
            if (!IsDebugLogEnable) return;
            var log = LogManager.GetLogger(callingType.GetType());
            log.Debug(message);
        }

        /// <summary>
        ///     Method log info into DB using Log4net
        /// </summary>
        /// <param name="message"></param>
        /// <param name="callingType"></param>
        public static void LogInfo(this string message, object callingType)
        {
#if DEBUG
            Trace.Write(message);
#endif
            var log = LogManager.GetLogger(callingType.GetType());
            log.Info(message);
        }

        #endregion

        #region LogRequest Object Extension

        /// <summary>
        ///     set Log for web api method request
        /// </summary>
        /// <param name="message"></param>
        /// <param name="callingType"></param>
        public static void Log(this object callingType, string message)
        {
            try
            {
#if DEBUG
                Trace.Write(message);
#endif
                if (!IsDebugLogEnable) return;
                var log = LogManager.GetLogger(callingType.GetType());
                log.Debug(message);
            }
            catch (Exception ex)
            {
                Log(ex.Message, callingType);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="callingType"></param>
        /// <param name="actionName"></param>
        /// <param name="controllerName"></param>
        /// <param name="logString"></param>
        public static void Log(this object callingType, string actionName, string controllerName,
            params KeyValuePair<string, string>[] logString)
        {
            try
            {
                if (!IsDebugLogEnable) return;

                var log = LogManager.GetLogger(callingType.GetType());
                StringBuilder sb = new StringBuilder();
                sb.Append("Action:" + actionName + "\t");
                sb.Append("Controller:" + controllerName + "\t");

                if (logString != null)
                {
                    foreach (KeyValuePair<string, string> logStringPair in logString)
                    {
                        sb.Append(Convert.ToString(logStringPair.Key) + ":" + Convert.ToString(logStringPair.Value) +
                                  "\t");
                    }
                }
#if DEBUG
                Trace.Write(sb.ToString());
#endif
                log.Debug(sb.ToString());
            }
            catch (Exception ex)
            {
                Log(ex.Message, callingType);
            }
        }

        #endregion

        static LogHelper()
        {
            RemoveOldLogs();
        }

        public static void RemoveOldLogs()
        {
            try
            {
                string directoryName = GlobalHelper.ToLocalPath("~/App_Data/Logs/");
                if (!Directory.Exists(directoryName)) return;
                DirectoryInfo logs = new DirectoryInfo(directoryName);

                foreach (FileInfo file in logs.GetFiles())
                {
                    DateTime logDate;
                    string[] fullName = file.Name.Split('.');
                    if (!DateTime.TryParse(fullName[0], out logDate)) continue;
#if DEBUG
                    if ((logDate.Date - DateTime.Now).Days < -1)
                    {
                        file.Delete();
                    }
#else
                if ((logDate.Date - DateTime.Now).Days < -30)
                {
                    file.Delete();
                }
#endif
                }

            }
            catch (Exception ex)
            {
                ex.Log(typeof(LogHelper));
            }
        }
    }
}