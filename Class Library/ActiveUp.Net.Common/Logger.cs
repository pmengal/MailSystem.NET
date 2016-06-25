// Copyright 2001-2010 - Active Up SPRLU (http://www.agilecomponents.com)
//
// This file is part of MailSystem.NET.
// MailSystem.NET is free software; you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
// 
// MailSystem.NET is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.

// You should have received a copy of the GNU Lesser General Public License
// along with SharpMap; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA 

using System;
using System.Text;
using System.IO;
using System.Collections.Generic;

namespace ActiveUp.Net.Mail
{
    /// <summary>
    /// Provides all logging facilities for any applications.
    /// </summary>
#if !PocketPC
    [Serializable]
#endif
    public static class Logger
    {
        /// <summary>
        /// Gets or sets the log entries that are stored in the memory.
        /// </summary>
        public static List<string> LogEntries { get; set; } = new List<string>();

        /// <summary>
        /// Gets or sets the logging level.
        /// </summary>
        public static int LogLevel { get; set; } = 0;

        /// <summary>
        /// Specify whether if you want to log in memory.
        /// </summary>
        public static bool LogInMemory { get; set; } = false;

        /// <summary>
        /// Gets or sets the full path to the text file to append when logging.
        /// </summary>
        public static string LogFile { get; set; } = string.Empty;

        /// <summary>
        /// Specify whether if the logger needs to append the Trace Context.
        /// </summary>
        public static bool UseTraceContext { get; set; } = false;

        /// <summary>
        /// Specify whether if the logger needs to append the Trace Console.
        /// </summary>
        public static bool UseTraceConsole { get; set; } = false;

        /// <summary>
        /// Specify whether if the logging functions are disabled.
        /// </summary>
        public static bool Disabled { get; set; } = false;

        /// <summary>
        /// Gets the number of log entries.
        /// </summary>
        public static int Count {
            get {
                if (LogEntries != null)
                    return LogEntries.Count;
                return 0;
            }
        }

        /// <summary>
        /// Add a log entry using the logging level.
        /// </summary>
        /// <param name="loggerType">The Source of the log entry</param>
        /// <param name="line">The entry to add.</param>
        /// <param name="level">The log entry level.</param>
        public static void AddEntry(Type loggerType, string line, int level)
        {
            if (!Disabled)
                if (level >= LogLevel)
                    AddEntry(loggerType, line);
        }

        /// <summary>
        /// Add a log entry in all logging objects availables.
        /// </summary>
        /// <param name="loggerType">The Source of the log entry</param>
        /// <param name="line">The entry to add.</param>
        public static void AddEntry(Type loggerType, string line)
        {
            if (string.IsNullOrEmpty(LogFile) && !UseTraceContext && !UseTraceConsole)
                return;
            if (!Disabled)
            {
                string logger = loggerType.ToString();
                DateTime now = DateTime.Now;
                StringBuilder logString = new StringBuilder();
                logString.Append(now.Year.ToString());
                logString.Append(".");
                logString.Append(now.Month.ToString().PadLeft(2, '0'));
                logString.Append(".");
                logString.Append(now.Day.ToString().PadLeft(2, '0'));
                logString.Append("-");
                logString.Append(now.Hour.ToString().PadLeft(2, '0'));
                logString.Append(":");
                logString.Append(now.Minute.ToString().PadLeft(2, '0'));
                logString.Append(":");
                logString.Append(now.Second.ToString().PadLeft(2, '0'));

                if (logger.Length > 20)
                    logger = logger.Substring(logger.Length - 20);
                else
                    while (logger.Length < 20)
                        logger += " ";

                logString.Append(" - ");
                logString.Append(logger);
                logString.Append(" - ");
                logString.Append(line);

                if (!string.IsNullOrEmpty(LogFile))
                    AddEntryToFile(logString.ToString());

#if !PocketPC
                if (UseTraceContext)
                    AddEntryToTrace(logString.ToString());
#endif

                if (UseTraceConsole)
                    AddEntryToConsole(logString.ToString());

                OnEntryAdded(EventArgs.Empty);
            }
        }

        /// <summary>
        /// Append the logging text file.
        /// </summary>
        /// <param name="line">The entry to add.</param>
        private static void AddEntryToFile(string line)
        {
            if (!Disabled)
            {
                StreamWriter _fileWriter = new StreamWriter(LogFile, true, Encoding.Default);
                _fileWriter.WriteLine(line);
                _fileWriter.Close();
            }
        }

        private static void AddEntryToConsole(string line)
        {
            if (!Disabled)
                Console.WriteLine("ActiveMail:{0}",line);
        }

#if !PocketPC
        /// <summary>
        /// Append the trace context.
        /// </summary>
        /// <param name="line">The entry to add.</param>
        private static void AddEntryToTrace(string line)
        {
            if (!Disabled)
            {
                if (System.Web.HttpContext.Current != null)
                    System.Web.HttpContext.Current.Trace.Write("ActiveMail", line);
            }
        }
#endif

        /// <summary>
        /// Gets an ArrayList containing the specified number of last entries.
        /// </summary>
        /// <param name="lines">The max lines to retrieve.</param>
        /// <returns>An ArrayList containing the maximum log entries.</returns>
        public static List<string> LastEntries(int lines)
        {
            List<string> entries = new List<string>();

            if (Count > 0)
                {
                for (int i = Count - lines; i <= Count; i++)
                    {
                    if (i >= 0)
                        {
                        if (LogEntries != null)
                            entries.Add(LogEntries[i]);
                    }
                }
            }
            return entries;
        }


        /// <summary>
        /// Gets a string containing a maximum of 30 log entries.
        /// </summary>
        /// <returns>A maximum of 30 entries separeted by a carriage return.</returns>
        public static string LastEntries()
        {
            List<string> entries = LastEntries(30);

            StringBuilder stringEntries = new StringBuilder();

            foreach (string entry in entries)
                stringEntries.Append(entry + "\n");

            return stringEntries.ToString();
        }

        /// <summary>
        /// Gets the last entry of the log.
        /// </summary>
        /// <returns>A string containing the last entry.</returns>
        public static string LastEntry()
        {
            if (LogEntries != null)
                return LogEntries[LogEntries.Count - 1];

            return string.Empty;
        }

        /// <summary>
        /// The EntryAdded event handler.
        /// </summary>
        public static event EventHandler EntryAdded;

        /// <summary>
        /// OnEntryAdded event.
        /// </summary>
        /// <param name="e">The event arguments.</param>
        private static void OnEntryAdded(EventArgs e) 
        {
            if (EntryAdded != null)
                EntryAdded(null,e);
        }

    }
}
