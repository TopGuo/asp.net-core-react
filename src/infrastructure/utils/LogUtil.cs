using System;
using infrastructure.Extensions;
using Microsoft.Extensions.Logging;

namespace infrastructure.utils
{
    public class LogUtil<T> where T : class
    {
        private static ILogger Logger = ServiceExtension.Get<ILogger<T>>();

        public static void Debug(string msg)
        {
            Logger.LogDebug(msg);
        }
        public static void Debug(Exception ex, string msg)
        {
            Logger.LogDebug(ex, msg);
        }
        public static void Error(string msg)
        {
            Logger.LogError(msg);
        }
        public static void Error(Exception ex, string msg)
        {
            Logger.LogError(ex, msg);
        }
        public static void Warn(string msg)
        {
            Logger.LogWarning(msg);
        }
        public static void Warn(Exception ex, string msg)
        {
            Logger.LogWarning(ex, msg);
        }
        public static void Info(string msg)
        {
            Logger.LogInformation(msg);
        }
        public static void Info(Exception ex, string msg)
        {
            Logger.LogInformation(ex, msg);
        }
        public static void Trace(string msg)
        {
            Logger.LogTrace(msg);
        }
        public static void Trace(Exception ex, string msg)
        {
            Logger.LogTrace(ex, msg);
        }
        public static void Critical(string msg)
        {
            Logger.LogCritical(msg);
        }
        public static void Critical(Exception ex, string msg)
        {
            Logger.LogCritical(ex, msg);
        }
    }
}