using NLog;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ServiceLoggerManager :ILoggerService
    {
        private static ILogger Logger = LogManager.GetCurrentClassLogger();

        public void LogDebug(string message) =>Logger.Debug(message);
       

        public void LogError(string message) => Logger.Error(message);


        public void LogInfo(string message) => Logger.Info(message);


        public void LogWarning(string message) => Logger.Warn(message);

    }
}
