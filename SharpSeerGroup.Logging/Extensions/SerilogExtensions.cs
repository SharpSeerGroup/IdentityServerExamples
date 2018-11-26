using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Linq;

namespace Serilog
{
    // https://nblumhardt.com/2016/08/context-and-correlation-structured-logging-concepts-in-net-5/

    public static class SerilogExtensions
    {
        /// <summary>
        /// Returns a logger with the context set to the property name and 
        /// destructured object of the value passed in.
        /// Taken from http://blachniet.com/blog/serilog-good-habits/
        /// </summary>
        /// <example>
        /// Log.With("User", user).Information("NewUser", "{Email}", user.Email);
        /// </example>
        /// <param name="logger"></param>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns>ILogger</returns>
        public static ILogger With(this ILogger logger, string propertyName, object value)
        {
            return logger.ForContext(propertyName, value, destructureObjects: true);
        }

        /// <summary>
        /// Returns a logger with the context set to name of the class of the value passed in
        /// including the destructured object
        /// </summary>
        /// <example>
        /// Log.With(user).Information("NewUser", "{Email}", user.Email);
        /// </example>
        /// <typeparam name="T"></typeparam>
        /// <param name="logger"></param>
        /// <param name="value"></param>
        /// <returns>ILogger</returns>
        public static ILogger With<T>(this ILogger logger, T value)
        {
            return logger.ForContext(typeof(T).Name, value, destructureObjects: true);
        }

        #region Shortcut methods to shorten Information() to Info()

        public static void Info(this ILogger logger, string messageTemplate) 
            => logger.Information(messageTemplate);

        public static void Info<T>(this ILogger logger, string messageTemplate, T propertyValue)
            => logger.Information(messageTemplate, propertyValue);

        public static void Info<T0, T1>(this ILogger logger, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
            => logger.Information(messageTemplate, propertyValue0, propertyValue1);

        public static void Info<T0, T1, T2>(this ILogger logger, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
            => logger.Information(messageTemplate, propertyValue0, propertyValue1, propertyValue2);

        public static void Info(this ILogger logger, string messageTemplate, params object[] propertyValues)
            => logger.Information(messageTemplate, propertyValues);

        public static void Info(this ILogger logger, Exception exception, string messageTemplate)
            => logger.Information(exception, messageTemplate);

        public static void Info<T>(this ILogger logger, Exception exception, string messageTemplate, T propertyValue)
            => logger.Information(exception, messageTemplate, propertyValue);

        public static void Info<T0, T1>(this ILogger logger, Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
            => logger.Information(exception, messageTemplate, propertyValue0, propertyValue1);

        public static void Info<T0, T1, T2>(this ILogger logger, Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
            => logger.Information(exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2);

        public static void Info(this ILogger logger, Exception exception, string messageTemplate, params object[] propertyValues)
            => logger.Information(exception, messageTemplate, propertyValues);

        #endregion
    }
}
