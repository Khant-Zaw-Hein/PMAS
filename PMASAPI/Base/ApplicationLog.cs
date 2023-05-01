using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMASAPI.Base
{
    public class ApplicationLog
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static void WriteError(string strMessage)
        {
            log.Error(strMessage);
        }

        public static void WriteWarning(string strMessage)
        {
            log.Warn(strMessage);
        }

        public static void WriteInfo(string strMessage)
        {
            log.Info(strMessage);
        }

        public static void WriteTrace(string strMessage)
        {
            log.Debug(strMessage);
        }

        #region Log with Logger
        public static void WriteError(string strMessage, string loggerName)
        {
            log4net.ILog customLog = log4net.LogManager.GetLogger(loggerName);
            customLog.Error(strMessage);
        }
        public static void WriteWarning(string strMessage, string loggerName)
        {
            log4net.ILog customLog = log4net.LogManager.GetLogger(loggerName);
            customLog.Warn(strMessage);
        }
        public static void WriteInfo(string strMessage, string loggerName)
        {
            log4net.ILog customLog = log4net.LogManager.GetLogger(loggerName);
            customLog.Info(strMessage);
        }
        public static void WriteTrace(string strMessage, string loggerName)
        {
            log4net.ILog customLog = log4net.LogManager.GetLogger(loggerName);
            customLog.Debug(strMessage);
        }

        public static void CustomWriteInfo(int JobID, string strMessage, string loggerName)
        {
            log4net.GlobalContext.Properties["LogName"] = JobID;
            log4net.Config.XmlConfigurator.Configure();
            log4net.ILog customLog = log4net.LogManager.GetLogger(loggerName);
            customLog.Info(strMessage);
        }
        #endregion Log with Logger
    }
}