using System;
using System.Web;
using NLog;

namespace DrynksMe.Services.Logging
{
    public class NLogger : ILogger
    {
        private readonly Logger _logger;
        public NLogger()
        {
            //_logger = LogManager.GetCurrentClassLogger();//has issues running on server
            _logger = LogManager.GetLogger("DrynksLogger");//works
        }
        public void LogInfo(string message)
        {
            _logger.Info(message);
        }

        public void LogWarning(string message)
        {
            _logger.Warn(message);
        }

        public void LogDebug(string message)
        {
            _logger.Debug(message);
        }

        public void LogError(string message)
        {
            _logger.Error(message);
        }
        public void LogError(Exception x)
        {
            LogError(BuildExceptionMessage(x));
        }
        public void LogFatal(string message)
        {
            _logger.Fatal(message);
        }
        public void LogFatal(Exception x)
        {
            LogFatal(BuildExceptionMessage(x));
        }
        string BuildExceptionMessage(Exception x)
        {
            Exception logException = x;
            if (x.InnerException != null)
            {
                logException = x.InnerException;
            }

            string strErrorMsg = Environment.NewLine + "Error in Path : " +
                (HttpContext.Current == null ? "NA" : HttpContext.Current.Request.Path);

            // Get the QueryString along with the Virtual Path
            strErrorMsg += Environment.NewLine + "Raw Url : " +
                 (HttpContext.Current == null ? "NA" : HttpContext.Current.Request.RawUrl);

            // Get the error message
            strErrorMsg += Environment.NewLine + "Message : " + logException.Message;

            // Source of the message
            strErrorMsg += Environment.NewLine + "Source : " + logException.Source;

            // Stack Trace of the error
            strErrorMsg += Environment.NewLine + "Stack Trace : " + logException.StackTrace;

            // Method where the error occurred
            strErrorMsg += Environment.NewLine + "TargetSite : " + logException.TargetSite;

            // Make sure we traverse all the inner exceptions. We've had cases where the exact
            // cause can't be identified because it was wrapped in an EF exception.
            var inner = logException.InnerException;
            while (inner != null)
            {
                strErrorMsg += Environment.NewLine + "Inner Exception : ";
                strErrorMsg += Environment.NewLine + "  Message : " + inner.Message;
                strErrorMsg += Environment.NewLine + "  Source : " + inner.Source;
                strErrorMsg += Environment.NewLine + "  Stack Trace : " + inner.StackTrace;
                strErrorMsg += Environment.NewLine + "  TargetSite : " + inner.TargetSite;

                inner = inner.InnerException;
            }

            strErrorMsg += Environment.NewLine;

            return strErrorMsg;
        }
    }
}