using NLog.Config;
using NLog.Extensions.Logging;
using NLog.Targets;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.Logging
{
    /// <summary>
    /// Privides extension methods for <see cref="ILoggerFactory"/>
    /// </summary>
    public static class LoggFactoryExtensions
    {
        /// <summary>
        /// Add a Default EamaShop LoggerProvider.
        /// </summary>
        /// <param name="loggerFactory"></param>
        /// <param name="minLevel"></param>
        /// <returns></returns>
        public static ILoggerFactory AddDefaultLog(this ILoggerFactory loggerFactory, LogLevel minLevel = LogLevel.Trace)
        {
            if (loggerFactory == null)
            {
                throw new ArgumentNullException(nameof(loggerFactory));
            }
            if (minLevel == LogLevel.None)
            {
                return loggerFactory;
            }

            loggerFactory.AddNLog();
            var config = new LoggingConfiguration();
            var title = "${longdate}|事件Id=${event-properties:item=EventId.Id}|${logger}";
            var body = "${newline}${message}";
            var layout = title + body + "${newline}Error: ${exception}";
            var fileTarge = new FileTarget()
            {

                //Layout = "${longdate}|事件Id=${event-properties:item=EventId.Id}${newline}${logger}|${uppercase:${level}}|${message} ${exception}"
                Layout = layout,
                ArchiveNumbering = ArchiveNumberingMode.Sequence,
                FileName = "../logs/${shortdate}/${level}.log",
                FileNameKind = FilePathKind.Relative,
                ArchiveFileKind = FilePathKind.Relative,
                ArchiveFileName = "../logs/${shortdate}/${level}-{####}.log",
                ArchiveEvery = FileArchivePeriod.None,
                ArchiveAboveSize = 1024 * 1024
            };
            var msTarge = new FileTarget()
            {

                //Layout = "${longdate}|事件Id=${event-properties:item=EventId.Id}${newline}${logger}|${uppercase:${level}}|${message} ${exception}"
                Layout = layout,
                ArchiveNumbering = ArchiveNumberingMode.Sequence,
                FileName = "../logs/${shortdate}/Microsoft.log",
                FileNameKind = FilePathKind.Relative,
                ArchiveFileKind = FilePathKind.Relative,
                ArchiveFileName = "../logs/${shortdate}/${level}-{####}.log",
                ArchiveEvery = FileArchivePeriod.None,
                ArchiveAboveSize = 1024 * 1024
            };
            config.AddTarget("file", fileTarge);
            config.AddTarget("microsoft", msTarge);
            config.AddTarget("skip", new NullTarget());
            var level = (int)minLevel;
            if (level < 1)
            {
                config.AddRuleForOneLevel(NLog.LogLevel.Trace, "file");
            }
            if (level < 2)
            {
                config.AddRuleForOneLevel(NLog.LogLevel.Debug, "file");
            }
            if (level < 3)
            {
                config.AddRuleForOneLevel(NLog.LogLevel.Info, "file");
            }
            if (level < 4)
            {
                config.AddRuleForOneLevel(NLog.LogLevel.Warn, "file");
            }
            if (level < 5)
            {
                config.AddRuleForOneLevel(NLog.LogLevel.Error, "file");
            }
            if (level < 6)
            {
                config.AddRuleForOneLevel(NLog.LogLevel.Fatal, "file");
            }
            config.AddRuleForOneLevel(NLog.LogLevel.Off, "file");
            config.AddRule(NLog.LogLevel.Trace, NLog.LogLevel.Debug, "skip", "Microsoft.*");
            config.AddRule(NLog.LogLevel.Info, NLog.LogLevel.Fatal, "microsoft", "Microsoft.*");
            loggerFactory.ConfigureNLog(config);
            return loggerFactory;
        }
    }
}
