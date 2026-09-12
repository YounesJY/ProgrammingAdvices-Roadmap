using System;
using System.Diagnostics;
using System.Text;

namespace DVLD_Common
{
    /// <summary>
    /// Provides static methods for logging information, warning, and error events to the Windows Event Log.
    /// This class is a wrapper around the System.Diagnostics.EventLog class that simplifies event logging.
    /// </summary>
    /// <remarks>
    /// This class must be configured at least once before using any logging methods.
    /// Call <see cref="Configure"/> in your application's entry point (e.g., Program.Main()).
    /// Not thread-safe due to potential race conditions in the <see cref="Configure"/> method.
    /// </remarks>
    public static class EventLogger
    {
        private static string _sourceName = null;

        /// <summary>
        /// Configures the event log source with the specified source and log names.
        /// This method must be called once before using any logging methods.
        /// </summary>
        /// <param name="sourceName">The name of the event log source to create or use. This identifies the application in the Event Log.</param>
        /// <param name="logName">The name of the event log to associate with the source (typically "Application", "System", or custom log name).</param>
        /// <exception cref="ArgumentException">Thrown when sourceName or logName is null, empty, or contains only whitespace.</exception>
        /// <remarks>
        /// If the event source does not exist, this method will attempt to create it.
        /// Concurrent calls to this method are not thread-safe and may result in unexpected behavior.
        /// 
        /// Example usage in Program.Main():
        /// <code>
        /// EventLogger.Configure("DVLD_Application", "DVLD_Events");
        /// </code>
        /// </remarks>
        public static void Configure(string sourceName, string logName)
        {
            if (string.IsNullOrWhiteSpace(sourceName) || string.IsNullOrWhiteSpace(logName))
                throw new ArgumentException($"The values for '{nameof(sourceName)}' and '{nameof(logName)}' must not be null or empty.");

            if (!EventLog.SourceExists(sourceName))
                EventLog.CreateEventSource(sourceName, logName);

            _sourceName = sourceName;
        }

        /// <summary>
        /// Writes an <see cref="EventLogEntryType.Information"/> type entry with the given message text to the Event Log.
        /// </summary>
        /// <param name="message">The message to be logged in the Event Log. If null or whitespace, a default message will be used.</param>
        /// <param name="eventID">The application-specific identifier for the event. Default is 0.</param>
        /// <exception cref="InvalidOperationException">Thrown when EventLogger has not been configured via the <see cref="Configure"/> method.</exception>
        /// <remarks>
        /// Information events are used to log successful operations and general application events.
        /// </remarks>
        public static void LogInformation(string message, int eventID = 0) 
            => LogEntry(message, EventLogEntryType.Information, eventID);

        /// <summary>
        /// Writes an <see cref="EventLogEntryType.Warning"/> type entry with the given message text to the Event Log.
        /// </summary>
        /// <param name="message">The message to be logged in the Event Log. If null or whitespace, a default message will be used.</param>
        /// <param name="eventID">The application-specific identifier for the event. Default is 0.</param>
        /// <exception cref="InvalidOperationException">Thrown when EventLogger has not been configured via the <see cref="Configure"/> method.</exception>
        /// <remarks>
        /// Warning events are used to log potential issues that do not prevent application execution.
        /// </remarks>
        public static void LogWarning(string message, int eventID = 0) 
            => LogEntry(message, EventLogEntryType.Warning, eventID);

        /// <summary>
        /// Writes an <see cref="EventLogEntryType.Error"/> type entry with the given message text to the Event Log.
        /// </summary>
        /// <param name="message">The message to be logged in the Event Log. If null or whitespace, a default message will be used.</param>
        /// <param name="eventID">The application-specific identifier for the event. Default is 0.</param>
        /// <exception cref="InvalidOperationException">Thrown when EventLogger has not been configured via the <see cref="Configure"/> method.</exception>
        /// <remarks>
        /// Error events are used to log failures and exceptions that require attention.
        /// </remarks>
        public static void LogError(string message, int eventID = 0) 
            => LogEntry(message, EventLogEntryType.Error, eventID);

        /// <summary>
        /// Writes an <see cref="EventLogEntryType.Error"/> type entry based on the exception provided to the Event Log.
        /// </summary>
        /// <param name="ex">The exception to be logged. The exception details will be formatted and logged.</param>
        /// <param name="eventID">The application-specific identifier for the event. Default is 0.</param>
        /// <exception cref="InvalidOperationException">Thrown when EventLogger has not been configured via the <see cref="Configure"/> method.</exception>
        /// <remarks>
        /// This method is useful for logging exceptions without an accompanying message.
        /// The complete exception stack trace and details will be logged.
        /// </remarks>
        public static void LogError(Exception ex, int eventID = 0) 
            => LogEntry(FormatExceptionForLogging(null, ex), EventLogEntryType.Error, eventID);

        /// <summary>
        /// Writes an <see cref="EventLogEntryType.Error"/> type entry from both the message and exception provided to the Event Log.
        /// </summary>
        /// <param name="message">The message to be logged in the Event Log alongside the exception details.</param>
        /// <param name="ex">The exception to be logged. The exception details will be formatted and logged with the message.</param>
        /// <param name="eventID">The application-specific identifier for the event. Default is 0.</param>
        /// <exception cref="InvalidOperationException">Thrown when EventLogger has not been configured via the <see cref="Configure"/> method.</exception>
        /// <remarks>
        /// This method is useful for logging exceptions with additional context provided by the developer message.
        /// Both the message and the complete exception stack trace will be logged.
        /// </remarks>
        public static void LogError(string message, Exception ex, int eventID = 0) 
            => LogEntry(FormatExceptionForLogging(message, ex), EventLogEntryType.Error, eventID);

        /// <summary>
        /// Formats exception information and developer message for logging purposes.
        /// </summary>
        /// <param name="message">The developer message to include with the exception. Can be null or empty.</param>
        /// <param name="ex">The exception to format. Can be null.</param>
        /// <returns>A formatted string containing the exception details and/or message, or a default message if both are null.</returns>
        private static string FormatExceptionForLogging(string message, Exception ex)
        {
            if (string.IsNullOrWhiteSpace(message) && ex is null)
                return "[No data available]";

            var sb = new StringBuilder();

            if (ex != null)
            {
                sb.AppendLine("=== Exception ===");
                sb.AppendLine(ex.ToString());
            }

            if (!string.IsNullOrWhiteSpace(message))
            {
                sb.AppendLine("\n=== Developer Message ===");
                sb.AppendLine(message);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Gets the configured event log source name.
        /// </summary>
        /// <returns>The source name if configured.</returns>
        /// <exception cref="InvalidOperationException">Thrown when EventLogger has not been configured.</exception>
        private static string GetSourceName() 
            => _sourceName ?? throw new InvalidOperationException($"The EventLogger must be configured before use. Call EventLogger.Configure() in your application's entry point.");

        /// <summary>
        /// Writes a log entry to the Event Log with the specified message, entry type, and event ID.
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        /// <param name="entryType">The type of log entry (Information, Warning, or Error).</param>
        /// <param name="eventID">The application-specific identifier for the event.</param>
        private static void LogEntry(string message, EventLogEntryType entryType, int eventID) 
            => EventLog.WriteEntry(GetSourceName(), string.IsNullOrWhiteSpace(message) ? "[No data available]" : message, entryType, eventID);
    }
}
