using log4net;
using System;
using System.Diagnostics;
using System.Reflection;

namespace FTEL.Common.Utilities
{
    public class LogUtil
    {
        /// <summary>
        /// Ghi log thông tin
        /// </summary>
        /// <param name="message">Thông báo</param>
        public static void Info(string message)
        {
            StackFrame frame = new StackFrame(1);
            MethodBase method = frame.GetMethod();
            string logMessage = String.Format("Method:{0}  Message:{1}", method.Name, message);
            ILog _log = LogManager.GetLogger(method.DeclaringType);
            _log.Info(logMessage);
        }

        /// <summary>
        /// Ghi log lỗi bằng log4net
        /// </summary>
        /// <param name="message">Thông báo lỗi</param>
        /// <param name="ex">Exception</param>
        public static void Error(string message, Exception ex)
        {
            StackFrame frame = new StackFrame(1);
            MethodBase method = frame.GetMethod();
            string logMessage = String.Format("Method:{0}  Message:{1}", method.Name, message);
            ILog _log = LogManager.GetLogger(method.DeclaringType);
            _log.Error(logMessage, ex);
        }

        /// <summary>
        /// Ghi log debug bằng log4net
        /// </summary>
        /// <param name="message">Thông báo lỗi</param>
        /// <param name="ex">Exception</param>
        public static void Debug(object message)
        {
            StackFrame frame = new StackFrame(1);
            MethodBase method = frame.GetMethod();
            string logMessage = String.Format("Method:{0}  Message:{1}", method.Name, message);
            ILog _log = LogManager.GetLogger(method.DeclaringType);
            _log.Debug(logMessage);
        }
    }
}
