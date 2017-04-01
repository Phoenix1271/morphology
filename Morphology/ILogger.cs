using System;

namespace Morphology
{
    /// <summary>
    /// The core Morphology logging API, used for writing log events.
    /// </summary>
    public interface ILogger
    {
        #region Public Methods

        /// <summary>
        /// Captures a log event with the error severity
        /// </summary>
        /// <param name="message">Message describing the event.</param>
        /// <example>
        /// Log.Error("Failed to estabilish connection.")
        /// </example>
        void Error(string message);

        /// <summary>
        /// Captures a log event with the error severity
        /// </summary>
        /// <param name="exception">Exception related to event.</param>
        /// <param name="message">Message describing the event.</param>
        /// <example>
        /// Log.Error("Failed to estabilish connection.")
        /// </example>
        void Error(Exception exception, string message);

        /// <summary>
        /// Captures a log event with the fatal severity
        /// </summary>
        /// <param name="message">Message describing the event.</param>
        /// <example>
        /// Log.Fatal("Process terminating.");
        /// </example>
        void Fatal(string message);

        /// <summary>
        /// Captures a log event with the fatal severity
        /// </summary>
        /// <param name="exception">Exception related to event.</param>
        /// <param name="message">Message describing the event.</param>
        /// <example>
        /// Log.Fatal("Process terminating.");
        /// </example>
        void Fatal(Exception exception, string message);

        /// <summary>
        /// Captures a log event with the information severity
        /// </summary>
        /// <param name="message">Message describing the event.</param>
        /// <example>
        /// Log.Information($"Processed {count} records in {time}.")
        /// </example>
        void Information(string message);

        /// <summary>
        /// Captures a log event with the information severity
        /// </summary>
        /// <param name="exception">Exception related to event.</param>
        /// <param name="message">Message describing the event.</param>
        /// <example>
        /// Log.Information(ex, $"Processed {count} records in {time}.")
        /// </example>
        void Information(Exception exception, string message);

        /// <summary>
        /// Captures a log event with the debug severity
        /// </summary>
        /// <param name="message">Message describing the event.</param>
        /// <example>
        /// Log.Debug(ex, "Just wondering where this rabbit hole leads.")
        /// </example>
        void Verbose(string message);

        /// <summary>
        /// Captures a log event with the debug severity
        /// </summary>
        /// <param name="exception">Exception related to event.</param>
        /// <param name="message">Message describing the event.</param>
        /// <example>
        /// Log.Debug(ex, "Just wondering where this rabbit hole leads.")
        /// </example>
        void Verbose(Exception exception, string message);

        /// <summary>
        /// Captures a log event with the warning severity
        /// </summary>
        /// <param name="message">Message describing the event.</param>
        /// <example>
        /// Log.Warning($"Skipped loading module {name}.")
        /// </example>
        void Warning(string message);

        /// <summary>
        /// Captures a log event with the warning severity
        /// </summary>
        /// <param name="exception">Exception related to event.</param>
        /// <param name="message">Message describing the event.</param>
        /// <example>
        /// Log.Warning($"Skipped loading module {name}.")
        /// </example>
        void Warning(Exception exception, string message);

        #endregion
    }
}
