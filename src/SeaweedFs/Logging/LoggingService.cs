// ***********************************************************************
// Assembly         : SeaweedFs
// Author           : piechpatrick
// Created          : 10-10-2021
//
// Last Modified By : piechpatrick
// Last Modified On : 10-10-2021
// ***********************************************************************
namespace SeaweedFs.Logging
{
    /// <summary>
    /// Interface ILoggingService
    /// </summary>
    internal interface ILoggingService
    {
        /// <summary>
        /// Sets the logging level.
        /// </summary>
        /// <param name="logEventLevel">The log event level.</param>
        public void SetLoggingLevel(string logEventLevel)
            => Extensions.LoggingLevelSwitch.MinimumLevel = Extensions.GetLogEventLevel(logEventLevel);
    }
    /// <summary>
    /// Class LoggingService.
    /// Implements the <see cref="SeaweedFs.Logging.ILoggingService" />
    /// </summary>
    /// <seealso cref="SeaweedFs.Logging.ILoggingService" />
    internal class LoggingService : ILoggingService { }
}